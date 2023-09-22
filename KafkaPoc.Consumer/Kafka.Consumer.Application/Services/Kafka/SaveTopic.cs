using Confluent.Kafka;
using KafkaConsumer.Domain.Entities;
using KafkaConsumer.Domain.Events;
using KafkaConsumer.Domain.Helpers.DataContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Kafka.Consumer.Application.Services.Kafka
{
    public class SaveTopic : ISaveTopic
    {
        private readonly ILogger<SaveTopic> _logger;
        private readonly IConfiguration _config;
        public SaveTopic(ILogger<SaveTopic> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public async Task Save()
        {
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                var eventName = "TestTopicEvent";

                c.Subscribe(eventName);

                CancellationTokenSource cts = new CancellationTokenSource();

                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = c.Consume(cts.Token);

                            var topicEvent = JsonConvert.DeserializeObject<TestTopicEvent>(cr.Message.Value);

                            var topic = new Topic
                            {
                                Name = topicEvent!.ProducerName,
                                Content = topicEvent.Content,
                                CreationDate = topicEvent.CreationDate
                            };

                            using var context = new DataContext(_config);

                            await context.Topic.AddAsync(topic);

                            await context.SaveChangesAsync();

                            _logger.Log(logLevel: LogLevel.Information, message: $"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");
                        }
                        catch (ConsumeException e)
                        {
                            _logger.Log(LogLevel.Error, $"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    c.Close();
                }
            }
        }
    }
}
