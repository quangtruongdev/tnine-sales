using System;
using System.Collections.Generic;
using tnine.Core;

namespace tnine.Application.Shared.IInvoiceService.Dto
{
    public class InvoiceAndInvoiceDetailsDto
    {
        public CreateOrEditInvoiceDto Invoice { get; set; }
        public List<InvoiceItemDto> Items { get; set; }

        public InvoiceAndInvoiceDetailsDto()
        {
            Invoice = new CreateOrEditInvoiceDto(); // Khởi tạo trong constructor
            Items = new List<InvoiceItemDto>();
        }
    }

    public class InvoiceItemDto
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public long SizeId { get; set; }
        public long ColorId { get; set; }
    }
}
