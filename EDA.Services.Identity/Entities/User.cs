﻿using EDA.Services.Identity.Entities.Base;
using EDA.Shared.Authorization;
using System.ComponentModel.DataAnnotations;

namespace EDA.Services.Identity.Entities
{
    public class User : AuditableEntity, IUser
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

}
