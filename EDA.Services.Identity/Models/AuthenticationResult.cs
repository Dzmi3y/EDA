﻿using EDA.Services.Identity.Enums;

namespace EDA.Services.Identity.Models
{
    public class AuthenticationResult
    {
        public string AccessToken { get; set; } = string.Empty;
        public Guid RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public IdentityErrorCode? Error { get; set; }
    }

}
