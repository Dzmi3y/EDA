using EDA.Services.Order.Entities.Base;

namespace EDA.Services.Order.Entities
{
    public class CartItem : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
