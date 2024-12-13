using Confluent.Kafka;
using EDA.Service.Identity.Interfaces;
using EDA.Service.Identity.Services;
using EDA.Shared.DTOs;
using EDA.Shared.Kafka.Consumer;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages;
using EDA.Shared.Kafka.Producer;
using Newtonsoft.Json;

namespace EDA.Service.Identity.EventHandlers
{
    public class SignUpRequestEventHandler : KafkaConsumerBase
    {
        private readonly ILogger<SignUpRequestEventHandler> _logger;
        private readonly IAccountService _accountService;
        private readonly IKafkaProducer _producer;
        public SignUpRequestEventHandler(KafkaConsumerBaseConfig config,
            ILogger<SignUpRequestEventHandler> logger,IAccountService accountService,
            IKafkaProducer producer)
            : base(config, Topics.SignInRequest, logger)
        {
            _logger = logger;
            _accountService = accountService;
            _producer = producer;
        }

        protected override async Task HandleAsync(ConsumeResult<string, string> result)
        {
            try
            {
                var message = JsonConvert.DeserializeObject<SignUpRequestMessage>(result.Message.Value);

                if (message == null)
                {
                    _logger.LogError("SignUpRequestMessage is null");
                    return;
                }

                var userId = await _accountService.SignUpAsync(new SignUpUserDto()
                {
                    Email = message.Email, 
                    Name = message.Name, 
                    PasswordHash = message .PasswordHash
                });

                var stringUserId = userId.ToString();

                _logger.LogInformation($"User {stringUserId}  has been successfully registered");

                await _producer.SendMessageAsync(Topics.SignUpResponse,
                    result.Message.Key, stringUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}