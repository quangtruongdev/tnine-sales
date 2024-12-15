using ClosedXML.Excel;
using iText.Barcodes;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using tnine.Application.Shared.IInvoiceService;

namespace tnine.Application.ExportService
{
    public class InvoiceExportService
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceExportService(
            IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public async Task<MemoryStream> ExportInvoicesToExcel()
        {
            var invoices = await _invoiceService.GetAll();
            if (invoices == null || !invoices.Any()) throw new InvalidOperationException("No invoices found to export.");

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


        public byte[] GeneratePDF(long id)
        {
            var invoice = _invoiceService.GetInvoiceDetailInfo(id);

            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(stream, new WriterProperties());
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");
                var noBorder = Border.NO_BORDER;
                var divider = new Paragraph(new string('-', 130)).SetTextAlignment(TextAlignment.CENTER);

                document.Add(new Paragraph("Tnine Clothes Company").SetTextAlignment(TextAlignment.CENTER).SetFontSize(25));
                document.Add(new Paragraph("Try and feel").SetTextAlignment(TextAlignment.CENTER).SetFontSize(12));
                document.Add(divider);

                document.Add(new Paragraph($"Invoice Number: {invoice.InvoiceNumber}"));
                document.Add(new Paragraph($"Date: {invoice.Date.ToShortDateString()}"));
                document.Add(new Paragraph($"Customer Name: {invoice.CustomerName}"));
                document.Add(divider);

                Table itemsTable = new Table(new float[] { 20, 20, 10, 50 }).SetWidth(UnitValue.CreatePercentValue(100));
                itemsTable.AddCell(CreateCell("Item Name", TextAlignment.LEFT, noBorder));
                itemsTable.AddCell(CreateCell("Quantity", TextAlignment.CENTER, noBorder));
                itemsTable.AddCell(CreateCell("Unit Price", TextAlignment.CENTER, noBorder));
                itemsTable.AddCell(CreateCell("Total Price", TextAlignment.RIGHT, noBorder));
                foreach (var item in invoice.Items)
                {
                    itemsTable.AddCell(CreateCell(item.ItemName, TextAlignment.LEFT, noBorder));
                    itemsTable.AddCell(CreateCell(item.Quantity.ToString(), TextAlignment.CENTER, noBorder));
                    itemsTable.AddCell(CreateCell(item.UnitPrice.ToString("C"), TextAlignment.CENTER, noBorder));
                    itemsTable.AddCell(CreateCell(item.TotalPrice.ToString("C"), TextAlignment.RIGHT, noBorder));
                }
                document.Add(itemsTable);
                document.Add(divider);

                Table totalTable = new Table(2).SetWidth(UnitValue.CreatePercentValue(100));
                decimal tax = invoice.TotalAmount * 0.1M;
                decimal totalBeforeTax = invoice.TotalAmount - tax;
                totalTable.AddCell(CreateCell("Price before tax:", TextAlignment.LEFT, noBorder));
                totalTable.AddCell(CreateCell(totalBeforeTax.ToString("C"), TextAlignment.RIGHT, noBorder));
                totalTable.AddCell(CreateCell("Tax:", TextAlignment.LEFT, noBorder));
                totalTable.AddCell(CreateCell(tax.ToString("C"), TextAlignment.RIGHT, noBorder));
                totalTable.AddCell(CreateCell("Total Amount:", TextAlignment.LEFT, noBorder));
                totalTable.AddCell(CreateCell(invoice.TotalAmount.ToString("C"), TextAlignment.RIGHT, noBorder));
                document.Add(totalTable);
                document.Add(divider);

                Table paymentTable = new Table(2).SetWidth(UnitValue.CreatePercentValue(100));
                paymentTable.AddCell(CreateCell($"Payment method: {invoice.PaymentMode}", TextAlignment.LEFT, noBorder));
                paymentTable.AddCell(CreateCell(invoice.TotalAmount.ToString("C"), TextAlignment.RIGHT, noBorder));
                document.Add(paymentTable);
                document.Add(divider);

                document.Add(new Paragraph($"Please keep your invoice to pay back. \n Thank you and see you again. \n")
                    .SetTextAlignment(TextAlignment.CENTER));

                BarcodeQRCode barcodeQRCode = new BarcodeQRCode("This is link to my app");
                Image qrCodeImage = new Image(barcodeQRCode.CreateFormXObject(pdf));
                qrCodeImage.SetWidth(100).SetHeight(100).SetHorizontalAlignment(HorizontalAlignment.CENTER);
                document.Add(new Paragraph("Scan this QR code for details:").SetTextAlignment(TextAlignment.CENTER));
                document.Add(qrCodeImage);

                document.Close();
                return stream.ToArray();
            }
        }

        private Cell CreateCell(string content, TextAlignment alignment, Border border)
        {
            return new Cell().Add(new Paragraph(content).SetTextAlignment(alignment)).SetBorder(border);
        }
    }
}
