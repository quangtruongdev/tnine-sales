using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.IImageService;
using tnine.Application.Shared.IImageService.Dto;

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

        [HttpPost]
        [Route("upload")]
        public async Task<HttpResponseMessage> UploadImage()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "Invalid file format");
            }

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (var file in provider.Contents)
            {
                var fileName = file.Headers.ContentDisposition.FileName?.Trim('"') ?? "uploadedFile";
                var buffer = await file.ReadAsByteArrayAsync();
                var wwwrootPath = GetWwwRootPath();

                string filePath = Path.Combine(wwwrootPath, fileName);

                try
                {
                    var directory = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await fileStream.WriteAsync(buffer, 0, buffer.Length);
                    }

                    var imageUrl = fileName;

                    return Request.CreateResponse(HttpStatusCode.OK, new { imageUrl });
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error uploading file: " + ex.Message);
                }
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No file uploaded");
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateOrEdit([FromBody] List<CreateOrEditImageDto> input)
        {
            await _iamgeService.CreateOrEdit(input);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<HttpResponseMessage> Delete(long id)
        {
            await _iamgeService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        protected string GetWwwRootPath()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string wwwRootPath = Path.Combine(baseDirectory, "wwwroot");

            return wwwRootPath;
        }
    }
}