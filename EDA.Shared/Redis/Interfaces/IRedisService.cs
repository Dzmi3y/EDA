
namespace EDA.Shared.Redis.Interfaces
{
    public interface IRedisService : IDisposable
    {
        Task<bool> AddAsync(string key, string value, TimeSpan? expiry = null);
        Task<bool> RemoveAsync(string key);
        Task<(bool keyExists, string value)> KeyIsExistAsync(string key);
        Task<string> WaitForKeyAsync(string key, TimeSpan? timeout = null);
    }
}
