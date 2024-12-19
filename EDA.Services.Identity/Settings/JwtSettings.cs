namespace EDA.Services.Identity.Settings
{
    public class JwtSettings
    {
        public string Secret { get; set; } = string.Empty;
        public TimeSpan AccessTokenLifetime { get; set; }
        public TimeSpan RefreshTokenLifetime { get; set; }
    }
}
