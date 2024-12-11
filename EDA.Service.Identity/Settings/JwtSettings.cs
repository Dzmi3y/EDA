namespace EDA.Service.Identity.Settings
{
    public class JwtSettings
    {
        public string Secret { get; set; } = String.Empty;
        public TimeSpan AccessTokenLifetime { get; set; }
        public TimeSpan RefreshTokenLifetime { get; set; }
    }
}
