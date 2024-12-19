using EDA.Services.Identity.Entities;
using EDA.Services.Identity.Models;

namespace EDA.Services.Identity.Interfaces
{
    public interface IIssueTokenService
    {
        Task<AuthenticationResult> RefreshTokenAsync(Guid refreshToken);
        Task<AuthenticationResult> GenerateAuthenticationResult(User user);
    }
}
