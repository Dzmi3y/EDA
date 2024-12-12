using System.ComponentModel.DataAnnotations;
using EDA.Service.Identity.Entities.Base;


namespace EDA.Service.Identity.Entities
{
    public class RefreshToken : AuditableEntity
    {
        [Key]
        public Guid Token { get; set; }

        public string JwtId { get; set; } = string.Empty;
        public DateTime ExpiryDateUtc { get; set; }
        public bool Used { get; set; }
        public bool Invalidated { get; set; }

        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
    }

}
