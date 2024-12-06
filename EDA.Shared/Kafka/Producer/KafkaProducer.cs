using Confluent.Kafka;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages.Base;
using Newtonsoft.Json;

namespace EDA.Shared.Kafka.Producer
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<String, string> _producer;

        public KafkaProducer(ProducerConfig config)
        {
            _producer = new ProducerBuilder<String, string>(config).Build();
        }

        public async Task SendMessageAsync(Topics topic,Guid key, MessageBase message)
        {
            string jsonString = JsonConvert.SerializeObject(message, Formatting.Indented);
            var msg = new Message<String, string>
            {
                Key = key.ToString(),
                Value = jsonString
            };
            await _producer.ProduceAsync(topic.ToStringRepresentation(), msg);
        }
    }

}
