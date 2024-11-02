using tnine.Core.Shared.Dto;

namespace tnine.Application.Shared.ICustomerService.Dto
{
    public class CreateOrEditCustomerDto : EntityDto<long>
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
