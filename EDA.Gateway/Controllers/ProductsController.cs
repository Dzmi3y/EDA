using Confluent.Kafka;
using EDA.Shared.Kafka.Consumer;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages;
using EDA.Shared.Kafka.Producer;
using EDA.Shared.Redis.Interfaces;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace EDA.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IKafkaProducer _producer;
        private readonly IKafkaConsumer _consumer;
        private readonly IRedisService _redis;

        public ProductsController(IKafkaProducer producer, IKafkaConsumer consumer, IRedisService redis)
        {
            _producer = producer;
            _consumer = consumer;
            _redis = redis;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var message = new ProductPageRequestMessage(8, 2);
                string key = message.ToString();

                
                await _producer.SendMessageAsync(Topics.Products, key, message);

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
