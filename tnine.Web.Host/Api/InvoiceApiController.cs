using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.IInvoiceService;
using tnine.Application.Shared.IInvoiceService.Dto;
using tnine.Web.Host.Infrastructure.Core;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/invoice")]
    public class InvoiceApiController : ApiControllerBase
    {
        private IInvoiceService _invoiceService;

        public InvoiceApiController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var invoices = await _invoiceService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, invoices);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetInvoiceForEdit(long id)
        {
            var invoice = await _invoiceService.GetById(id);
            return Request.CreateResponse(HttpStatusCode.OK, invoice);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateOrEdit([FromBody] CreateOrEditInvoiceDto input)
        {
            if (input == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }

            try
            {
                await _invoiceService.CreateOrEdit(input);
                return Request.CreateResponse(HttpStatusCode.OK, "Invoice created or updated successfully.");
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
            await _invoiceService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK, "Invoice deleted successfully.");
        }
    }
}