namespace EDA.Shared.Kafka.Messages.Responses.ResponsePayloads
{
    public class TokenRefreshResponsePayload
    {
        public string AccessToken { get; set; } = string.Empty;
        public Guid RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
