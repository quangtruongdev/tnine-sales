using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using tnine.Application.Shared.Authorization.IAccountService.Dto;
using tnine.Application.Shared.Authorization.IUserService;
using tnine.Application.Shared.Authorization.IUserService.Dto;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/user")]
    [Authorize]
    public class UserApiController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IUserService _userService;

        public UserApiController()
        {
        }

        public UserApiController(
            ApplicationSignInManager signInManager,
            ApplicationUserManager userManager,
            IUserService userService
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
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

        // GET api/User
        //[HttpGet]
        //[Route("")]
        //public async Task<IHttpActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        //{
        //    var totalCount = UserManager.Users.Count();
        //    var users = await Task.Run(() => UserManager.Users
        //    .OrderBy(u => u.Id)
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList()
        //    );
        //    var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        //    return Ok(new
        //    {
        //        Success = true,
        //        Data = users,
        //        TotalCount = totalCount,
        //        PageNumber = pageNumber,
        //        PageSize = pageSize,
        //        TotalPages = totalPages
        //    });
        //}


        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll([FromUri] GetUserForInputDto input)
        {
            return Ok(new
            {
                Success = true,
                Data = await _userService.GetAll(input)
            });
        }

        // GET api/User/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetById(long id)
        {
            var user = await UserManager.FindByIdAsync(id);
            var roles = await UserManager.GetRolesAsync(id);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            return Ok(new
            {
                Success = true,
                Data = user
            });
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