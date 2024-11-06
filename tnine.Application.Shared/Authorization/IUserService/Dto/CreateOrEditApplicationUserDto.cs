using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IUserService.Dto
{
    public class CreateOrEditApplicationUserDto : EntityDto<long>
    {
        public string UserName { get; set; }
    }
}
