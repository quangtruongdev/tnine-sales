using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.ICategoryService;
using tnine.Application.Shared.ICategoryService.Dto;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/category")]
    public class CategoryApiController : ApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryApiController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var categories = await _categoryService.GetAll();

            return Request.CreateResponse(HttpStatusCode.OK, categories);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateOrEdit([FromBody] CreateOrEditCategoryDto input)
        {
            await _categoryService.CreateOrEdit(input);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<HttpResponseMessage> Delete([FromUri] long id)
        {
            await _categoryService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetById([FromUri] long id)
        {
            var category = await _categoryService.GetById(id);

            return Request.CreateResponse(HttpStatusCode.OK, category);
        }
    }
}