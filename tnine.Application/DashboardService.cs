using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.IDashboardService;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class DashboardService : IDashboardService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductInvoicesRepository _productInvoicesRepository;

        public DashboardService(
            IInvoiceRepository invoiceRepository,
            IProductRepository productRepository,
            IProductInvoicesRepository productInvoicesRepository)
        {
            _invoiceRepository = invoiceRepository;
            _productRepository = productRepository;
            _productInvoicesRepository = productInvoicesRepository;
        }

        public async Task<List<GetProductBestSalesDto>> GetProductBestSaleOfMonth()
        {
            var crrDate = DateTime.Now;

            var startOfMonth = new DateTime(crrDate.Year, crrDate.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            var invoices = await _invoiceRepository.GetAllAsync();
            var invoiceOfMonths = invoices.Where(o => o.CreationTime >= startOfMonth && o.CreationTime <= endOfMonth)
                                            .Select(o => o.Id)
                                            .ToList();
            var invoiceDetails = await _productInvoicesRepository.GetAllAsync();
            var filteredInvoiceDetails = invoiceDetails.Where(d => invoiceOfMonths.Contains(d.InvoiceId)).ToList();

            var result = filteredInvoiceDetails.GroupBy(o => o.ProductId)
                .Select(o => new GetProductBestSalesDto
                {
                    ProductId = o.Key,
                    Quantity = o.Sum(s => s.Quantity)
                })
                .OrderByDescending(o => o.Quantity)
                .Take(3)
                .ToList();

            foreach (var item in result)
            {
                var product = await _productRepository.FirstOrDefaultAsync(p => p.Id == item.ProductId);
                item.ProductName = product.Name;
            }

            return result;
        }

        public async Task<List<GetValueForDashboardDto>> GetValueForDashboard()
        {
            var crrDate = DateTime.Now;

            var result = new List<GetValueForDashboardDto>();

            for (int i = 0; i <= 5; i++)
            {
                var monthToCheck = crrDate.AddMonths(-i);

                var totalForMonth = await Sum(monthToCheck);

                result.Add(new GetValueForDashboardDto
                {
                    DateTime = monthToCheck,
                    Value = totalForMonth
                });
            }

            return result;
        }


        private async Task<decimal> Sum(DateTime input)
        {
            var invoices = await _invoiceRepository.GetAllAsync();

            var startOfMonth = new DateTime(input.Year, input.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            var result = invoices.Where(o => o.CreationTime >= startOfMonth && o.CreationTime <= endOfMonth);

            return result.Sum(o => o.Total);
        }

    }
}
