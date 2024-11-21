using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.ExportService;
using tnine.Application.Shared.IInvoiceService;
using tnine.Application.Shared.IInvoiceService.Dto;
using tnine.Web.Host.Infrastructure.Core;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/invoice")]
    public class InvoiceApiController : ApiControllerBase
    {
        private IInvoiceService _invoiceService;
        private InvoiceExportService _invoiceExportService;

        public InvoiceApiController(IInvoiceService invoiceService, 
            InvoiceExportService invoiceExportService)
        {
            _invoiceService = invoiceService;
            _invoiceExportService = invoiceExportService;
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

        [HttpGet]
        [Route("exportExcel")]
        public async Task<HttpResponseMessage> ExportExcel()
        {
            string fileName = $"Invoices{DateTime.UtcNow}.xlsx";
            MemoryStream stream = await _invoiceExportService.ExportInvoicesToExcel();

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };
            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            return httpResponseMessage;
        }
    }
}