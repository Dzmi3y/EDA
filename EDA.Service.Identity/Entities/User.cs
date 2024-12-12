using System.ComponentModel.DataAnnotations;
using EDA.Service.Identity.Entities.Base;


namespace EDA.Service.Identity.Entities
{
    public class User : AuditableEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

}
