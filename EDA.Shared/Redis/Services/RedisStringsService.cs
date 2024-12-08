using EDA.Shared.Redis.Interfaces;
using StackExchange.Redis;

namespace EDA.Shared.Redis.Services
{
    public class RedisStringsService : IRedisStringsService
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly TimeSpan _defaultExpiry;
        private readonly TimeSpan _defaultTimeout;
        private bool _disposed = false;

        public RedisStringsService(RedisConfig redisConfig)
        {
            _redis = ConnectionMultiplexer.Connect(redisConfig.Configuration);
            _defaultExpiry = redisConfig.DefaultExpiry;
            _defaultTimeout = redisConfig.DefaultTimeout;
        }

        public async Task<bool> AddAsync(string key, string value, TimeSpan? expiry = null)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Key cannot be null or empty", nameof(key));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(value));
            }

            var db = _redis.GetDatabase();
            expiry ??= _defaultExpiry;
            return await db.StringSetAsync(key, value, expiry, When.NotExists);
        }

        public async Task<bool> RemoveAsync(string key)
        {

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Key cannot be null or empty", nameof(key));
            }

            var db = _redis.GetDatabase();

            return await db.KeyDeleteAsync(key);
        }

        public async Task<(bool keyExists, string value)> CheckKeyExistsAsync(string key)
        {
            var db = _redis.GetDatabase();
            if (await db.KeyExistsAsync(key))
            {
                var value = await db.StringGetAsync(key);
                return (true, value.ToString());
            }

            return (false, string.Empty);
        }

        public async Task<string> WaitForKeyAsync(string key,  TimeSpan? timeout = null)
        {
            timeout ??= _defaultTimeout;
            var cancellationTokenSource = new CancellationTokenSource((TimeSpan)timeout);
            try
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    (bool keyExists, string value) = await CheckKeyExistsAsync(key);
                    if (keyExists)
                    {
                        return value;
                    }

                    await Task.Delay(1000);
                }
                throw new TimeoutException(
                    $"Timeout reached: The key '{key}' did not appear within the specified timeout of {timeout?.TotalSeconds} seconds.");
            }
            catch (OperationCanceledException)
            {
                throw new TimeoutException(
                    $"Timeout reached: The key '{key}' did not appear within the specified timeout of {timeout?.TotalSeconds} seconds.");
            }
        }

        public void Dispose()
        {
            Dispose(true); 
            GC.SuppressFinalize(this);

        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _redis.Close();
                    _redis.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
