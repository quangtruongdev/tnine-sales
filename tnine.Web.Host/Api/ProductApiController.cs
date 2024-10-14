using System.Net;
using System.Net.Http;
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
        public HttpResponseMessage GetAll()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _productService.GetAll());
        }
        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}