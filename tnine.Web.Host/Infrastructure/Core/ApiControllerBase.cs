using System.Web.Http;
using System.Web.Http.Cors;

namespace tnine.Web.Host.Infrastructure.Core
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApiControllerBase : ApiController
    {
        //private static readonly ILog log = LogManager.GetLogger(typeof(ApiControllerBase));

        //static ApiControllerBase()
        //{
        //    XmlConfigurator.Configure();
        //}

        //public override async Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        return await base.ExecuteAsync(controllerContext, cancellationToken);
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("An unhandled exception occurred while processing the request.", ex);
        //        throw;
        //    }
        //}
    }
}
