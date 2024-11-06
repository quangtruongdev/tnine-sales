using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.ISizeService.Dto
{
    public class CreateOrEditSizeDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
