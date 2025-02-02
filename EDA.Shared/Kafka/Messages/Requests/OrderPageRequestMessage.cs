using EDA.Shared.Data;

namespace EDA.Shared.Kafka.Messages.Requests
{
    public class OrderPageRequestMessage : MessageBase
    {
        public string UserId { get; set; }
        public string OrderId { get; set; }

        public List<CartItem> CartItemList { get; set; }
    }
}
