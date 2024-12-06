using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages.Base;

namespace EDA.Shared.Kafka.Producer
{
    public interface IKafkaProducer
    {
        Task SendMessageAsync(Topics topic, Guid key, MessageBase message);
    }

}
