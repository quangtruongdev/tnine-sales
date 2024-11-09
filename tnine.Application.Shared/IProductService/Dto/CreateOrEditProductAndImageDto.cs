using System.Collections.Generic;
using tnine.Application.Shared.IImageService.Dto;

namespace tnine.Application.Shared.IProductService.Dto
{
    public class CreateOrEditProductAndImageDto
    {
        public CreateOrEditProductDto Product { get; set; }
        public List<CreateOrEditImageDto> ImgUrl { get; set; }
        //public List<CreateOrEditProductVariayionDto> ProductVariayion { get; set; }
    }
}
