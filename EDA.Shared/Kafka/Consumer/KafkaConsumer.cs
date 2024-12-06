using Confluent.Kafka;
using EDA.Shared.Kafka.Enums;
using Microsoft.Extensions.Hosting;

namespace EDA.Shared.Kafka.Consumer
{
    public class KafkaConsumer : BackgroundService, IKafkaConsumer
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly Topics? _topic;

        public KafkaConsumer(ConsumerConfig config, Topics? topic = null)
        {
            _consumer = new ConsumerBuilder<string, string>(config).Build();
            _topic = topic;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_topic == null)
            {
                throw new ArgumentNullException(nameof(_topic), "Topic cannot be null or empty.");
            }

            await StartConsuming(stoppingToken, (Topics)_topic);
        }
        public Task<string> StartConsuming(CancellationToken stoppingToken, Topics topic, Guid? key = null)
        {
            var l = topic.ToStringRepresentation();
            _consumer.Subscribe(topic.ToStringRepresentation());
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumeResult = _consumer.Consume(stoppingToken);

                    Console.WriteLine(
                        $"Consumed message '{consumeResult.Value}' at: '{consumeResult.TopicPartitionOffset}'.");
                    //return Task.FromResult(consumeResult.Value);

                }
            }
            catch (OperationCanceledException)
            {
                _consumer.Close();
            }

            return Task.FromResult("");
        }

        public override void Dispose()
        {
            _consumer.Close();
            base.Dispose();
        }
    }
}
