using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.IRoleService;
using tnine.Application.Shared.IRoleService.Dto;
using tnine.Core.Shared.Dtos;

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
            var input = new EntityDto<long> { Id = id };
            var role = await _roleService.GetById(input);
            return Ok(role);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateOrEdit([FromBody] CreateOrEditRoleDto input)
        {
            if (ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                await _roleService.CreateOrEdit(input);
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
            await _roleService.Delete(input);
            return Ok("Role deleted successfully.");
        }
    }
}