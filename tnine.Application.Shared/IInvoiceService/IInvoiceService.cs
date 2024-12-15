using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.IInvoiceService.Dto;

namespace tnine.Application.Shared.IInvoiceService
{
    public interface IInvoiceService
    {
        Task<List<GetInvoiceForViewDto>> GetAll();
        Task<GetInvoiceForEditOutputDto> GetById(long id);
        Task CreateOrEdit(CreateOrEditInvoiceDto input);
        Task Delete(long id);
        GetInvoiceDetailDto GetInvoiceDetailInfo(long id);
    }
}
