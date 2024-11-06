using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using tnine.Application.Shared.Authorization.IAccountService.Dto;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/user")]
    [Authorize]
    public class UserApiController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public UserApiController()
        {
        }

        public UserApiController(
            ApplicationSignInManager signInManager,
            ApplicationUserManager userManager
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set
            {
                _userManager = value;
            }
        }

        // POST api/User/CreateOrEditRoleForUser
        [Route("CreateOrEditRoleForUser")]
        public async Task<IHttpActionResult> CreateOrEditRoleForUser(CreateOrEditRoleForUserDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await UserManager.FindByIdAsync(input.UserId);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);
            var rolesToAdd = input.RoleIds.Except(userRoles.Select(long.Parse));
            var rolesToRemove = userRoles.Except(input.RoleIds.Select(id => id.ToString()));

            foreach (var role in rolesToAdd)
            {
                var roleResult = await UserManager.AddToRoleAsync(user.Id, role.ToString());
                if (!roleResult.Succeeded)
                {
                    return BadRequest(string.Join(", ", roleResult.Errors));
                }
            }

            foreach (var role in rolesToRemove)
            {
                var roleResult = await UserManager.RemoveFromRoleAsync(user.Id, role);
                if (!roleResult.Succeeded)
                {
                    return BadRequest(string.Join(", ", roleResult.Errors));
                }
            }

            return Ok();
        }
    }
}