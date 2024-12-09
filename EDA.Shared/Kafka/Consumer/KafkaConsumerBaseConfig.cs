using Confluent.Kafka;

namespace EDA.Shared.Kafka.Consumer
{
    public class KafkaConsumerBaseConfig : ConsumerConfig
    {
        public int MaxRetryCount { get; set; }
        public int BaseDelayMilliseconds { get; set; }
    }
}