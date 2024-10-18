using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.IProductService;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/product")]
    public class ProductApiController : ApiController
    {
        private readonly IProductService _productService;

        public ProductApiController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var products = await _productService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }
    }
}