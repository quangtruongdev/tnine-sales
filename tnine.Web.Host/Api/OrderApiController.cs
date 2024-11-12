using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.IOrderService;
using tnine.Application.Shared.IOrderService.Dto;
using tnine.Web.Host.Infrastructure.Core;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/order")]
    public class OrderApiController : ApiControllerBase
    {
        private IOrderService _orderService;

        public OrderApiController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var orders = await _orderService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, orders);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetOrderForEdit(long id)
        {
            var oder = await _orderService.GetById(id);
            return Request.CreateResponse(HttpStatusCode.OK, oder);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateOrEdit([FromBody] CreateOrEditOrderDto input)
        {
            if (input == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }

            try
            {
                await _orderService.CreateOrEdit(input);
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
            await _orderService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK, "Invoice deleted successfully.");
        }
    }
}