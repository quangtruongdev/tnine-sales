using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.IProductWarehouseReceiptService;
using tnine.Application.Shared.IProductWarehouseReceiptService.Dto;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/productWarehouseReceipt")]
    public class ProductWarehouseReceiptApiController : ApiController
    {
        private readonly IProductWarehouseReceiptService _productWarehouseReceiptService;
        public ProductWarehouseReceiptApiController(IProductWarehouseReceiptService productWarehouseReceiptService)
        {
            _productWarehouseReceiptService = productWarehouseReceiptService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetProductWarehouseReceiptByWarehouseReceiptId([FromUri] long Id)
        {
            var productWarehouseReceipt = await _productWarehouseReceiptService.GetProductWarehouseReceiptById(Id);

            return Request.CreateResponse(HttpStatusCode.OK, productWarehouseReceipt);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateOrEdit([FromBody] List<CreateOrEditProductWarehouseReceiptDto> input)
        {
            await _productWarehouseReceiptService.CreateOrEdit(input);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("{warehouseReceiptId}/{productId}/{colorId}/{sizeId}")]
        public async Task<HttpResponseMessage> Delete([FromUri] long warehouseReceiptId, [FromUri] long productId, [FromUri] long colorId, [FromUri] long sizeId)
        {
            await _productWarehouseReceiptService.Delete(warehouseReceiptId, productId, colorId, sizeId);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}