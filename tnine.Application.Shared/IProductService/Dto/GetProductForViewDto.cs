using tnine.Core.Shared.Dto;

namespace tnine.Application.Shared.IProductService.Dto
{
    public class GetProductForViewDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
