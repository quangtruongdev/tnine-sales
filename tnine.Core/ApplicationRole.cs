using Microsoft.AspNet.Identity.EntityFramework;

namespace tnine.Core
{
    public class ApplicationRole : IdentityRole<long, ApplicationUserRole>
    {
        public long RoleId => this.Id;
        public string RoleName => this.Name;
    }
}
