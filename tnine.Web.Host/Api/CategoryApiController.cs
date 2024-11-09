using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.ICategoryService;

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
    }
}