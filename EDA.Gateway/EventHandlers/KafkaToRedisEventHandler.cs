using Confluent.Kafka;
using EDA.Shared.Kafka.Consumer;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Redis.Interfaces;

namespace EDA.Gateway.EventHandlers
{
    public class KafkaToRedisEventHandler : KafkaConsumerBase
    {
        private readonly IRedisStringsService _redis;
        public KafkaToRedisEventHandler(IRedisStringsService redis, KafkaConsumerBaseConfig config,
            ILogger<KafkaToRedisEventHandler> logger, Topics topic)
            : base(config, topic, logger)
        {
            Console.WriteLine("a");
            _redis = redis;
        }

        protected override async Task HandleAsync(ConsumeResult<string, string> result)
        {
            await _redis.AddAsync(result.Key, result.Value);
        }
    }
}
