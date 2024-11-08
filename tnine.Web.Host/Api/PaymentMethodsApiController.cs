using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.IPaymentMethodsService;
using tnine.Application.Shared.IPaymentMethodsService.Dto;
using tnine.Core.Shared.Dtos;
namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/PaymentMethods")]
    public class PaymentMethodsApiController : ApiController
    {
        private readonly IPaymentMethodsService _paymentMethodsService;

        public PaymentMethodsApiController(IPaymentMethodsService paymentMethodsService)
        {
            _paymentMethodsService = paymentMethodsService;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var paymentMethods = await _paymentMethodsService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, paymentMethods);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetById(long id)
        {
            var input = new EntityDto<long> { Id = id };
            var paymentMethods = await _paymentMethodsService.GetById(input);
            return Request.CreateResponse(HttpStatusCode.OK, paymentMethods);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateOrEdit([FromBody] CreateOrEditPaymentMethodsDto input)
        {
            if (input == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }

            try
            {
                await _paymentMethodsService.CreateOrEdit(input);
                return Request.CreateResponse(HttpStatusCode.OK, "PaymentMethods created or updated successfully.");
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
            await _paymentMethodsService.Delete(input);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}