using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.IShopService.Dto;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IShop
{
    public interface IShopService
    {
        Task<List<GetShopForViewDto>> GetAll();
        //Task<PagedResultDto<GetTodoForViewDto>> GetAll(GetTodoForInputDto input);
        Task CreateOrEdit(CreateOrEditShopDto input);
        Task<GetShopForEditOutputDto> GetById(EntityDto<long> input);
        Task Delete(EntityDto<long> input);
    }
}
