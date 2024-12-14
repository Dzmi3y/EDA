
namespace EDA.Shared.Redis.Interfaces
{
    public interface IRedisStringsService : IDisposable
    {
        Task<bool> AddAsync(string key, string value, TimeSpan? expiry = null);
        Task<bool> RemoveAsync(string key);
        Task<(bool keyExists, string value)> ReadAsync(string key);
        Task<string> WaitForKeyAsync(string key, bool deleteAfterReading = false, TimeSpan? timeout = null);
    }
}
