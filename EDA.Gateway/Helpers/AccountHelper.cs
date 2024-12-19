using EDA.Gateway.Contracts.Responses;
using EDA.Shared.Kafka.Messages.Responses;
using Newtonsoft.Json;
using System.Net;

namespace EDA.Gateway.Helpers
{
    public static class AccountHelper
    {
        public static (int statusCode, object? value) DeserializeResponse<T>(string response)
        {
            var result = new Response<T>();
            int status = (int)HttpStatusCode.InternalServerError;

            try
            {
                var res = JsonConvert.DeserializeObject<ResponseMessage<T>>(response);
                if (res == null)
                {
                    result.ErrorMessage = Resource.DeserializationFailedNull;
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
                result.ErrorMessage = Resource.DeserializationFailedResponse;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = Resource.DeserializationFailedUnexpected;
            }

            return (status, result);
        }

    }
}
