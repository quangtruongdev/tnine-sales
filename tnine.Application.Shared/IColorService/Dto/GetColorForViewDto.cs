using tnine.Core.Shared.Dto;

namespace tnine.Core.Shared.IColorService.Dto
{
    public class GetColorForViewDto : EntityDto<long>
    {
        public string HexCode { get; set; }
        public string Code { get; set; }
    }
}
