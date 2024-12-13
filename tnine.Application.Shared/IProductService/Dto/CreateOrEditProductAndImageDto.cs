using System.Collections.Generic;
using tnine.Application.Shared.IImageService.Dto;
using tnine.Application.Shared.IProductVariationService.Dto;

namespace tnine.Application.Shared.IProductService.Dto
{
    public class CreateOrEditProductAndImageDto
    {
        public CreateOrEditProductDto Product { get; set; }
        public List<CreateOrEditImageDto> ImgUrl { get; set; }
        public List<CreateOrEditProductVariaionDto> ProductVariation { get; set; }
    }
}
