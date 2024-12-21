using Confluent.Kafka;
using EDA.Services.Identity.Interfaces;
using EDA.Shared.Exceptions;
using EDA.Shared.Kafka.Consumer;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages.Requests;
using EDA.Shared.Kafka.Messages.Responses;
using EDA.Shared.Kafka.Messages.Responses.ResponsePayloads;
using EDA.Shared.Kafka.Producer;
using Newtonsoft.Json;
using System.Net;

namespace EDA.Services.Identity.EventHandlers
{
    public class SignOutRequestEventHandler : KafkaConsumerBase
    {
        private readonly ILogger<SignOutRequestEventHandler> _logger;
        private readonly IKafkaProducer _producer;
        private readonly IServiceScopeFactory _scopeFactory;
        public SignOutRequestEventHandler(KafkaConsumerBaseConfig config,
            ILogger<SignOutRequestEventHandler> logger, IServiceScopeFactory scopeFactory,
            IKafkaProducer producer)
            : base(config, Topics.SignOutRequest, logger)
        {
            _logger = logger;
            _producer = producer;
            _scopeFactory = scopeFactory;
        }

        protected override async Task HandleAsync(ConsumeResult<string, string> result)
        {
            var responseMessage = new ResponseMessage<SignInResponsePayload>();
            try
            {
                var message = JsonConvert.DeserializeObject<SignOutRequestMessage>(result.Message.Value);

                if (message == null)
                {
                    _logger.LogError("TokenRefreshRequestMessage is null");
                    return;
                }

                var isRefreshTokenValid = Guid.TryParse(message.RefreshToken, out Guid refreshToken);

                if (!isRefreshTokenValid)
                {
                    _logger.LogError($"Invalid refresh token received: {message.RefreshToken}");
                    responseMessage.ExceptionMessage = "Invalid refresh token";
                    responseMessage.Status = HttpStatusCode.BadRequest;

                    await _producer.SendMessageAsync(Topics.SignOutResponse,
                       result.Message.Key, responseMessage.ToString());
                    return;
                }

                using var scope = _scopeFactory.CreateScope();

                var accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();
                await accountService.SignOutAsync(refreshToken);

                responseMessage.Status = HttpStatusCode.OK;
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
                await _producer.SendMessageAsync(Topics.SignOutResponse,
                    result.Message.Key, responseMessage.ToString());
            }
        }
    }
}
