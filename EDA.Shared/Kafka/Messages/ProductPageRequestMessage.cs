using Newtonsoft.Json;

namespace EDA.Shared.Kafka.Messages
{
    public class ProductPageRequestMessage 
    {
        public ProductPageRequestMessage(int pageSize, int pageNumber = 1)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        } 

        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public string ToKeyString()
        {
            return $"PS{PageSize}PN{PageNumber}";
        }
    }
}
