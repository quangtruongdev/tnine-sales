using System.Collections.Generic;

namespace tnine.Application.Shared.IProductService.Dto
{
    public class CreateOrEditProductAndImageDto
    {
        public CreateOrEditProductDto Product { get; set; }
        public List<string> ImgUrl { get; set; }
    }
}
