using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.ICatagoryService.Dto;
using tnine.Application.Shared.IProductService.Dto;
using tnine.Core.Shared.Dto;

namespace tnine.Application.Shared.IProductService
{
    public interface IProductService
    {
        Task<PagedResultDto<GetProductForViewDto>> GetAll();
        Task<GetProductForEditDto> GetProductForEdit(long Id);
        Task Delete(long Id);
        Task CreateOrEdit(CreateOrEditProductAndImageDto input);
        Task<PagedResultDto<GetProductForViewDto>> GetProductByCategoryId(long CateGoryId);
        Task<List<GetCategoryForViewDto>> GetListCategories();
    }
}
