namespace EDA.Shared.Kafka.Messages.Requests
{
    public class SignUpRequestMessage : MessageBase
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string EncryptedPassword { get; set; } = string.Empty;
    }
}
