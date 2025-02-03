using EDA.Gateway.Contracts.Responses;
using EDA.Shared.Data;
using EDA.Shared.Kafka.Messages.Responses.ResponsePayloads;
using EDA.Shared.Kafka.Producer;
using EDA.Shared.Redis.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using EDA.Shared.Kafka.Enums;
using EDA.Gateway.Contracts.Requests;
using EDA.Shared.Kafka.Messages.Requests;
using EDA.Shared.Authorization.Settings;
using EDA.Shared.Authorization;

namespace EDA.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : EDAControllerBase
    {
        public OrdersController(IKafkaProducer producer, IRedisStringsService redis) : base(producer, redis)
        {

        }

        [HttpPost]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Response<string>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(Response<string>))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(Response<string>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(Response<string>))]
        public async Task<IActionResult> CreateOrder([FromBody] List<CartItem> cartItems)
        {
            try
            {
                var userIdClaim = User.FindFirst(Claims.Id);
                var key = Guid.NewGuid().ToString();
                var orderPageRequestMessage = new OrderPageRequestMessage
                {
                    UserId = userIdClaim.Value,
                    OrderId = key,
                    CartItemList = cartItems
                };
                return await GetResponse<string>(key, orderPageRequestMessage.ToString(), Topics.OrderRequest);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response<string>
                {
                    ErrorMessage = Resource.ServerError
                });
            }

        }
    }
}
