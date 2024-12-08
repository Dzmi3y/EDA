namespace EDA.Shared.Kafka.Enums
{
    public static class TopicsExtensions
    {
        public static string ToStringRepresentation(this Topics topic)
        {
            switch (topic)
            {
                case Topics.ProductPageResponse:
                    return "ProductPageResponse";
                case Topics.ProductPageRequest:
                    return "ProductPageRequest";
                default:
                    throw new ArgumentOutOfRangeException(nameof(topic), topic, null);
            }
        }
    }

}
