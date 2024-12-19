using EDA.Services.Identity.Entities;
using EDA.Services.Identity.Models;
using EDA.Shared.DTOs;

namespace EDA.Services.Identity.Interfaces
{
    public interface IAccountService
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<Guid> SignUpAsync(SignUpUserDto userDto);
        Task<AuthenticationResult> SignInAsync(SignInUserDto userDto);
        Task SignOutAsync(Guid refreshToken);
        Task<AuthenticationResult> RefreshAsync(Guid refreshToken);
        Task DeleteAccountAsync(Guid userId);
    }
}
