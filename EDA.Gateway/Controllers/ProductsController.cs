using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages.Requests;
using EDA.Shared.Kafka.Producer;
using EDA.Shared.Redis.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EDA.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IKafkaProducer _producer;
        private readonly IRedisStringsService _redis;

        public ProductsController(IKafkaProducer producer, IRedisStringsService redis)
        {
            _producer = producer;
            _redis = redis;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int size, int startIndex)
        {
            try
            {
                var message = new ProductPageRequestMessage(size, startIndex);
                var key = message.ToKeyString();
                var value = message.ToString();

                (bool keyExists, string result) = await _redis.ReadAsync(key);

                if (keyExists)
                {
                    return Ok($"Successful {result}");
                }

                await _producer.SendMessageAsync(Topics.ProductPageRequest, key, value);

                result = await _redis.WaitForKeyAsync(key);

                return Ok($"Successful {result}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
