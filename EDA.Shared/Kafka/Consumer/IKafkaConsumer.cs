using EDA.Shared.Kafka.Enums;

namespace EDA.Shared.Kafka.Consumer
{
    public interface IKafkaConsumer
    {
        Task<string> StartConsuming(CancellationToken stoppingToken, Topics topic, Guid? key);
    }

}
