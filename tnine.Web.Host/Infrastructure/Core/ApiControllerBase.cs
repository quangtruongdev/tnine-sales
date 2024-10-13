using log4net;
using log4net.Config;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace tnine.Web.Host.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ApiControllerBase));

        static ApiControllerBase()
        {
            XmlConfigurator.Configure();
        }

        public override async Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            try
            {
                return await base.ExecuteAsync(controllerContext, cancellationToken);
            }
            catch (Exception ex)
            {
                log.Error("An unhandled exception occurred while processing the request.", ex);
                throw;
            }
        }
    }
}
