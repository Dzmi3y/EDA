using Confluent.Kafka;
using EDA.Shared.Kafka.Enums;
using Microsoft.Extensions.Hosting;

namespace EDA.Shared.Kafka.Consumer
{
    public abstract class KafkaConsumerBase : BackgroundService
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly Topics? _topic;

        protected KafkaConsumerBase(ConsumerConfig config, Topics topic)
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

            await Task.Run(() =>  StartConsuming(stoppingToken, (Topics)_topic), stoppingToken);
        }

        public async Task StartConsuming(CancellationToken stoppingToken, Topics topic)
        {
            _consumer.Subscribe(topic.ToStringRepresentation());
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumeResult = _consumer.Consume(stoppingToken);

                    _consumer.Consume(stoppingToken);
                    if (consumeResult != null)
                    {
                        await HandleAsync(consumeResult);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _consumer.Close();
            }
        }

        protected abstract Task HandleAsync(ConsumeResult<string, string> result);

        public override void Dispose()
        {
            _consumer.Close();
            base.Dispose();
        }
    }
}
