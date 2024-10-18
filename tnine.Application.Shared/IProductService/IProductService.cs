using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Core;

namespace tnine.Application.Shared.IProductService
{
    public interface IProductService
    {
        //IEnumerable<Product> GetAll();
        //Task<List<PagedResultDto<GetProductForViewDto>>> GetAll(GetProductInputDto input);
        Task<List<Product>> GetAll();
    }
}
