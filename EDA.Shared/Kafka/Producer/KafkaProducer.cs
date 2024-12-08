using Confluent.Kafka;
using EDA.Shared.Kafka.Enums;

namespace EDA.Shared.Kafka.Producer
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<String, string> _producer;

        public KafkaProducer(ProducerConfig config)
        {
            _producer = new ProducerBuilder<String, string>(config).Build();
        }

        public async Task SendMessageAsync(Topics topic,string key, string message)
        {
            var msg = new Message<String, string>
            {
                Key = key,
                Value = message
            };
            await _producer.ProduceAsync(topic.ToStringRepresentation(), msg);
        }
    }

}
