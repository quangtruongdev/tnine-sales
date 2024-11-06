using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.ICustomerService;
using tnine.Application.Shared.ICustomerService.Dto;
using tnine.Core.Shared.Dtos;
using tnine.Web.Host.Infrastructure.Core;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/customer")]
    public class CustomerApiController : ApiControllerBase
    {
        private ICustomerService _customerService;

        public CustomerApiController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var customers = await _customerService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, customers);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetCustomerForEdit(long id)
        {
            var input = new EntityDto<long> { Id = id };
            var customer = await _customerService.GetCustomerForEdit(input);
            return Request.CreateResponse(HttpStatusCode.OK, customer);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateOrEdit([FromBody] CreateOrEditCustomerDto input)
        {
            if (input == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }

            try
            {
                await _customerService.CreateOrEdit(input);
                return Request.CreateResponse(HttpStatusCode.OK, "Customer created or updated successfully.");
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
            await _customerService.Delete(input);
            return Request.CreateResponse(HttpStatusCode.OK, "Customer deleted successfully.");
        }
    }
}