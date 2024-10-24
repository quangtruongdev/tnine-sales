using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using tnine.Application.Shared.IAccountService.Dto;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/account")]
    public class AccountApiController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountApiController() { }

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

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<HttpResponseMessage> Login(HttpRequestMessage req, LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var result = await SignInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, loginDto.RememberMe, shouldLockout: false);
            return req.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Authorize]
        [Route("logout")]
        public HttpResponseMessage Logout(HttpRequestMessage req)
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return req.CreateResponse(HttpStatusCode.OK, "Logged out successfully.");
        }
    }
}