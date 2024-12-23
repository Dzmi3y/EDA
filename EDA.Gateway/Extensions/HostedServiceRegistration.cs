using EDA.Gateway.EventHandlers;
using EDA.Shared.Kafka.Consumer;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Redis.Interfaces;

namespace EDA.Gateway.Extensions
{
    public static class HostedServiceRegistration
    {
        public static void AddKafkaToRedisHostedServices(this IServiceCollection services, Topics[] topics)
        {
            foreach (var topic in topics)
            {
                services.AddHostedService(provider =>
                {
                    var redis = provider.GetRequiredService<IRedisStringsService>();
                    var config = provider.GetRequiredService<KafkaConsumerBaseConfig>();
                    var logger = provider.GetRequiredService<ILogger<ProductResponseEventHandler>>();
                    return new KafkaToRedisEventHandler(redis, config, logger, topic);
                });
            }
        }
    }
}
