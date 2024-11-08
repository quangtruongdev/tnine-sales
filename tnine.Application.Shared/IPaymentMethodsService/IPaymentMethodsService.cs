using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.IPaymentMethodsService.Dto;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IPaymentMethodsService
{
    public interface IPaymentMethodsService
    {
        Task<List<GetPaymentMethodsForViewDto>> GetAll();
        Task CreateOrEdit(CreateOrEditPaymentMethodsDto input);
        Task<GetPaymentMethodsForEditOutputDto> GetById(EntityDto<long> input);
        Task Delete(EntityDto<long> input);
    }
}
