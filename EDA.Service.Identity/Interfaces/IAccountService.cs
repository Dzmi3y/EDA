using EDA.Service.Identity.Entities;
using EDA.Service.Identity.Models;
using EDA.Shared.DTOs;

namespace EDA.Service.Identity.Interfaces
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
