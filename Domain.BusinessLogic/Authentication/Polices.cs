﻿using Microsoft.AspNetCore.Authorization;

namespace Domain.BusinessLogic.Authentication
{
    public class Polices
    {
        public const string Admin = "Admin";
        public const string User = "User";

        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }
        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();
        }
    }
}
