using EDA.Services.Order.Entities.Base;

namespace EDA.Services.Order.Entities
{
    public class Order : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Price { get; set; }

        public virtual IList<CartItem> Cart { get; set; }
    }
}
