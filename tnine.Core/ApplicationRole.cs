using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tnine.Core
{
    public class ApplicationRole : IdentityRole<long, ApplicationUserRole>
    {
        public long RoleId => Id;
        public string RoleName => Name;
        [StringLength(256)]
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
