using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.Authorization.IPermissionService;
using tnine.Application.Shared.Authorization.IPermissionService.Dto;
using tnine.Core.Shared.Dtos;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/permission")]
    [Authorize]
    public class PermissionApiController : ApiController
    {
        private readonly IPermissionService _permissionService;
        private ApplicationUserManager _userManager;

        public PermissionApiController(
            IPermissionService permissionService,
            ApplicationUserManager userManager
            )
        {
            _permissionService = permissionService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            var permissions = await _permissionService.GetAll();
            return Ok(permissions);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateOrEdit([FromBody] CreateOrEditPermissionDto input)
        {
            if (input == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                await _permissionService.CreateOrEdit(input);
                return Ok("Permission created or updated successfully.");
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetById(long id)
        {
            var input = new EntityDto<long> { Id = id };
            var permission = await _permissionService.GetById(input);
            return Ok(permission);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(long id)
        {
            var input = new EntityDto<long> { Id = id };
            await _permissionService.Delete(input);
            return Ok("Permission deleted successfully.");
        }

        //[HttpPost]
        //[Route("IsGranted")]
        //public async Task<IHttpActionResult> IsGranted([FromBody] string permissionName)
        //{
        //    var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    var user = await userManager.FindByNameAsync(User.Identity.Name);

        //    if (user == null)
        //    {
        //        return BadRequest("User not found");
        //    }

        //    var roles = await userManager.GetRolesAsync(user.Id);
        //    var roleIds = await _permissionService.GetRoleIdsByNames(roles.ToArray());

        //    var input = new GetPermissionWithRoleDto
        //    {
        //        PermissionName = permissionName,
        //        Roles = roles.ToList()
        //    };

        //    var isGranted = await _permissionService.IsGraned(input);
        //    return Ok(isGranted);
        //}
    }
}