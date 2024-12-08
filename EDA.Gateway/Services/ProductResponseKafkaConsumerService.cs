using Confluent.Kafka;
using EDA.Shared.Kafka.Consumer;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Redis.Interfaces;

namespace EDA.Gateway.Services
{
    public class ProductResponseKafkaConsumerService: KafkaConsumerBase
    {
        private readonly IRedisStringsService _redis;
        public ProductResponseKafkaConsumerService(IRedisStringsService redis,ConsumerConfig config)
            : base(config, Topics.ProductPageResponse)
        {
            _redis = redis;
        }

        protected override async Task HandleAsync(ConsumeResult<string, string> result)
        {
            await _redis.AddAsync(result.Key, result.Value);
        }
    }
}
