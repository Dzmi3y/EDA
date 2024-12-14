using EDA.Gateway.Contracts.Requests;
using EDA.Gateway.Contracts.Responses;
using EDA.Shared.Authorization;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages.Requests;
using EDA.Shared.Kafka.Messages.Responses;
using EDA.Shared.Kafka.Producer;
using EDA.Shared.Redis.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        [SwaggerResponse((int)HttpStatusCode.Created, Type = typeof(UiSignUpResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(UiSignUpResponse))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(UiSignUpResponse))]
        public async Task<IActionResult> SignUp([FromBody] UiSignUpRequest request)
        {
            try
            {
                var isInValidData = 
                    string.IsNullOrEmpty(request.Name) ||
                    string.IsNullOrEmpty(request.Email) ||
                    string.IsNullOrEmpty(request.Password);

                if (isInValidData)
                {
                    return BadRequest(new UiSignUpResponse()
                    {
                        ErrorMessage = "Fields cannot be empty"
                    });
                }

                var passwordHash = _passwordHasher.HashPassword(null, request.Password);
                var key = Guid.NewGuid().ToString();

                var signUpRequestMessage = new SignUpRequestMessage
                {
                    Email = request.Email,
                    Name = request.Name,
                    PasswordHash = passwordHash
                };

                var value = signUpRequestMessage.ToString();


                (bool keyExists, string result) = await _redis.ReadAsync(key);

                if (keyExists)
                {
                    return GetResult(result);
                }

                await _producer.SendMessageAsync(Topics.SignUpRequest, key, value);

                result = await _redis.WaitForKeyAsync(key, true);

                return GetResult(result);
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, new UiSignUpResponse
                {
                    ErrorMessage = "Internal Server Error"
                });
            }
        }

        [NonAction]
        private IActionResult GetResult(string response)
        {
            var result = new UiSignUpResponse();
            int status = (int)HttpStatusCode.InternalServerError;

            try
            {
                var res = JsonConvert.DeserializeObject<SignUpResponseMessage>(response);
                if (res == null)
                {
                    result.ErrorMessage = "Failed to deserialize response: returned null.";
                }
                else
                {
                    status = (int)res.Status;
                    result.UserId = res.UserId;
                    result.ErrorMessage = res.ExceptionMessage;
                }
            }
            catch (JsonSerializationException ex)
            {
                result.ErrorMessage = "Error deserializing response.";
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "Unexpected error during deserialization.";
            }

            return StatusCode(status, result);
        }


        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] string username)
        {
            return Ok();
        }
    }
}
