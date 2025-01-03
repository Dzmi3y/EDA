using EDA.Gateway.Contracts.Requests;
using EDA.Gateway.Contracts.Responses;
using EDA.Shared.Authorization.Settings;
using EDA.Shared.Authorization;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages.Requests;
using EDA.Shared.Kafka.Messages.Responses.ResponsePayloads;
using EDA.Shared.Kafka.Producer;
using EDA.Shared.Redis.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace EDA.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : EDAControllerBase
    {

        public ProductsController(IKafkaProducer producer, IRedisStringsService redis) : base(producer, redis)
        {

        }
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Response<ProductResponsePayload>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(Response<ProductResponsePayload>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(Response<ProductResponsePayload>))]

        public async Task<IActionResult> Get([FromQuery] int size, int startIndex)
        {
            try
            {
                var message = new ProductPageRequestMessage(size, startIndex);
                var key = message.ToKeyString();
                var value = message.ToString();

                return await GetResponse<ProductResponsePayload>(key, value, Topics.ProductPageRequest);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response<ProductResponsePayload>
                {
                    ErrorMessage = Resource.ServerError
                });
            }
        }
    }
}
