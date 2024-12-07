using StackExchange.Redis;
using System;
namespace EDA.Shared.Redis
{
    public class RedisConfig
    {
        public required string Configuration;
        public required TimeSpan DefaultExpiry;
        public required TimeSpan DefaultTimeout;
    }
}
