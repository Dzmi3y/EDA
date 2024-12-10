using EDA.Gateway.DTOs;
using EDA.Shared.Kafka.Producer;
using EDA.Shared.Redis.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EDA.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IKafkaProducer _producer;
        private readonly IRedisStringsService _redis;

        public OrdersController(IKafkaProducer producer, IRedisStringsService redis)
        {
            _producer = producer;
            _redis = redis;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] List<OrderItemDTO> orderList)
        {
            return Ok();
        }
    }
}
