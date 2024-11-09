using System.Collections.Generic;
using tnine.Application.Shared.IImageService.Dto;

namespace tnine.Application.Shared.IProductService.Dto
{
    public class CreateOrEditProductAndImageDto
    {
        public CreateOrEditProductDto Product { get; set; }
        public List<CreateOrEditImageDto> ImgUrl { get; set; }
        public List<long> ColorIds { get; set; }
        public List<long> SizeIds { get; set; }
    }
}
