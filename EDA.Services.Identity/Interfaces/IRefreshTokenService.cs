using EDA.Services.Identity.Entities;
using EDA.Services.Identity.Models;

namespace EDA.Services.Identity.Interfaces
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
