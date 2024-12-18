using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using tnine.Application.Shared.IRoleService;
using tnine.Core;
using tnine.Web.Host.Models;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/Account")]
    public class AccountApiController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IRoleService _roleService;

        public AccountApiController()
        {
        }

        public AccountApiController(
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            IRoleService roleService
            )
        {
            UserManager = userManager;
            SignInManager = signInManager;
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

        // POST api/Account/Login
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IHttpActionResult> Login(LoginViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await SignInManager.PasswordSignInAsync(input.Email, input.Password, input.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return Ok(new { Success = true, Message = "Login successful" });
                case SignInStatus.LockedOut:
                    return BadRequest("Locked out");
                case SignInStatus.RequiresVerification:
                    return BadRequest("Requires verification");
                case SignInStatus.Failure:
                default:
                    return BadRequest("Invalid login attempt");
            }
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await UserManager.FindByEmailAsync(input.Email);
            if (existingUser != null)
            {
                return BadRequest("User already exists");
            }

            var user = new ApplicationUser { UserName = input.Email, Email = input.Email };
            var result = await UserManager.CreateAsync(user, input.Password);

            if (!result.Succeeded)
            {
                return BadRequest(string.Join(", ", result.Errors));
            }
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetAccountInfo")]
        public async Task<IHttpActionResult> GetAccountInfo()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                return Ok(new
                {
                    IsAuthenticated = false,
                    Username = "",
                });

            var userManager = UserManager;

            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                Username = User.Identity.Name,
                Roles = await userManager.GetRolesAsync(user.Id),
            });
        }

        // POST api/Account/Logout
        [AllowAnonymous]
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            HttpContext.Current.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Ok(new
            {
                Message = "Logout successful"
            });
        }
    }
}