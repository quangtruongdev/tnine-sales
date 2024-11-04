using tnine.Core.Shared.Dto;

namespace tnine.Core.Shared.IColorService.Dto
{
    public class CreateOrEditColorDto : EntityDto<long>
    {
        public string Code { get; set; }
        public string HexCode { get; set; }
    }
}
