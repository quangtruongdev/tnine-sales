using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/user")]
    [Authorize]
    public class UserApiController : ApiController
    {
        private ApplicationUserManager _userManager;


        public UserApiController()
        {
        }

        public UserApiController(
            ApplicationUserManager userManager
            )
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetUserInfo")]
        public async Task<IHttpActionResult> GetUserInfo()
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                return BadRequest("User not found");
            }
            var role = await userManager.GetRolesAsync(user.Id);
            return Ok(new { user.UserName, role });
        }
    }
}