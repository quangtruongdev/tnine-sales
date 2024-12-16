using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.ISupplierService;
using tnine.Application.Shared.ISupplierService.Dto;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/supplier")]
    public class SupplierApiController : ApiController
    {
        private ISupplierService _supplierService;
        public SupplierApiController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var suppliers = await _supplierService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, suppliers);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetById(long id)
        {
            var supplier = await _supplierService.GetById(id);
            return Request.CreateResponse(HttpStatusCode.OK, supplier);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateOrEdit([FromBody] CreateOrEditSupplierDto input)
        {
            if (input == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }
            try
            {
                await _supplierService.CreateOrEdit(input);
                return Request.CreateResponse(HttpStatusCode.OK, "Supplier created or updated successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<HttpResponseMessage> Delete(long id)
        {
            try
            {
                await _supplierService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Supplier deleted successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}