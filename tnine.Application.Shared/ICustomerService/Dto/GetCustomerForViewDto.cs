using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.ICustomerService.Dto
{
    public class GetCustomerForViewDto : EntityDto<long>
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
