using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.IColorService;
using tnine.Core.Shared.IColorService.Dto;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/color")]
    public class ColorApiController : ApiController
    {
        private IColorService _colorService;
        public ColorApiController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var colors = await _colorService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, colors);
        }

        [HttpGet]
        [Route("GetById{id}")]
        public async Task<HttpResponseMessage> GetById(long id)
        {
            var color = await _colorService.GetById(id);
            return Request.CreateResponse(HttpStatusCode.OK, color);
        }

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<HttpResponseMessage> CreateOrEdit([FromBody] CreateOrEditColorDto input)
        {
            if (input == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }

            try
            {
                await _colorService.CreateOrEdit(input);
                return Request.CreateResponse(HttpStatusCode.OK, "Todo created or updated successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("Delete{id}")]
        public async Task<HttpResponseMessage> Delete(long id)
        {
            await _colorService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}