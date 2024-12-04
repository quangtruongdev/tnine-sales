using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.IReportService;
using tnine.Core.Shared.Infrastructure;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class ReportService : IReportService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReportService(
            IInvoiceRepository invoiceRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<decimal> GetDailyRevenue(DateTime date)
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            return invoices
            .Where(x => x.CreationTime.Value.Day == date.Day && x.CreationTime.Value.Month == date.Month && x.CreationTime.Value.Year == date.Year)
            .Sum(x => x.Total);
        }

        public async Task<decimal> GetMonthlyRevenue(DateTime date)
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            return invoices
            .Where(x => x.CreationTime.Value.Year == date.Year && x.CreationTime.Value.Month == date.Month)
            .Sum(x => x.Total);
        }

        public async Task<decimal> GetYearlyRevenue(DateTime date)
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            return invoices
            .Where(x => x.CreationTime.Value.Year == date.Year)
            .Sum(x => x.Total);
        }
    }
}
