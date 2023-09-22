using Kafka.Consumer.Application.Services.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Kafka.Consumer.Application.Services
{
    public class KafkaService : IHostedService
    {
        private readonly ILogger<KafkaService> _logger;
        private readonly ISaveTopic _topic;
        public KafkaService(ILogger<KafkaService> logger, ISaveTopic topic)
        {
            _logger = logger;
            _topic = topic;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consumer service has started.");

            await _topic.Save();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consumer sevices has stopped");
            Console.WriteLine("Parei");

            return Task.CompletedTask;
        }

    }
}
