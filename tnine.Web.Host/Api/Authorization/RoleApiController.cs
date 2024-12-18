using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.IRoleService;
using tnine.Application.Shared.IRoleService.Dto;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/role")]
    [Authorize]
    public class RoleApiController : ApiController
    {
        private readonly IRoleService _roleService;

        public RoleApiController(IRoleService applicationRoleService)
        {
            _roleService = applicationRoleService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            var roles = await _roleService.GetAll();
            return Ok(roles);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetById(long id)
        {
            var role = await _roleService.GetById(id);
            return Ok(role);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateOrEdit([FromBody] CreateOrEditRoleDto input)
        {
            try
            {
                await _roleService.CreateOrEdit(input);
                if (input.Id.HasValue)
                {
                    return Ok("Role updated successfully.");
                }
                else
                {
                    return Ok("Role created successfully.");
                }
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete([FromUri] long id)
        {
            await _roleService.Delete(id);
            return Ok("Role deleted successfully.");
        }
    }
}