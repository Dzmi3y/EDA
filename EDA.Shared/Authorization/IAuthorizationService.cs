namespace EDA.Shared.Authorization
{
    public interface IAuthorizationService
    {
        bool IsAuthorized(string token, string secretKey, out AuthUserInfo? authUserInfo);
    }
}