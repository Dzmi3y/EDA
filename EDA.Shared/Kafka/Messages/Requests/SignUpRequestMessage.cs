using EDA.Shared.Authorization;

namespace EDA.Shared.Kafka.Messages.Requests
{
    public class SignUpRequestMessage : MessageBase, IUser
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
