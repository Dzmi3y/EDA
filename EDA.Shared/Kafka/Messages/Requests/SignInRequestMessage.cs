namespace EDA.Shared.Kafka.Messages.Requests
{
    public class SignInRequestMessage : MessageBase
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
