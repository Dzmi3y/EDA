using EDA.Gateway.Contracts.Requests;
using EDA.Shared.Authorization;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages;
using EDA.Shared.Kafka.Producer;
using EDA.Shared.Redis.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

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

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] UiSignUpRequest request)
        {
            try
            {
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
                    return Ok($"Successful {result}");
                }

                await _producer.SendMessageAsync(Topics.SignUpRequest, key, value);

                result = await _redis.WaitForKeyAsync(key); //todo: set timeouts

                return Ok($"Successful {result}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Login([FromBody] string username)
        {
            return Ok();
        }
    }
}
