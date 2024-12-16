using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.ISupplierService.Dto
{
    public class GetSupplierForEditOutputDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
