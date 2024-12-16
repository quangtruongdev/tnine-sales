using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.IWarehouseReceiptService.Dto;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IWarehouseReceiptService
{
    public interface IWarehouseReceiptService
    {
        Task<PagedResultDto<GetWarehouseReceiptForViewDto>> GetAll();
        Task<List<GetWarehouseReceiptForEditDto>> GetWarehouseReceiptForEdit(long Id);
        Task Delete(long Id);
        Task CreateOrEdit(CreateOrEditWarehouseReceiptDto input);
        Task<PagedResultDto<GetWarehouseReceiptForViewDto>> GetWarehouseReceiptBySupplierId(long SupplierId);
        Task<GetWarehouseReceiptForViewDto> GetDetailWarehouseReceipt(long id);
        Task<decimal> GetTotalWarehouseReceipt(long id);
        Task<string> GetSupplier(long id);
    }
}
