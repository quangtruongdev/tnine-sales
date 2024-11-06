using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IUserService.Dto
{
    public class GetApplicationUserForViewDto : EntityDto<long>
    {
        public string UserName { get; set; }
    }
}
