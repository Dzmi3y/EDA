using EDA.Gateway.Contracts.Responses;
using EDA.Gateway.Helpers;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Producer;
using EDA.Shared.Redis.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EDA.Gateway.Controllers
{
    public class EDAControllerBase : ControllerBase
    {
        protected readonly IKafkaProducer _producer;
        protected readonly IRedisStringsService _redis;

        public EDAControllerBase(IKafkaProducer producer, IRedisStringsService redis)
        {
            _producer = producer;
            _redis = redis;
        }
        protected async Task<IActionResult> GetResponse<T>(string key, string requestMessage, Topics topic, bool deleteRedisMessage = true)
        {
            try
            {
                int statusCodeResult = (int)(HttpStatusCode.OK);
                object? resultValue = null;

                (bool keyExists, string redisResponse) = await _redis.ReadAsync(key);

                if (keyExists)
                {
                    (statusCodeResult, resultValue) = AccountHelper.DeserializeResponse<T>(redisResponse);

                    return StatusCode(statusCodeResult, resultValue);
                }

                await _producer.SendMessageAsync(topic, key, requestMessage);

                redisResponse = await _redis.WaitForKeyAsync(key, deleteRedisMessage);
                (statusCodeResult, resultValue) = AccountHelper.DeserializeResponse<T>(redisResponse);

                return StatusCode(statusCodeResult, resultValue);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response<T>
                {
                    ErrorMessage = Resource.ServerError
                });
            }
        }
    }
}
