using System;
using System.Collections.Generic;

namespace tnine.Application.Shared.IDashboardService
{
    public class GetMasterDataForDashBoardDto
    {
        public decimal TotalRevenue { get; set; }
        public long TotalInvoice { get; set; }
        public long TotalProduct { get; set; }
        public long TotalCustomer { get; set; }
        public List<GetValueForDashboardDto> RevenueMonthly { get; set; }
        public List<CategoriesPercent> CategoriesPercents { get; set; }
        public List<GetProductBestSalesDto> ProductBestSales { get; set; }
        public List<ProductSellInMonth> ProductSellIn12Months { get; set; }

    }

    public class CategoriesPercent
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class ProductSellInMonth
    {
        public DateTime DateTime { get; set; }
        public long Quantity { get; set; }
    }
}
