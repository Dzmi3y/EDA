using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDA.Shared.Redis.Enums;

namespace EDA.Shared.Redis.Services
{
    public class RedisService
    {
        private readonly ConnectionMultiplexer _redis;

        public RedisService(string configuration)
        {
            _redis = ConnectionMultiplexer.Connect(configuration);
        }

        public async Task AddRequest(string key,string value, RedisDatabaseNames dbName)
        {
            var db = _redis.GetDatabase((int)dbName);
            var isKeyExist = await db.KeyExistsAsync(key);
            if (isKeyExist)
            {
                return;
            }

            await db.StringSetAsync(key, value);
        }
    }
}
