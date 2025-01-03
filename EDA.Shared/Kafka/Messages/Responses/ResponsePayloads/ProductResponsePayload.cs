using EDA.Shared.Data;

namespace EDA.Shared.Kafka.Messages.Responses.ResponsePayloads
{
    public class ProductResponsePayload
    {
        public IEnumerable<Product> Products { get; set; }
    }
}
