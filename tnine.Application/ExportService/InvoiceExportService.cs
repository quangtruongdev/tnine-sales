using ClosedXML.Excel;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using tnine.Application.Shared.IInvoiceService;

namespace tnine.Application.ExportService
{
    public class InvoiceExportService
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceExportService(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public async Task<MemoryStream> ExportInvoicesToExcel()
        {
            var invoices = await _invoiceService.GetAll();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Invoices");
                worksheet.Cell(1, 1).Value = "Invoice ID";
                worksheet.Cell(1, 2).Value = "Creation Time";
                worksheet.Cell(1, 3).Value = "Customer Name";
                worksheet.Cell(1, 4).Value = "Customer Telephone";
                worksheet.Cell(1, 5).Value = "Payment Status";
                worksheet.Cell(1, 6).Value = "Payment Method";
                worksheet.Cell(1, 7).Value = "Total";

                int row = 2;
                foreach (var invoice in invoices)
                {
                    worksheet.Cell(row, 1).Value = invoice.Id;
                    worksheet.Cell(row, 2).Value = invoice.CreationTime.ToString("F", new CultureInfo("vi-VN"));
                    worksheet.Cell(row, 3).Value = invoice.CustomerName;
                    worksheet.Cell(row, 4).Value = invoice.CustomerTelephone;
                    worksheet.Cell(row, 5).Value = invoice.PaymentStatusName;
                    worksheet.Cell(row, 6).Value = invoice.PaymentMethodName;
                    worksheet.Cell(row, 7).Value = invoice.Total;
                    row++;
                }

                var memoryStream = new MemoryStream();
                workbook.SaveAs(memoryStream);
                memoryStream.Position = 0;

                return memoryStream;
            }
        }
    }
}
