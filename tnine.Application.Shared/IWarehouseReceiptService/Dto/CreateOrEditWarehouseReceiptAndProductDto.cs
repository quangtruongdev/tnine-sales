using System.Collections.Generic;
using tnine.Application.Shared.IProductService.Dto;
using tnine.Application.Shared.ISizeService.Dto;
using tnine.Core.Shared.IColorService.Dto;

namespace tnine.Application.Shared.IWarehouseReceiptService.Dto
{
    public class CreateOrEditWarehouseReceiptAndProductDto
    {
        public CreateOrEditWarehouseReceiptDto WarehouseReceipt { get; set; }
        public List<CreateOrEditProductDto> Product { get; set; }
        public List<CreateOrEditColorDto> Color { get; set; }
        public List<CreateOrEditSizeDto> Size { get; set; }
    }
}
