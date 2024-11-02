using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.IApplicationRoleService;
using tnine.Application.Shared.IApplicationRoleService.Dto;
using tnine.Core.Shared.Dto;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/role")]
    [Authorize]
    public class RoleApiController : ApiController
    {
        private readonly IApplicationRoleService _applicationRoleService;

        public RoleApiController(IApplicationRoleService applicationRoleService)
        {
            _applicationRoleService = applicationRoleService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            var roles = await _applicationRoleService.GetAll();
            return Ok(roles);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetById(long id)
        {
            var input = new EntityDto<long> { Id = id };
            var role = await _applicationRoleService.GetById(input);
            return Ok(role);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateOrEdit([FromBody] CreateOrEditRoleDto input)
        {
            if (input == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                await _applicationRoleService.CreateOrEdit(input);
                return Ok("Role created or updated successfully.");
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(long id)
        {
            var input = new EntityDto<long> { Id = id };
            await _applicationRoleService.Delete(input);
            return Ok("Role deleted successfully.");
        }
    }
}