using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace APP.DOMAIN.Identity
{
    public class Role: IdentityRole<string>
    {
        public List<UserRole> UserRoles{get; set; }
    }
}