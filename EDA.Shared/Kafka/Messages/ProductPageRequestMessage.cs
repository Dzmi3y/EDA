using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages.Base;

namespace EDA.Shared.Kafka.Messages
{
    public class ProductPageRequestMessage : MessageBase
    {
        public ProductPageRequestMessage(int pageSize, int startIndex = 0)
            :base(EventTypes.ProductPageRequest)
        {
            PageSize = pageSize;
            StartIndex = startIndex;
        }

        public int PageSize { get; set; }
        public int StartIndex { get; set; }
    }
}
