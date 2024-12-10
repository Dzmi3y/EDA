using EDA.Gateway.DTOs;
using EDA.Shared.Kafka.Producer;
using EDA.Shared.Redis.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EDA.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly IKafkaProducer _producer;
        private readonly IRedisStringsService _redis;

        public BasketsController(IKafkaProducer producer, IRedisStringsService redis)
        {
            _producer = producer;
            _redis = redis;
        }

        [HttpGet]
        public IActionResult GetBasket()
        {
            return Ok();
        }


        [HttpPut]
        public IActionResult UpdateBasket([FromBody] List<BasketItemDTO> items)
        {
            return Ok();
        }
    }
}
