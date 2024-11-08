using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IShopService.Dto
{
    public class GetShopForViewDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
