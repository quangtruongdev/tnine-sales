using System.Collections.Generic;
using System.Threading.Tasks;

namespace tnine.Application.Shared.IDashboardService
{
    public interface IDashboardService
    {
        Task<List<GetValueForDashboardDto>> GetValueForDashboard();
        Task<List<GetProductBestSalesDto>> GetProductBestSaleOfMonth();
    }
}
