using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages;
using EDA.Shared.Kafka.Producer;
using Microsoft.AspNetCore.Mvc;

namespace EDA.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private IKafkaProducer _producer;

        public ProductsController(IKafkaProducer producer)
        {
            _producer=producer;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
           await _producer.SendMessageAsync(
                Topics.Products,new ProductPageRequestMessage(8,2));
           


            return Ok("Successful");
        }
    }
}
