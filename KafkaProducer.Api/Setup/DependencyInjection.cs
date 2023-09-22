using KafkaPoc.Application;
using KafkaPoc.Application.Services;

namespace KafkaProducer.Api.Setup
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {

            services.AddScoped<IPublishEventService, PublishEventService>();

            // Kafka
            services.AddTransient<IKafkaBus, KafkaBus>();
            

            return services;
        }
    }
}
