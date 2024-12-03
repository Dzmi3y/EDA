using Confluent.Kafka;

namespace EDA.Shared.Kafka.Producer
{
    public class KafkaProducerConfig<Tk, Tv> : ProducerConfig
    {
        public string Topic { get; set; }
    }
}
