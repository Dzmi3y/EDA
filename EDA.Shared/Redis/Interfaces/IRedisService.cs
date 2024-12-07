using EDA.Shared.Redis.Enums;

namespace EDA.Shared.Redis.Interfaces
{
    public interface IRedisService:IDisposable
    {
        Task<bool> AddAsync(string key, string value, RedisDatabaseNames dbName, TimeSpan? expiry);
        Task<bool> RemoveAsync(string key, RedisDatabaseNames dbName);
        Task<(bool keyExists, string value)> KeyIsExistAsync(string key, RedisDatabaseNames dbName);
        Task<string> WaitForKeyAsync(string key, RedisDatabaseNames dbName, TimeSpan? timeout);
    }
}
