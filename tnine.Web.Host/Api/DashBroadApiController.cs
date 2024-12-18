using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.IDashboardService;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/dashboard")]
    public class DashBroadApiController : ApiController
    {
        private readonly IDashboardService _dashboardService;

        public DashBroadApiController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetValueForDashboard()
        {
            var result = await _dashboardService.GetValueForDashboard();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("product-best-sales")]
        public async Task<HttpResponseMessage> GetProductBestSaleOfMonth()
        {
            var result = await _dashboardService.GetProductBestSaleOfMonth();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("master-data")]
        public async Task<HttpResponseMessage> GetMasterDataForDashBoard()
        {
            var result = await _dashboardService.GetMasterDataForDashBoard();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}