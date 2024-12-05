using Confluent.Kafka;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages.Base;
using Newtonsoft.Json;

namespace EDA.Shared.Kafka.Producer
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducer(ProducerConfig config)
        {
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task SendMessageAsync(Topics topic, MessageBase message)
        {
            string jsonString = JsonConvert.SerializeObject(message, Formatting.Indented);
            var msg = new Message<Null, string> { Value = jsonString };
            await _producer.ProduceAsync(topic.ToStringRepresentation(), msg);
        }
    }

}
