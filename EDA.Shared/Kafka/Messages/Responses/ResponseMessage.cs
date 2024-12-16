using System.Net;

namespace EDA.Shared.Kafka.Messages.Responses
{
    public class ResponseMessage<T> : MessageBase
    {
        public T Payload { get; set; }
        public HttpStatusCode Status { get; set; }
        public string ExceptionMessage { get; set; } = string.Empty;
    }
}
