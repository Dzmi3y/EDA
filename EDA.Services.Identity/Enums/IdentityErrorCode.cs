namespace EDA.Services.Identity.Enums
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
