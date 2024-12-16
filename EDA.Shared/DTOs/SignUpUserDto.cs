using EDA.Shared.Authorization;

namespace EDA.Shared.DTOs
{
    public class SignUpUserDto : IUser
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
