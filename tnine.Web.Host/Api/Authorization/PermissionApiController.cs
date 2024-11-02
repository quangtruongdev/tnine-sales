﻿using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.Authorization.IPermissionService;
using tnine.Application.Shared.Authorization.IPermissionService.Dto;
using tnine.Core.Shared.Dto;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/permission")]
    [Authorize]
    public class PermissionApiController : ApiController
    {
        private readonly IPermissionService _permissionService;

        public PermissionApiController(
            IPermissionService permissionService
            )
        {
            _permissionService = permissionService;
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
    }
}