using tnine.Core.Shared.Dto;

namespace tnine.Application.Shared.ICatagoryService.Dto
{
    public class GetCategoryForViewDto : EntityDto<long>
    {
        public string Name { get; set; }

    }
}
