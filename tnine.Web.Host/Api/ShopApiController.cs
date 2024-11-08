using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.IShop;
using tnine.Application.Shared.IShopService.Dto;
using tnine.Core.Shared.Dtos;
using tnine.Web.Host.Infrastructure.Core;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/shop")]
    public class ShopApiController : ApiControllerBase
    {
        private IShopService _shopService;

        public ShopApiController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var shops = await _shopService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, shops);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetById(long id)
        {
            var input = new EntityDto<long> { Id = id };
            var shop = await _shopService.GetById(input);
            return Request.CreateResponse(HttpStatusCode.OK, shop);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateOrEdit([FromBody] CreateOrEditShopDto input)
        {
            if (input == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }

            try
            {
                await _shopService.CreateOrEdit(input);
                return Request.CreateResponse(HttpStatusCode.OK, "Shop created or updated successfully.");
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
            await _shopService.Delete(input);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}