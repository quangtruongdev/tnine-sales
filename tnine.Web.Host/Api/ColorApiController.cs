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
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var colors = await _colorService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, colors);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetById(long id)
        {
            var color = await _colorService.GetById(id);
            return Request.CreateResponse(HttpStatusCode.OK, color);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateOrEdit([FromBody] CreateOrEditColorDto input)
        {

            await _colorService.CreateOrEdit(input);
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                Message = "Save successfully."
            });

        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<HttpResponseMessage> Delete(long id)
        {
            await _colorService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                Message = "Delete successfully."
            });
        }
    }
}