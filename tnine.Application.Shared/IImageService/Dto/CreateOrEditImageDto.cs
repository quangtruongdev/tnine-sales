using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IImageService.Dto
{
    public class CreateOrEditImageDto : EntityDto<long>
    {
        public string ImgUrl { get; set; }
    }
}
