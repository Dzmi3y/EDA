using EDA.Service.Identity.Entities;
using EDA.Service.Identity.Models;

namespace EDA.Service.Identity.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<RefreshTokenInfo> GetAsync(Guid token);
        Task SetAsUsedAsync(Guid token);
        Task SetAsInvalidatedAsync(Guid token);
        Task<Guid> CreateAsync(RefreshTokenInfo tokenInfo);
        Task<User?> GetUserByRefreshTokenAsync(Guid refreshToken);
    }
}
