using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.IImageService;
using tnine.Application.Shared.IImageService.Dto;
using tnine.Core.Shared.Dtos;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class ImagerService : IIamgeService
    {
        private readonly IImageRepository _imageRepository;
        public ImagerService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task Delete(long Id)
        {
            var image = await _imageRepository.FirstOrDefaultAsync(e => e.Id == Id);
            await _imageRepository.DeleteAsync(image);
        }

        public async Task<PagedResultDto<GetImageForViewDto>> GetImageByProductId(long productId)
        {
            var images = await _imageRepository.GetAllAsync();
            var query = from image in images
                        where image.ProductId == productId
                        select new GetImageForViewDto
                        {
                            Id = image.Id,
                            ImgUrl = image.ImgUrl,
                        };
            var totalCount = query.Count();
            //var result = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<GetImageForViewDto>(totalCount, query.ToList());
        }
    }
}
