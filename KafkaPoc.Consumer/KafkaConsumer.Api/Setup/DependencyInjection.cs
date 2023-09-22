using Kafka.Consumer.Application.Services;
using Kafka.Consumer.Application.Services.Kafka;

namespace KafkaConsumer.Api.Setup
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<ISaveTopic, SaveTopic>();

            // Kafka Consume
            services.AddHostedService<KafkaService>();
        }
    }
}
