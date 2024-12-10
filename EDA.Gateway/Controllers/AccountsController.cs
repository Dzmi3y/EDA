using EDA.Shared.Kafka.Producer;
using EDA.Shared.Redis.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EDA.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IKafkaProducer _producer;
        private readonly IRedisStringsService _redis;

        public AccountsController(IKafkaProducer producer, IRedisStringsService redis)
        {
            _producer = producer;
            _redis = redis;
        }

        [HttpPost]
        public IActionResult Login([FromBody] string username)
        {
            return Ok();
        }
    }
}
