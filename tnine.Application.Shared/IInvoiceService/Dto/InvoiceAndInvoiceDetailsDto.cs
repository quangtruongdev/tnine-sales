using System;
using System.Collections.Generic;

namespace tnine.Application.Shared.IInvoiceService.Dto
{
    public class InvoiceAndInvoiceDetailsDto
    {
        public CreateOrEditInvoiceDto Invoice { get; set; }
        public List<InvoiceItemDto> Items { get; set; }
    }

    public class InvoiceItemDto
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public long SizeId { get; set; }
        public long ColorId { get; set; }
    }
}
