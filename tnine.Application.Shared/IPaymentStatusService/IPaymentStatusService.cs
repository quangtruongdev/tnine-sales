using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.IPaymentStatusService.Dto;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IPaymentStatusService
{
    public interface IPaymentStatusService
    {
        Task<List<GetPaymentStatusForViewDto>> GetAll();
        Task CreateOrEdit(CreateOrEditPaymentStatusDto input);
        Task<GetPaymentStatusForEditOutputDto> GetById(EntityDto<long> input);
        Task Delete(EntityDto<long> input);
    }
}
