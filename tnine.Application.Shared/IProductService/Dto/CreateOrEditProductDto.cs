using tnine.Core.Shared.Dto;

namespace tnine.Application.Shared.IProductService.Dto
{
    public class CreateOrEditProductDto : EntityDto<long>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        //public List<string> ListImages { get; set; }
        public long CategoryId { get; set; }
        //public List<long> ColorId { get; set; }
        //public List<long> SizeId { get; set; }

    }
}
