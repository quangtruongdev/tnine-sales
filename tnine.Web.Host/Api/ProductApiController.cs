using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.IProductService;
using tnine.Application.Shared.IProductService.Dto;

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

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetProductForEdit([FromUri] long Id)
        {
            var product = await _productService.GetProductForEdit(Id);

            return Request.CreateResponse(HttpStatusCode.OK, product);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<HttpResponseMessage> Delete([FromUri] long Id)
        {
            await _productService.Delete(Id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateOrEdit([System.Web.Http.FromBody] CreateOrEditProductAndImageDto input)
        {
            await _productService.CreateOrEdit(input);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}