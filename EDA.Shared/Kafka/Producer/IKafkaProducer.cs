using EDA.Shared.Kafka.Enums;

namespace EDA.Shared.Kafka.Producer
{
    public interface IKafkaProducer
    {
        Task SendMessageAsync(Topics topic, string key, string message);
    }

}
