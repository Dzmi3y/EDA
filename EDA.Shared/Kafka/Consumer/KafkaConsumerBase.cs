using Confluent.Kafka;
using EDA.Shared.Kafka.Enums;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EDA.Shared.Kafka.Consumer
{
    public abstract class KafkaConsumerBase : BackgroundService
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly Topics? _topic;
        private readonly ILogger<KafkaConsumerBase> _logger;
        private readonly KafkaConsumerBaseConfig _config;

        protected KafkaConsumerBase(KafkaConsumerBaseConfig config, Topics topic, ILogger<KafkaConsumerBase> logger)
        {
            _consumer = new ConsumerBuilder<string, string>(config).Build();
            _topic = topic;
            _logger = logger;
            _config= config;
        }

        private async Task TryToStartConsuming(CancellationToken stoppingToken)
        {
             int maxRetryCount = _config.MaxRetryCount;
             int baseDelayMilliseconds = _config.BaseDelayMilliseconds;

            for (var i = 0; i < maxRetryCount; i++)
            {
                try
                {
                    await Task.Run(() => StartConsuming(stoppingToken, (Topics)_topic), stoppingToken);
                    return; 
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError($"Attempt {i + 1} failed: {ex.Message}");

                    if (i == maxRetryCount - 1)
                    {
                        _logger.LogError("Max retry attempts reached. Throwing exception.");
                        throw; 
                    }

                    var delay = baseDelayMilliseconds * Math.Pow(2, i);
                    _logger.LogInformation($"Waiting {delay}ms before next retry attempt.");
                    await Task.Delay((int)delay, stoppingToken);
                }
            }
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_topic == null)
            {
                throw new ArgumentNullException(nameof(_topic), "Topic cannot be null or empty.");
            }

            try
            {
                await TryToStartConsuming(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");
                throw;
            }
        }


        public async Task StartConsuming(CancellationToken stoppingToken, Topics topic)
        {
            _consumer.Subscribe(topic.ToStringRepresentation());
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumeResult = _consumer.Consume(stoppingToken);

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
