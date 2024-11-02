using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using tnine.Core;

namespace tnine.Web.Host.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser, long> _userManager;

        public UserController()
        {
        }

        public UserController(UserManager<ApplicationUser, long> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ActionResult> GetUserWithRole()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<ApplicationUser, long>>();
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                return Json(new { error = "User not found" }, JsonRequestBehavior.AllowGet);
            }

            var role = await userManager.GetRolesAsync(user.Id);
            return Json(new { user.UserName, role }, JsonRequestBehavior.AllowGet);
        }

    }
}