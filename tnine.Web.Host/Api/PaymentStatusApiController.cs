using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.IPaymentStatusService;
using tnine.Application.Shared.IPaymentStatusService.Dto;
using tnine.Core.Shared.Dtos;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/PaymentStatus")]
    public class PaymentStatusApiController : ApiController
    {
        private readonly IPaymentStatusService _paymentStatusService;

        public PaymentStatusApiController(IPaymentStatusService paymentStatusService)
        {
            _paymentStatusService = paymentStatusService;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var paymentStatus = await _paymentStatusService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, paymentStatus);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetById(long id)
        {
            var input = new EntityDto<long> { Id = id };
            var paymentStatus = await _paymentStatusService.GetById(input);
            return Request.CreateResponse(HttpStatusCode.OK, paymentStatus);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateOrEdit([FromBody] CreateOrEditPaymentStatusDto input)
        {
            if (input == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }

            try
            {
                await _paymentStatusService.CreateOrEdit(input);
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    Message = "Save successfully."
                });
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
            var input = new EntityDto<long> { Id = id };
            await _paymentStatusService.Delete(input);
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                Message = "Delete successfully."
            });
        }
    }
}