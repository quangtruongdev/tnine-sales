using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.IWarehouseReceiptService;
using tnine.Application.Shared.IWarehouseReceiptService.Dto;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/warehouse-receipt")]
    public class WarehouseReceiptApiController : ApiController
    {
        private readonly IWarehouseReceiptService _warehouseReceiptService;

        public WarehouseReceiptApiController(IWarehouseReceiptService warehouseReceiptService)
        {
            _warehouseReceiptService = warehouseReceiptService;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var warehouseReceipts = await _warehouseReceiptService.GetAll();

            return Request.CreateResponse(HttpStatusCode.OK, warehouseReceipts);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetWarehouseReceiptForEdit([FromUri] long Id)
        {
            var warehouseReceipt = await _warehouseReceiptService.GetWarehouseReceiptForEdit(Id);

            return Request.CreateResponse(HttpStatusCode.OK, warehouseReceipt);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<HttpResponseMessage> Delete([FromUri] long Id)
        {
            await _warehouseReceiptService.Delete(Id);

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                Message = "Delete successfully."
            });
        }
        [HttpGet]
        [Route("supplier/{id}")]
        public async Task<HttpResponseMessage> GetSupplier([FromUri] long id)
        {
            var supplier = await _warehouseReceiptService.GetSupplier(id);

            return Request.CreateResponse(HttpStatusCode.OK, supplier);
        }

        [HttpGet]
        [Route("total/{id}")]
        public async Task<HttpResponseMessage> GetTotalWarehouseReceipt([FromUri] long id)
        {
            var warehouseReceipts = await _warehouseReceiptService.GetTotalWarehouseReceipt(id);

            return Request.CreateResponse(HttpStatusCode.OK, warehouseReceipts);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateOrEdit([FromBody] CreateOrEditWarehouseReceiptDto input)
        {
            try
            {
                await _warehouseReceiptService.CreateOrEdit(input);
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    Message = "Save successfully."
                });
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exceptionMessage);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}