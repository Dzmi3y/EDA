using Confluent.Kafka;
using EDA.Shared.Kafka.Consumer;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Redis.Interfaces;
using Microsoft.Extensions.Logging;

namespace EDA.Gateway.Services
{
    public class ProductResponseKafkaConsumerService: KafkaConsumerBase
    {
        private readonly IRedisStringsService _redis;
        public ProductResponseKafkaConsumerService(IRedisStringsService redis, KafkaConsumerBaseConfig config,
        ILogger<ProductResponseKafkaConsumerService> logger)
            : base(config, Topics.ProductPageResponse, logger)
        {
            _redis = redis;
        }

        protected override async Task HandleAsync(ConsumeResult<string, string> result)
        {
            await _redis.AddAsync(result.Key, result.Value);
        }
    }
}
