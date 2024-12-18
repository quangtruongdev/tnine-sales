using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IRoleService.Dto
{
    public class GetRoleForInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
