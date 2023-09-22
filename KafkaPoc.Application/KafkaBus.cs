using Confluent.Kafka;
using KafkaPoc.Domain.Events.Base;
using Newtonsoft.Json;

namespace KafkaPoc.Application
{
    public class KafkaBus : IKafkaBus
    {
        public async Task Publish<T>(T @event) where T : Event
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092", EnableIdempotence = true };

            // If serializers are not specified, default serializers from
            // `Confluent.Kafka.Serializers` will be automatically used where
            // available. Note: by default strings are encoded as UTF8.
            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var title = typeof(T).Name;
                    var content = JsonConvert.SerializeObject(@event);

                    var dr = await p.ProduceAsync(title, new Message<Null, string> { Value = content });
                    Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }
    }
}