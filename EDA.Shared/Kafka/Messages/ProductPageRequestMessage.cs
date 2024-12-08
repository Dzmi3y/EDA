using Newtonsoft.Json;

namespace EDA.Shared.Kafka.Messages
{
    public class ProductPageRequestMessage 
    {
        public ProductPageRequestMessage(int pageSize, int startIndex = 0)
        {
            PageSize = pageSize;
            StartIndex = startIndex;
        } 

        public int PageSize { get; set; }
        public int StartIndex { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
            
        }

        public string ToKeyString()
        {
            return $"PS{PageSize}SI{StartIndex}";
        }
    }
}
