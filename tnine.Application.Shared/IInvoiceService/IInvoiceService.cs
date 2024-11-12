using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.IInvoiceService.Dto;

namespace tnine.Application.Shared.IInvoiceService
{
    public interface IOrderService
    {
        Task<List<GetInvoiceForViewDto>> GetAll();
        Task<GetInvoiceForEditOutputDto> GetById(long id);
        Task CreateOrEdit(CreateOrEditInvoiceDto input);
        Task Delete(long id);
    }
}
