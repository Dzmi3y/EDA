﻿using System.ComponentModel.DataAnnotations;
using EDA.Service.Identity.Entities.Base;
using EDA.Shared.Authorization;


namespace EDA.Service.Identity.Entities
{
    public class User :  AuditableEntity, IUser
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

}
