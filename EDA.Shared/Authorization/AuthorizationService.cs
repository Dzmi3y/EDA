using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EDA.Shared.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ILogger<AuthorizationService> _logger;

        public AuthorizationService( ILogger<AuthorizationService> logger)
        {
            _logger = logger;
        }

        private AuthUserInfo? GetUserInfo(JwtSecurityToken jwtToken)
        {
            var userId = jwtToken.Claims.First(x => x.Type == Claims.Id).Value;

            if (string.IsNullOrEmpty(userId))
                return null;

            return new AuthUserInfo()
            {
                Id = userId,
                Email = jwtToken.Claims.First(x => x.Type == Claims.Email).Value,
                Name = jwtToken.Claims.First(x => x.Type == Claims.Name).Value
            };
        }

        public bool IsAuthorized(string token, string secretKey, out AuthUserInfo? authUserInfo)
        {
            authUserInfo = null;

            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                authUserInfo = GetUserInfo(jwtToken);

                return authUserInfo!=null;
            }
            catch
            {
                _logger.LogError($"Token {token} is invalid");
                return false;
            }
        }
    }
}