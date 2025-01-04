using EDA.Gateway.Contracts.Requests;
using EDA.Gateway.Contracts.Responses;
using EDA.Shared.Authorization;
using EDA.Shared.Authorization.Settings;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages.Requests;
using EDA.Shared.Kafka.Messages.Responses.ResponsePayloads;
using EDA.Shared.Kafka.Producer;
using EDA.Shared.Redis.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace EDA.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : EDAControllerBase
    {
        private readonly PasswordEncryptionConfig _passwordEncryptionConfig;

        public AccountsController(IKafkaProducer producer, IRedisStringsService redis,
            PasswordEncryptionConfig passwordEncryptionConfig) : base(producer, redis)
        {
            _passwordEncryptionConfig = passwordEncryptionConfig;
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

        private (string key, string message) CreateSignUpMessage(SignUpRequest request)
        {
            string encryptionKey = _passwordEncryptionConfig.Key;
            var encryptedPassword = EncryptionHelper.Encrypt(request.Password, encryptionKey);
            var key = Guid.NewGuid().ToString();

            var signUpRequestMessage = new SignUpRequestMessage
            {
                Email = request.Email,
                Name = request.Name,
                EncryptedPassword = encryptedPassword
            };

            string message = signUpRequestMessage.ToString();
            return (key, message);
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

        private (string key, string message) CreateSignInMessage(SignInRequest request)
        {
            string encryptionKey = _passwordEncryptionConfig.Key;
            var encryptedPassword = EncryptionHelper.Encrypt(request.Password, encryptionKey);
            var key = Guid.NewGuid().ToString();

            var signInRequestMessage = new SignInRequestMessage
            {
                Email = request.Email,
                EncryptedPassword = encryptedPassword
            };

            string message = signInRequestMessage.ToString();
            return (key, message);
        }

        [HttpPost("token_refresh")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Response<TokenRefreshResponsePayload>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(Response<TokenRefreshResponsePayload>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(Response<TokenRefreshResponsePayload>))]
        public async Task<IActionResult> TokenRefresh([FromBody] TokenRefreshRequest request)
        {
            try
            {
                (string key, string tokenRefreshRequestMessage) = CreateTokenRefreshMessage(request);
                return await GetResponse<TokenRefreshResponsePayload>(key, tokenRefreshRequestMessage, Topics.TokenRefreshRequest);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response<TokenRefreshResponsePayload>
                {
                    ErrorMessage = Resource.ServerError
                });
            }
        }

        private (string key, string message) CreateTokenRefreshMessage(TokenRefreshRequest request)
        {
            var key = Guid.NewGuid().ToString();

            var tokenRefreshRequestMessage = new TokenRefreshRequestMessage
            {
                RefreshToken = request.RefreshToken,
            };

            string message = tokenRefreshRequestMessage.ToString();
            return (key, message);
        }

        [HttpPost("signout")]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.NoContent, Type = typeof(Response<object>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(Response<object>))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(Response<object>))]
        public async Task<IActionResult> SignOut([FromBody] SignOutRequest request)
        {
            try
            {
                (string key, string signOutRequestMessage) = CreateSignOutMessage(request);
                return await GetResponse<object>(key, signOutRequestMessage, Topics.SignOutRequest);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response<object>
                {
                    ErrorMessage = Resource.ServerError
                });
            }
        }

        private (string key, string message) CreateSignOutMessage(SignOutRequest request)
        {
            var key = Guid.NewGuid().ToString();

            var signOutRequestMessage = new SignOutRequestMessage
            {
                RefreshToken = request.RefreshToken
            };

            string message = signOutRequestMessage.ToString();
            return (key, message);
        }

        [HttpDelete]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.NoContent, Type = typeof(Response<object>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(Response<object>))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(Response<object>))]
        public async Task<IActionResult> Delete()
        {
            try
            {
                var userIdClaim = User.FindFirst(Claims.Id);
                if (userIdClaim == null)
                {
                    return StatusCode((int)HttpStatusCode.Unauthorized);
                }

                string userid = userIdClaim.Value;
                (string key, string deleteAccountMessage) = CreateDeleteAccountMessage(userid);
                return await GetResponse<object>(key, deleteAccountMessage, Topics.DeleteAccountRequest);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response<object>
                {
                    ErrorMessage = Resource.ServerError
                });
            }
        }

        private (string key, string message) CreateDeleteAccountMessage(string userId)
        {
            var key = Guid.NewGuid().ToString();

            var signOutRequestMessage = new DeleteAccountRequestMessage
            {

                UserId = userId
            };

            string message = signOutRequestMessage.ToString();
            return (key, message);
        }
    }
}
