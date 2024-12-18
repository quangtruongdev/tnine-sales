using Microsoft.AspNet.Identity.EntityFramework;

namespace tnine.Core
{
    public class ApplicationRole : IdentityRole<long, ApplicationUserRole>
    {
        public long RoleId => Id;
        public string RoleName => Name;
    }
}
