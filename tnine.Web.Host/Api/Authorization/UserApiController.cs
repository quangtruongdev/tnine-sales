using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using tnine.Application.Shared.Authorization.IUserService;
using tnine.Application.Shared.Authorization.IUserService.Dto;
using tnine.Application.Shared.IRoleService;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/user")]
    [Authorize]
    public class UserApiController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IUserService _userService;
        private IRoleService _roleService;

        public UserApiController()
        {
        }

        public UserApiController(
            ApplicationSignInManager signInManager,
            ApplicationUserManager userManager,
            IUserService userService,
            IRoleService roleService
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
            _roleService = roleService;
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

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            var users = await UserManager.Users
                .Select(user => new
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                })
                .ToListAsync();

            return Ok(users);
        }


        // GET api/User/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetById(long id)
        {
            var roles = await UserManager.GetRolesAsync(id);
            var userResult = await UserManager.Users
                .Select(u => new
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Email = u.Email,
                    Roles = u.Roles.Select(role => role.RoleId)
                }).FirstOrDefaultAsync(u => u.Id == id);
            if (userResult == null)
            {
                return BadRequest("User not found");
            }

            return Ok(userResult);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateOrEdit([FromBody] EditUserDto input)
        {
            var user = await UserManager.FindByIdAsync(input.Id.Value);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            if (!string.IsNullOrEmpty(input.Email))
            {
                user.Email = input.Email;
                var result = await UserManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest(string.Join(", ", result.Errors));
                }
            }

            if (!string.IsNullOrEmpty(input.Password))
            {
                var result = await UserManager.RemovePasswordAsync(input.Id.Value);
                if (!result.Succeeded)
                {
                    return BadRequest(string.Join(", ", result.Errors));
                }
                result = await UserManager.AddPasswordAsync(input.Id.Value, input.Password);
                if (!result.Succeeded)
                {
                    return BadRequest(string.Join(", ", result.Errors));
                }
            }

            var currentRoles = await UserManager.GetRolesAsync(input.Id.Value);

            var rolesToRemove = currentRoles.Except(input.Roles.Select(r => r.ToString()));
            if (rolesToRemove.Any())
            {
                var result = await UserManager.RemoveFromRolesAsync(input.Id.Value, rolesToRemove.ToArray());
                if (!result.Succeeded)
                {
                    return BadRequest(string.Join(", ", result.Errors));
                }
            }

            var rolesToAdd = input.Roles.Select(r => r.ToString()).Except(currentRoles).ToList();
            if (rolesToAdd.Any())
            {
                var result = await UserManager.AddToRolesAsync(input.Id.Value, rolesToAdd.ToArray());
                if (!result.Succeeded)
                {
                    return BadRequest(string.Join(", ", result.Errors));
                }
            }

            return Ok("User updated successfully.");
        }
    }
}