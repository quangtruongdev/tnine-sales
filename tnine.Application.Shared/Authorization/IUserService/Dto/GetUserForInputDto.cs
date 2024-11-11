using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.Authorization.IUserService.Dto
{
    public class GetUserForInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
