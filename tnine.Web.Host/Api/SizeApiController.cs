using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.ISizeService;
using tnine.Application.Shared.ISizeService.Dto;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/size")]
    public class SizeApiController : ApiController
    {
        private ISizeService _sizeService;
        public SizeApiController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }
        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var sizes = await _sizeService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, sizes);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetById(long id)
        {
            var size = await _sizeService.GetById(id);
            return Request.CreateResponse(HttpStatusCode.OK, size);
        }
        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateOrEdit([FromBody] CreateOrEditSizeDto input)
        {
            if (input == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }
            try
            {
                await _sizeService.CreateOrEdit(input);
                return Request.CreateResponse(HttpStatusCode.OK, "Size created or updated successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<HttpResponseMessage> Delete(long id)
        {
            await _sizeService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}