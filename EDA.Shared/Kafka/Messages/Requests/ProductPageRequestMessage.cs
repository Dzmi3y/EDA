namespace EDA.Shared.Kafka.Messages.Requests
{
    public class ProductPageRequestMessage : MessageBase
    {
        public ProductPageRequestMessage(int pageSize, int pageNumber = 1)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public string ToKeyString()
        {
            return $"PS{PageSize}PN{PageNumber}";
        }
    }
}
