using EDA.Services.Identity.DTOs;
using EDA.Services.Identity.Entities;
using EDA.Services.Identity.Models;

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
