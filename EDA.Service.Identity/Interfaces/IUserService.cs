using EDA.Service.Identity.Entities;
using EDA.Service.Identity.Models;
using EDA.Shared.DTOs;

namespace EDA.Service.Identity.Interfaces
{
    public interface IUserService
    {
        Task<Guid> SignUp(SignUpUserDto userDto);
        Task<AuthenticationResult> SignIn(SignInUserDto userDto);

    }
}
