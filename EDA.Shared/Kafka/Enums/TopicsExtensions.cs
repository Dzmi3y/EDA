namespace EDA.Shared.Kafka.Enums
{
    public static class TopicsExtensions
    {
        public static string ToStringRepresentation(this Topics topic)
        {
            switch (topic)
            {
                case Topics.Products:
                    return "products";
                case Topics.Orders:
                    return "orders";
                case Topics.Users:
                    return "users";
                default:
                    throw new ArgumentOutOfRangeException(nameof(topic), topic, null);
            }
        }
    }

}
