using Confluent.Kafka;
using EDA.Shared.Kafka.Enums;
using Microsoft.Extensions.Hosting;

namespace EDA.Shared.Kafka.Consumer
{
    public class KafkaConsumer : BackgroundService, IKafkaConsumer
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly Topics? _topic;
        private readonly Action<ConsumeResult<string, string>>? _handler;

        public KafkaConsumer(ConsumerConfig config, Action<ConsumeResult<string, string>>? handler = null, Topics? topic = null)
        {
            _consumer = new ConsumerBuilder<string, string>(config).Build();
            _topic = topic;
            _handler = handler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_topic == null)
            {
                throw new ArgumentNullException(nameof(_topic), "Topic cannot be null or empty.");
            }

            await StartConsuming(stoppingToken, (Topics)_topic, _handler);
        }
        public async Task StartConsuming(CancellationToken stoppingToken, Topics topic, Action<ConsumeResult<string, string>>? messageHandler)
        {
            _consumer.Subscribe(topic.ToStringRepresentation());
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumeResult = _consumer.Consume(stoppingToken);

                    _consumer.Consume(stoppingToken);
                    if (consumeResult != null && messageHandler != null)
                    {
                        await Task.Run(() => messageHandler.Invoke(consumeResult), stoppingToken);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _consumer.Close();
            }
        }

        public override void Dispose()
        {
            _consumer.Close();
            base.Dispose();
        }
    }
}
