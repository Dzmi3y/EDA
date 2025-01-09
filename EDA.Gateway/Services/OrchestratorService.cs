using EDA.Gateway.EventHandlers;
using EDA.Shared.Kafka.Consumer;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Redis.Interfaces;

namespace EDA.Gateway.Services
{
    public class OrchestratorService : BackgroundService
    {
        private readonly IRedisStringsService _redis;
        private readonly KafkaConsumerBaseConfig _config;
        private readonly ILogger<KafkaToRedisEventHandler> _logger;
        private readonly Topics[] _topicList;

        public OrchestratorService(IRedisStringsService redis, KafkaConsumerBaseConfig config, ILogger<KafkaToRedisEventHandler> logger, Topics[] topicList)
        {
            _redis = redis;
            _config = config;
            _logger = logger;
            _topicList = topicList;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            foreach (var topic in _topicList)
            {
                var backgroundService = new KafkaToRedisEventHandler(_redis, _config, _logger, topic);
                await backgroundService.StartAsync(stoppingToken);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
