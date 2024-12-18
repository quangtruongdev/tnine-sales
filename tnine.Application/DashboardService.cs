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
        private readonly ICustomerRepository _customerRepository;
        private readonly ICategoreisRepository _categoreisRepository;


        public DashboardService(
            IInvoiceRepository invoiceRepository,
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            ICategoreisRepository categoreisRepository,
            IProductInvoicesRepository productInvoicesRepository)
        {
            _invoiceRepository = invoiceRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _categoreisRepository = categoreisRepository;
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
                .Take(5)
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
                    Value = totalForMonth,
                    TotalInvoiceInMonth = await GetTotalInvoiceInMonth(monthToCheck)
                });
            }

            return result;
        }

        private async Task<long> GetTotalInvoiceInMonth(DateTime input)
        {
            var invoices = await _invoiceRepository.GetAllAsync();

            var startOfMonth = new DateTime(input.Year, input.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            var result = invoices.Where(o => o.CreationTime >= startOfMonth && o.CreationTime <= endOfMonth);

            return result.Count();
        }

        private async Task<decimal> Sum(DateTime input)
        {
            var invoices = await _invoiceRepository.GetAllAsync();

            var startOfMonth = new DateTime(input.Year, input.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            var result = invoices.Where(o => o.CreationTime >= startOfMonth && o.CreationTime <= endOfMonth);

            return result.Sum(o => o.Total);
        }

        public async Task<GetMasterDataForDashBoardDto> GetMasterDataForDashBoard()
        {
            return new GetMasterDataForDashBoardDto
            {
                TotalRevenue = await GetTotalRevenue(),
                TotalProduct = await GetTotalProduct(),
                TotalCustomer = await GetTotalCustomer(),
                TotalInvoice = await GetTotalAverageInvoiceInEveryMonth(),
                ProductBestSales = await GetProductBestSaleOfMonth(),
                RevenueMonthly = await GetValueForDashboard(),
                CategoriesPercents = await GetCategoriesPercent(),
                ProductSellIn12Months = await GetProductQuantitySellIn12MonthsByEveryMonth()
            };
        }

        private async Task<List<ProductSellInMonth>> GetProductQuantitySellIn12MonthsByEveryMonth()
        {
            var crrDate = DateTime.Now;

            var result = new List<ProductSellInMonth>();

            for (int i = 0; i <= 11; i++)
            {
                var monthToCheck = crrDate.AddMonths(-i);

                var startOfMonth = new DateTime(monthToCheck.Year, monthToCheck.Month, 1);
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

                var invoices = await _invoiceRepository.GetAllAsync();
                var invoiceOfMonths = invoices.Where(o => o.CreationTime >= startOfMonth && o.CreationTime <= endOfMonth)
                                                .Select(o => o.Id)
                                                .ToList();
                var invoiceDetails = await _productInvoicesRepository.GetAllAsync();
                var filteredInvoiceDetails = invoiceDetails.Where(d => invoiceOfMonths.Contains(d.InvoiceId)).ToList();

                var quantity = filteredInvoiceDetails.Sum(o => o.Quantity);

                result.Add(new ProductSellInMonth
                {
                    DateTime = monthToCheck,
                    Quantity = quantity
                });
            }

            return result;
        }

        private async Task<List<CategoriesPercent>> GetCategoriesPercent()
        {
            var categories = await _categoreisRepository.GetAllAsync();
            var products = await _productRepository.GetAllAsync();

            var result = categories.Select(o => new CategoriesPercent
            {
                Name = o.Name,
                Value = products.Count(p => p.CategoryId == o.Id).ToString()
            }).ToList();

            return result;
        }

        private async Task<decimal> GetTotalRevenue()
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            return invoices.Sum(o => o.Total);
        }

        private async Task<long> GetTotalProduct()
        {
            var products = await _productRepository.GetAllAsync();

            return products.Count();
        }

        private async Task<long> GetTotalCustomer()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Count();
        }

        private async Task<long> GetTotalAverageInvoiceInEveryMonth()
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            var result = invoices
                        .Where(o => o.CreationTime.HasValue)
                        .GroupBy(o => o.CreationTime.Value.Month)
                        .Select(o => new
                        {
                            Month = o.Key,
                            Count = o.Count()
                        })
                        .Average(o => o.Count);
            return Convert.ToInt64(result);
        }
    }
}
