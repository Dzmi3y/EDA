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
        [SwaggerResponse((int)HttpStatusCode.Created, Type = typeof(Response<SignUpResponsePayload>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(Response<SignUpResponsePayload>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(Response<SignUpResponsePayload>))]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            try
            {
                (string key, string signUpRequestMessage) = CreateSignUpMessage(request);
                return await GetResponse<SignUpResponsePayload>(key, signUpRequestMessage, Topics.SignUpRequest);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response<SignUpResponsePayload>
                {
                    ErrorMessage = Resource.ServerError
                });
            }
        }

        [HttpPost("signin")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Response<SignInResponsePayload>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(Response<SignInResponsePayload>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(Response<SignInResponsePayload>))]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            try
            {
                (string key, string signInRequestMessage) = CreateSignInMessage(request);
                return await GetResponse<SignInResponsePayload>(key, signInRequestMessage, Topics.SignInRequest);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response<SignInResponsePayload>
                {
                    ErrorMessage = Resource.ServerError
                });
            }
        }

        private (string key, string message) CreateSignUpMessage(SignUpRequest request)
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

        private (string key, string message) CreateSignInMessage(SignInRequest request)
        {
            var passwordHash = _passwordHasher.HashPassword(null, request.Password);
            var key = Guid.NewGuid().ToString();

            var signUpRequestMessage = new SignInRequestMessage
            {
                Email = request.Email,
                PasswordHash = passwordHash
            };

            string message = signUpRequestMessage.ToString();
            return (key, message);
        }

        private async Task<IActionResult> GetResponse<T>(string key, string signUpRequestMessage, Topics topic)
        {
            try
            {
                int statusCodeResult = (int)(HttpStatusCode.OK);
                object? resultValue = null;

                (bool keyExists, string redisResponse) = await _redis.ReadAsync(key);

                if (keyExists)
                {
                    (statusCodeResult, resultValue) = AccountHelper.DeserializeResponse<T>(redisResponse);

                    return StatusCode(statusCodeResult, resultValue);
                }

                await _producer.SendMessageAsync(topic, key, signUpRequestMessage);

                redisResponse = await _redis.WaitForKeyAsync(key, true);
                (statusCodeResult, resultValue) = AccountHelper.DeserializeResponse<T>(redisResponse);

                return StatusCode(statusCodeResult, resultValue);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response<T>
                {
                    ErrorMessage = Resource.ServerError
                });
            }
        }
    }
}
