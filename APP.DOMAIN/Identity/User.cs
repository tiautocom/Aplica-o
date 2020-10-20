
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.DOMAIN.Identity
{
    public class User: IdentityUser<string>
    {
        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }
        public string  CelWApp { get; set; }

        public List<UserRole> UserRoles{get; set; }
    }
}