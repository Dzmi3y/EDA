namespace EDA.Shared.Kafka.Messages.Requests
{
    public class DeleteAccountRequestMessage : MessageBase
    {
        public string UserId { get; set; }
    }
}
