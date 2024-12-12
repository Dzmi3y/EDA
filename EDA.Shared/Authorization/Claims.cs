using System.IdentityModel.Tokens.Jwt;

namespace EDA.Shared.Authorization
{
    public static class Claims
    {
       public const string Id = JwtRegisteredClaimNames.Sub;
       public const string Name = JwtRegisteredClaimNames.Name;
       public const string Email = JwtRegisteredClaimNames.Email;
    }
}
