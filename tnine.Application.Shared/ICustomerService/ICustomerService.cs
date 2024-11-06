using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.ICustomerService.Dto;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.ICustomerService
{
    public interface ICustomerService
    {
        Task<List<GetCustomerForViewDto>> GetAll();
        Task CreateOrEdit(CreateOrEditCustomerDto input);
        Task<GetCustomerForEditOutputDto> GetCustomerForEdit(EntityDto<long> input);
        Task Delete(EntityDto<long> input);
    }
}
