using tnine.Core.Shared.Dto;

namespace tnine.Application.Shared.ISizeService.Dto
{
    public class CreateOrEditSizeDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
