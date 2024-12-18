using EDA.Gateway.Contracts.Requests;
using EDA.Gateway.Contracts.Responses;
using EDA.Gateway.Helpers;
using EDA.Shared.Authorization;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages.Requests;
using EDA.Shared.Kafka.Messages.Responses.ResponsePayloads;
using EDA.Shared.Kafka.Producer;
using EDA.Shared.Redis.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace EDA.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IKafkaProducer _producer;
        private readonly IRedisStringsService _redis;
        private readonly IPasswordHasher<IUser> _passwordHasher;

        public AccountsController(IKafkaProducer producer, IRedisStringsService redis,
            IPasswordHasher<IUser> passwordHasher)
        {
            _producer = producer;
            _redis = redis;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("signup")]
        [SwaggerResponse((int)HttpStatusCode.Created, Type = typeof(UiResponse<SignUpResponsePayload>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(UiResponse<SignUpResponsePayload>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(UiResponse<SignUpResponsePayload>))]
        public async Task<IActionResult> SignUp([FromBody] UiSignUpRequest request)
        {
            try
            {
                int statusCodeResult = (int)(HttpStatusCode.OK);
                object? resultValue = null;

                if (IsSignUpDataInvalid(request, out var badRequest))
                {
                    return badRequest;
                }

                (string key, string signUpRequestMessage) = CreateSignUpMessage(request);


                (bool keyExists, string redisResponse) = await _redis.ReadAsync(key);

                if (keyExists)
                {
                    (statusCodeResult, resultValue) = AccountHelper.DeserializeResponse<SignUpResponsePayload>(redisResponse);

                    return StatusCode(statusCodeResult, resultValue);
                }

                await _producer.SendMessageAsync(Topics.SignUpRequest, key, signUpRequestMessage);

                redisResponse = await _redis.WaitForKeyAsync(key, true);
                (statusCodeResult, resultValue) = AccountHelper.DeserializeResponse<SignUpResponsePayload>(redisResponse);

                return StatusCode(statusCodeResult, resultValue);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new UiResponse<SignUpResponsePayload>
                {
                    ErrorMessage = "Internal Server Error"
                });
            }
        }

        private (string key, string message) CreateSignUpMessage(UiSignUpRequest request)
        {
            var passwordHash = _passwordHasher.HashPassword(null, request.Password);
            var key = Guid.NewGuid().ToString();

            var signUpRequestMessage = new SignUpRequestMessage
            {
                Email = request.Email,
                Name = request.Name,
                PasswordHash = passwordHash
            };

            string message = signUpRequestMessage.ToString();
            return (key, message);
        }

        private bool IsSignUpDataInvalid(UiSignUpRequest request, out IActionResult? badRequest)
        {
            var isInValidData =
                string.IsNullOrEmpty(request.Name) ||
                string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrEmpty(request.Password);

            badRequest = null;

            if (!isInValidData)
            {
                return false;
            }

            badRequest = BadRequest(new UiResponse<object>()
            {
                ErrorMessage = "Fields cannot be empty"
            });
            return true;
        }


        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] string username)
        {
            return Ok();
        }
    }
}
