using System;
using System.Collections.Generic;

namespace tnine.Application.Shared.IInvoiceService.Dto
{
    public class GetInvoiceDetailDto
    {
        public string InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string PaymentMode { get; set; }
        public List<InvoiceItem> Items { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class InvoiceItem
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
