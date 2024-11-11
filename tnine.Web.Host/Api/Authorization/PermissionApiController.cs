using System.Linq;
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
            //if (input == null)
            //{
            //    return BadRequest("Invalid data.");
            //}

            try
            {
                await _permissionService.CreateOrEdit(input);
                if (input.Id == null)
                {
                    return Ok("Permission created successfully.");
                }
                else
                {
                    return Ok("Permission updated successfully.");
                }
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

        [HttpPost]
        [Route("IsGranted")]
        public async Task<IHttpActionResult> IsGranted([FromBody] string permissionName)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var roleIds = user.Roles.Select(x => x.RoleId).ToList();
            //await _permissionService.CheckPermission(new GetPermissionWithRoleDto { PermissionName = permissionName, RoleIds = roleIds });
            return Ok();
        }

        [HttpGet]
        [Route("GetPermissionParent")]
        public async Task<IHttpActionResult> GetPermissionParent()
        {
            var permissions = await _permissionService.GetPermissionParent();
            return Ok(permissions);
        }
    }
}