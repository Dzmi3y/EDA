namespace EDA.Shared.Kafka.Messages.Requests
{
    public class SignInRequestMessage : MessageBase
    {
        public string Email { get; set; } = string.Empty;
        public string EncryptedPassword { get; set; } = string.Empty;
    }
}
