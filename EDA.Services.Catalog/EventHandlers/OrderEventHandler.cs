using Confluent.Kafka;
using EDA.Services.Catalog.Repositories;
using EDA.Shared.Exceptions;
using EDA.Shared.Kafka.Consumer;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages.Requests;
using EDA.Shared.Kafka.Messages.Responses;
using EDA.Shared.Kafka.Producer;
using Newtonsoft.Json;
using System.Net;

namespace EDA.Services.Catalog.EventHandlers
{
    public class OrderEventHandler : KafkaConsumerBase
    {
        private readonly ILogger<OrderEventHandler> _logger;
        private readonly IKafkaProducer _producer;
        private readonly IServiceProvider _serviceProvider;
        private readonly IProductRepository _repository;

        public OrderEventHandler(KafkaConsumerBaseConfig config,
            ILogger<OrderEventHandler> logger, IKafkaProducer producer,
            IServiceProvider serviceProvider, IProductRepository repository)
            : base(config, Topics.OrderRequest, logger)
        {
            _logger = logger;
            _producer = producer;
            _serviceProvider = serviceProvider;
            _repository = repository;
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

                var cartItems = message.CartItemList.Select(ci =>
                    (ProductId: Guid.Parse(ci.Id), Count: ci.Count))
                    .ToList();

                foreach (var cartItem in cartItems)
                {
                    var product = await _repository.GetByIdAsync(cartItem.ProductId.ToString());
                    product.Count = Math.Max(product.Count - cartItem.Count, 0);

                    await _repository.UpdateAsync(product);
                }

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
                await _producer.SendMessageAsync(Topics.OrderResponse,
                    result.Message.Key, responseMessage.ToString());
            }
        }
    }
}
