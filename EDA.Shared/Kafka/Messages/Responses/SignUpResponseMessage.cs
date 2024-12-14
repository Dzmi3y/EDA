using System.Net;

namespace EDA.Shared.Kafka.Messages.Responses
{
    public class SignUpResponseMessage : MessageBase
    {
        public string UserId { get; set; } = string.Empty;
        public HttpStatusCode Status { get; set; }
        public string ExceptionMessage { get; set; } = string.Empty;
    }
}
