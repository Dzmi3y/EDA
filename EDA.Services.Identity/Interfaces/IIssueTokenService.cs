using EDA.Service.Identity.Entities;
using EDA.Service.Identity.Models;

namespace EDA.Service.Identity.Interfaces
{
    public interface IIssueTokenService
    {
        Task<AuthenticationResult> RefreshTokenAsync(Guid refreshToken);
        Task<AuthenticationResult> GenerateAuthenticationResult(User user);
    }
}
