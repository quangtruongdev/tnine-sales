using System;
using System.Threading.Tasks;

namespace tnine.Application.Shared.IReportService
{
    public interface IReportService
    {
        Task<decimal> GetDailyRevenue(DateTime date);
        Task<decimal> GetMonthlyRevenue(DateTime date);
        Task<decimal> GetYearlyRevenue(DateTime date);
    }
}
