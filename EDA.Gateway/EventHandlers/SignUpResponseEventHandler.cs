﻿using Confluent.Kafka;
using EDA.Shared.Kafka.Consumer;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Redis.Interfaces;

namespace EDA.Gateway.EventHandlers
{
    public class SignInResponseEventHandler : KafkaConsumerBase
    {
        private readonly IRedisStringsService _redis;
        public SignInResponseEventHandler(IRedisStringsService redis, KafkaConsumerBaseConfig config,
            ILogger<ProductResponseEventHandler> logger)
            : base(config, Topics.SignInResponse, logger)
        {
            _redis = redis;
        }

        protected override async Task HandleAsync(ConsumeResult<string, string> result)
        {
            await _redis.AddAsync(result.Key, result.Value);
        }
    }
}
