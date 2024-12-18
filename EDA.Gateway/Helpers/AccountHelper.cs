using EDA.Gateway.Contracts.Responses;
using Newtonsoft.Json;
using System.Net;
using EDA.Shared.Kafka.Messages.Responses;

namespace EDA.Gateway.Helpers
{
    public static class AccountHelper
    {
        public static (int statusCode, object? value) DeserializeResponse<T>(string response)
        {
            var result = new UiResponse<T>();
            int status = (int)HttpStatusCode.InternalServerError;

            try
            {
                var res = JsonConvert.DeserializeObject<ResponseMessage<T>>(response);
                if (res == null)
                {
                    result.ErrorMessage = "Failed to deserialize response: returned null.";
                }
                else
                {
                    status = (int)res.Status;
                    result.Payload = res.Payload;
                    result.ErrorMessage = res.ExceptionMessage;
                }
            }
            catch (JsonSerializationException ex)
            {
                result.ErrorMessage = "Error deserializing response.";
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "Unexpected error during deserialization.";
            }

            return (status, result);
        }

    }
}
