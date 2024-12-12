namespace EDA.Service.Identity.Enums
{
    public enum IdentityErrorCode
    {
        RefreshTokenNotExists,
        RefreshTokenExpired,
        RefreshTokenInvalidated,
        RefreshTokenUsed,
        NoAssociatedUser
    }
}
