namespace EDA.Shared.Redis
{
    public class RedisConfig
    {
        public string Configuration { get; set; }
        public int DefaultExpiryMin { get; set; }
        public int DefaultTimeoutMin { get; set; }
    }
}
