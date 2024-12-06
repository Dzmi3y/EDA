using EDA.Shared.Kafka.Consumer;
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
        private readonly IKafkaProducer _producer;
        private readonly IKafkaConsumer _consumer;

        public ProductsController(IKafkaProducer producer, IKafkaConsumer consumer)
        {
            _producer = producer;
            _consumer = consumer;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                //var key = Guid.NewGuid();
                
                Guid.TryParse("f99ad5f7-6bbd-409a-9fc7-f2469e7b69ba", out Guid key);
                
                await _producer.SendMessageAsync(
                    Topics.Products, key, new ProductPageRequestMessage(8, 2));
                //var cts = new CancellationTokenSource();

                //var result = await _consumer.StartConsuming(cts.Token, Topics.Products, key);


                return Ok($"Successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
