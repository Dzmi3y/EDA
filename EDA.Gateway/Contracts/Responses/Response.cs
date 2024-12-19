namespace EDA.Gateway.Contracts.Responses
{
    public class Response<T>
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public T Payload { get; set; }
    }
}
