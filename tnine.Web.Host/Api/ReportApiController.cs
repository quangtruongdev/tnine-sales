using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.IReportService;
using tnine.Web.Host.Infrastructure.Core;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/report")]
    public class ReportApiController : ApiControllerBase
    {

        private IReportService _reportService;

        public ReportApiController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        [Route("daily-revenue")]
        public async Task<HttpResponseMessage> GetDailyRevenue(DateTime date)
        {
            var revenue = await _reportService.GetDailyRevenue(date);
            return Request.CreateResponse(HttpStatusCode.OK, revenue);
        }

        [HttpGet]
        [Route("monthly-revenue")]
        public async Task<HttpResponseMessage> GetMonthlyRevenue(DateTime date)
        {
            var revenue = await _reportService.GetMonthlyRevenue(date);
            return Request.CreateResponse(HttpStatusCode.OK, revenue);
        }

        [HttpGet]
        [Route("yearly-revenue")]
        public async Task<HttpResponseMessage> GetYearlyRevenue(DateTime date)
        {
            var revenue = await _reportService.GetYearlyRevenue(date);
            return Request.CreateResponse(HttpStatusCode.OK, revenue);
        }
    }
}