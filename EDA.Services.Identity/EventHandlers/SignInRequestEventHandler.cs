using Confluent.Kafka;
using EDA.Services.Identity.DTOs;
using EDA.Services.Identity.Interfaces;
using EDA.Shared.Authorization;
using EDA.Shared.Authorization.Settings;
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
    public class SignInRequestEventHandler : KafkaConsumerBase
    {
        private readonly ILogger<SignUpRequestEventHandler> _logger;
        private readonly IKafkaProducer _producer;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly PasswordEncryptionConfig _passwordEncryptionConfig;
        public SignInRequestEventHandler(KafkaConsumerBaseConfig config,
            ILogger<SignUpRequestEventHandler> logger, IServiceScopeFactory scopeFactory,
            IKafkaProducer producer, PasswordEncryptionConfig passwordEncryptionConfig)
            : base(config, Topics.SignInRequest, logger)
        {
            _logger = logger;
            _producer = producer;
            _scopeFactory = scopeFactory;
            _passwordEncryptionConfig = passwordEncryptionConfig;
        }

        protected override async Task HandleAsync(ConsumeResult<string, string> result)
        {
            var responseMessage = new ResponseMessage<SignInResponsePayload>();
            var encryptionKey = _passwordEncryptionConfig.Key;
            try
            {
                var message = JsonConvert.DeserializeObject<SignInRequestMessage>(result.Message.Value);

                if (message == null)
                {
                    _logger.LogError("SignInRequestMessage is null");
                    return;
                }

                using var scope = _scopeFactory.CreateScope();

                var accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();
                var authenticationResult = await accountService.SignInAsync(new SignInUserDto()
                {
                    Email = message.Email,
                    Password = EncryptionHelper.Decrypt(message.EncryptedPassword, encryptionKey)
                });

                responseMessage.Status = HttpStatusCode.Created;
                responseMessage.Payload = new SignInResponsePayload()
                {
                    AccessToken = authenticationResult.AccessToken,
                    ExpiresIn = authenticationResult.ExpiresIn,
                    RefreshToken = authenticationResult.RefreshToken
                };
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
                await _producer.SendMessageAsync(Topics.SignInResponse,
                    result.Message.Key, responseMessage.ToString());
            }
        }
    }
}
