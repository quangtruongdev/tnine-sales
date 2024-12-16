using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.IProductWarehouseReceiptService.Dto;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IProductWarehouseReceiptService
{
    public interface IProductWarehouseReceiptService
    {
        Task<PagedResultDto<GetProductWarehouseReceiptForViewDto>> GetProductWarehouseReceiptByWarehouseReceiptId(GetProductWarehouseReceiptInputDto input);
        Task CreateOrEdit(List<CreateOrEditProductWarehouseReceiptDto> input);
        Task Delete(long warehouseReceiptId, long productId, long colorId, long sizeId);
        Task<List<GetProductWarehouseReceiptForEditDto>> GetProductWarehouseReceiptById(long warehouseReceiptId);
    }
}
