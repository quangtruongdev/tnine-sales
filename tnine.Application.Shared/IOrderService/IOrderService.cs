using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.IOrderService.Dto;

namespace tnine.Application.Shared.IOrderService
{
    public interface IOrderService
    {
        Task<List<GetOrderForViewDto>> GetAll();
        Task<GetOrderForEditOutputDto> GetById(long id);
        Task CreateOrEdit(CreateOrEditOrderDto input);
        Task Delete(long id);
    }
}
