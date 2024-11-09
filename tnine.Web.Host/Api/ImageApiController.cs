using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.IImageService;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/image")]
    public class ImageApiController : ApiController
    {
        private IIamgeService _iamgeService;

        public ImageApiController(IIamgeService iamgeService)
        {
            _iamgeService = iamgeService;
        }
        [HttpGet]
        [Route("{productId}")]
        public async Task<HttpResponseMessage> GetImageByProductId(long productId)
        {
            var images = await _iamgeService.GetImageByProductId(productId);

            return Request.CreateResponse(HttpStatusCode.OK, images);
        }
    }
}