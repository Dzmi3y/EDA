using Confluent.Kafka;
using EDA.Services.Order.Entities;
using EDA.Shared.Exceptions;
using EDA.Shared.Kafka.Consumer;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages.Requests;
using EDA.Shared.Kafka.Messages.Responses;
using EDA.Shared.Kafka.Producer;
using Newtonsoft.Json;
using System.Net;

namespace EDA.Services.Order.EventHandlers
{

    public class OrderEventHandler : KafkaConsumerBase
    {
        private readonly ILogger<OrderEventHandler> _logger;
        private readonly IKafkaProducer _producer;
        private readonly AppDbContext _db;

        public OrderEventHandler(KafkaConsumerBaseConfig config,
            ILogger<OrderEventHandler> logger, IKafkaProducer producer, AppDbContext db)
            : base(config, Topics.SignUpRequest, logger)
        {
            _logger = logger;
            _producer = producer;
            _db = db;
        }

        protected override async Task HandleAsync(ConsumeResult<string, string> result)
        {
            var responseMessage = new ResponseMessage<string>();
            try
            {
                var message = JsonConvert.DeserializeObject<OrderPageRequestMessage>(result.Message.Value);

                if (message == null)
                {
                    _logger.LogError("OrderPageRequestMessage is null");
                    return;
                }

                Guid.TryParse(message.OrderId, out Guid orderId);
                Guid.TryParse(message.UserId, out Guid userId);
                var totalPrice = message.CartItemList.Sum(ci => ci.Count * ci.Price);
                var cartItem = message.CartItemList.Select(ci => new CartItem
                {
                    Id = Guid.NewGuid(),
                    Count = ci.Count,
                    ProductId = Guid.Parse(ci.Id),
                    OrderId = orderId,

                }).ToList();

                var newOrder = new Entities.Order()
                {
                    Id = orderId,
                    UserId = userId,
                    Price = totalPrice,
                    Cart = cartItem
                };

                await _db.Orders.AddAsync(newOrder);
                await _db.SaveChangesAsync();


                _logger.LogInformation($"Order {orderId} created successfully");

                responseMessage.Status = HttpStatusCode.Created;
                responseMessage.Payload = $"Order {orderId} created successfully";
            }
            catch (UserException userEx)
            {
                responseMessage.ExceptionMessage = userEx.Message;
                responseMessage.Status = HttpStatusCode.BadRequest;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                responseMessage.ExceptionMessage = "Internal server error";
                responseMessage.Status = HttpStatusCode.InternalServerError;
            }
            finally
            {
                await _producer.SendMessageAsync(Topics.SignUpResponse,
                    result.Message.Key, responseMessage.ToString());
            }
        }
    }
}
