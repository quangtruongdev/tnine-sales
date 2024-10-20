using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.IProductService.Dto;

namespace tnine.Application.Shared.IProductService
{
    public interface IProductService
    {
        //IEnumerable<Product> GetAll();
        //Task<List<PagedResultDto<GetProductForViewDto>>> GetAll(GetProductInputDto input);

        //Task<PagedResultDto<GetProductForViewDto>> GetAll(GetProductInputDto input);
        Task<List<GetProductForViewDto>> GetAll();
    }
}
