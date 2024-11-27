
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.IProductVariationDto;
using tnine.Application.Shared.IProductVariationDto.Dto;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/productVariation")]
    public class ProductVariationApiController : ApiController
    {
        private readonly IProductVariationService _productVariationsAppService;

        public ProductVariationApiController(IProductVariationService productVariationsAppService)
        {
            _productVariationsAppService = productVariationsAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetProductVariationByProductId([FromUri] long Id)
        {
            var productVariation = await _productVariationsAppService.GetProductVariationById(Id);

            return Request.CreateResponse(HttpStatusCode.OK, productVariation);

        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateOrEdit([FromBody] List<CreateOrEditProductVariaionDto> input)
        {
            await _productVariationsAppService.CreateOrEdit(input);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("{productId}/{colorId}/{sizeId}")]
        public async Task<HttpResponseMessage> Delete([FromUri] long productId, [FromUri] long colorId, [FromUri] long sizeId)
        {
            await _productVariationsAppService.Delete(productId, colorId, sizeId);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}