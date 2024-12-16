using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.ISupplierService.Dto;

namespace tnine.Application.Shared.ISupplierService
{
    public interface ISupplierService
    {
        Task<List<GetSupplierForViewDto>> GetAll();
        Task CreateOrEdit(CreateOrEditSupplierDto input);
        Task Delete(long id);
        Task<GetSupplierForEditOutputDto> GetById(long id);
    }
}
