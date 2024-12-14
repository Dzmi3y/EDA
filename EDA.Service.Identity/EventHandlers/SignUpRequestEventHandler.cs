using Confluent.Kafka;
using EDA.Service.Identity.Interfaces;
using EDA.Service.Identity.Services;
using EDA.Shared.DTOs;
using EDA.Shared.Exceptions;
using EDA.Shared.Kafka.Consumer;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages.Requests;
using EDA.Shared.Kafka.Messages.Responses;
using EDA.Shared.Kafka.Producer;
using Newtonsoft.Json;
using System.Net;

namespace EDA.Service.Identity.EventHandlers
{
    public class SignUpRequestEventHandler : KafkaConsumerBase
    {
        private readonly ILogger<SignUpRequestEventHandler> _logger;
        private readonly IKafkaProducer _producer;
        private readonly IServiceScopeFactory _scopeFactory;
        public SignUpRequestEventHandler(KafkaConsumerBaseConfig config,
            ILogger<SignUpRequestEventHandler> logger, IServiceScopeFactory scopeFactory,
            IKafkaProducer producer)
            : base(config, Topics.SignUpRequest, logger) 
        {
            _logger = logger;
            _producer = producer;
            _scopeFactory = scopeFactory;
        }

        protected override async Task HandleAsync(ConsumeResult<string, string> result)
        {
            var responseMessage = new SignUpResponseMessage();
            try
            {
                var message = JsonConvert.DeserializeObject<SignUpRequestMessage>(result.Message.Value);

                if (message == null)
                {
                    _logger.LogError("SignUpRequestMessage is null");
                    return;
                }

                using var scope = _scopeFactory.CreateScope();

                var accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();
                var userId = await accountService.SignUpAsync(new SignUpUserDto()
                {
                    Email = message.Email,
                    Name = message.Name,
                    PasswordHash = message.PasswordHash
                });


                var stringUserId = userId.ToString();

                _logger.LogInformation($"User {stringUserId}  has been successfully registered");

                responseMessage.Status = HttpStatusCode.Created;
                responseMessage.UserId = stringUserId;


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