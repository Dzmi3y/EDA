namespace EDA.Gateway.Contracts.Responses
{
    public class UiResponse<T>
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public T Payload { get; set; }
    }
}
