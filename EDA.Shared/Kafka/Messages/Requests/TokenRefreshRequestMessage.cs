namespace EDA.Shared.Kafka.Messages.Requests
{
    public class TokenRefreshRequestMessage : MessageBase
    {
        public string RefreshToken { get; set; }
    }
}
