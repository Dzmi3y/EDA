using Confluent.Kafka;
using EDA.Services.Identity.Interfaces;
using EDA.Shared.Exceptions;
using EDA.Shared.Kafka.Consumer;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages.Requests;
using EDA.Shared.Kafka.Messages.Responses;
using EDA.Shared.Kafka.Producer;
using Newtonsoft.Json;
using System.Net;

namespace EDA.Services.Identity.EventHandlers
{
    public class DeleteAccountRequestEventHandler : KafkaConsumerBase
    {
        private readonly ILogger<DeleteAccountRequestEventHandler> _logger;
        private readonly IKafkaProducer _producer;
        private readonly IServiceScopeFactory _scopeFactory;

        public DeleteAccountRequestEventHandler(KafkaConsumerBaseConfig config,
            ILogger<DeleteAccountRequestEventHandler> logger, IServiceScopeFactory scopeFactory,
            IKafkaProducer producer)
            : base(config, Topics.DeleteAccountRequest, logger)
        {
            _logger = logger;
            _producer = producer;
            _scopeFactory = scopeFactory;
        }

        protected override async Task HandleAsync(ConsumeResult<string, string> result)
        {
            var responseMessage = new ResponseMessage<object>();
            try
            {
                var message = JsonConvert.DeserializeObject<DeleteAccountRequestMessage>(result.Message.Value);

                if (message == null)
                {
                    _logger.LogError("DeleteAccountRequestMessage is null");
                    return;
                }

                var isRefreshTokenValid = Guid.TryParse(message.UserId, out Guid userId);

                if (!isRefreshTokenValid)
                {
                    _logger.LogError($"Invalid UserId received: {message.UserId}");
                    responseMessage.ExceptionMessage = "Invalid UserId";
                    responseMessage.Status = HttpStatusCode.BadRequest;

                    await _producer.SendMessageAsync(Topics.DeleteAccountResponse,
                        result.Message.Key, responseMessage.ToString());
                    return;
                }

                using var scope = _scopeFactory.CreateScope();

                var accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();
                await accountService.DeleteAccountAsync(userId);

                responseMessage.Status = HttpStatusCode.NoContent;
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
                await _producer.SendMessageAsync(Topics.DeleteAccountResponse,
                    result.Message.Key, responseMessage.ToString());
            }

        }
    }
}