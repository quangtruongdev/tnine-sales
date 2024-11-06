using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IProductService.Dto
{
    public class CreateOrEditProductDto : EntityDto<long>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
