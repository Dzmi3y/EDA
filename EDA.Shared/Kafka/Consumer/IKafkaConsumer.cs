using Confluent.Kafka;
using EDA.Shared.Kafka.Enums;

namespace EDA.Shared.Kafka.Consumer
{
    public interface IKafkaConsumer
    {
        Task StartConsuming(CancellationToken stoppingToken, Topics topic, Action<ConsumeResult<string, string>>? messageHandler);
    }
}
