using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using tnine.Application.Shared.Authorization.IAccountService.Dto;
using tnine.Application.Shared.IAccountService.Dto;
using tnine.Core;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/Account")]
    public class AccountApiController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountApiController()
        {
        }

        public AccountApiController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
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
        public async Task<IHttpActionResult> Login(LoginDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await SignInManager.PasswordSignInAsync(input.Email, input.Password, input.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return Ok();
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
        public async Task<IHttpActionResult> Register(RegisterInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser { UserName = input.Email, Email = input.Email };
            var result = await UserManager.CreateAsync(user, input.Password);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(string.Join(", ", result.Errors));
        }


        // POST api/Account/Logout
        [AllowAnonymous]
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            HttpContext.Current.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Ok();
        }
    }
}