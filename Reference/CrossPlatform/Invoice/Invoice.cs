using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Actions;
using Xfinium.Pdf.FlowDocument;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Graphics.FormattedContent;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// Invoice sample.
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream verdanaFontStream, Stream verdanaBoldFontStream, Stream logoImageStream)
        {
            PdfAnsiTrueTypeFont verdana = new PdfAnsiTrueTypeFont(verdanaFontStream, 10, true);
            PdfAnsiTrueTypeFont verdanaBold = new PdfAnsiTrueTypeFont(verdanaBoldFontStream, 10, true);
            PdfPngImage logoImage = new PdfPngImage(logoImageStream);

            PdfFlowDocumentDefaults fdd = new PdfFlowDocumentDefaults();
            // Automatically tag content for accessibility.
            fdd.EnableAutomaticTagging = true;
            PdfFlowDocument document = new PdfFlowDocument(fdd);
            document.Language = "en-US";
            document.ViewerPreferences = new PdfViewerPreferences();
            document.ViewerPreferences.DisplayDocumentTitle = true;

            document.DocumentInformation = new PdfDocumentInformation();
            document.DocumentInformation.Title = "Invoice Demo with Structured Tagged PDF content";

            document.XmpMetadata = new PdfXmpMetadata();

            PdfFlowContent header = BuildHeader(verdana, logoImage);
            document.AddContent(header);

            PdfFlowContent sellerSection = BuildSellerSection(verdana, verdanaBold);
            document.AddContent(sellerSection);

            PdfFlowContent invoiceInfoSection = BuildInvoiceInfoSection(verdana, verdanaBold);
            document.AddContent(invoiceInfoSection);

            PdfFlowContent buyerSection = BuildBuyerSection(verdana, verdanaBold);
            document.AddContent(buyerSection);

            PdfFlowContent invoiceItemsSection = BuildInvoiceItemsSection(verdana, verdanaBold);
            document.AddContent(invoiceItemsSection);

            PdfFlowContent endSection = BuildEndSection(verdana);
            document.AddContent(endSection);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.invoice.pdf") };
            return output;
        }

        private static PdfFlowContent BuildHeader(PdfAnsiTrueTypeFont verdana, PdfImage logoImage)
        {
            PdfFlowTableContent headerTable = new PdfFlowTableContent(2);
            PdfFlowTableRow row = headerTable.Rows.AddRowWithCells("Invoice", logoImage);

            PdfAnsiTrueTypeFont headerFont = new PdfAnsiTrueTypeFont(verdana);
            headerFont.Size = 36;
            (row.Cells[0] as PdfFlowTableStringCell).Font = headerFont;
            row.Cells[0].VerticalAlign = PdfGraphicAlign.Center;
            row.Cells[1].HorizontalAlign = PdfGraphicAlign.Far;
            (row.Cells[1] as PdfFlowTableImageCell).ImageWidth = 135;

            return headerTable;
        }

        private static PdfFlowContent BuildSellerSection(PdfAnsiTrueTypeFont verdana, PdfAnsiTrueTypeFont verdanaBold)
        {
            PdfFlowTableContent sellerTable = new PdfFlowTableContent(2);

            PdfCode39Barcode c39 = new PdfCode39Barcode("1234567890");
            c39.BarcodeTextPosition = PdfBarcodeTextPosition.None;
            PdfFormXObject xo = new PdfFormXObject(c39.Width, c39.Height);
            xo.Graphics.DrawBarcode(c39, 0, 0);
            PdfFlowFormXObjectContent flowXO = new PdfFlowFormXObjectContent(xo);

            PdfAnsiTrueTypeFont labelFont = new PdfAnsiTrueTypeFont(verdanaBold);
            labelFont.Size = 12;
            PdfAnsiTrueTypeFont contentFont = new PdfAnsiTrueTypeFont(verdana);
            contentFont.Size = 12;

            PdfFormattedContent sellerInfo = new PdfFormattedContent();
            sellerInfo.Paragraphs.Add(new PdfFormattedTextBlock("DemoCompany LLC", contentFont));
            sellerInfo.Paragraphs.Add(new PdfFormattedTextBlock("3000 Alandala Road", contentFont));
            sellerInfo.Paragraphs.Add(new PdfFormattedTextBlock("Beverly Hills", contentFont));
            sellerInfo.Paragraphs.Add(new PdfFormattedTextBlock("CA 90210", contentFont));
            sellerInfo.Paragraphs.Add(new PdfFormattedTextBlock("United States", contentFont));
            sellerInfo.Paragraphs.Add(" ");

            PdfFormattedTextBlock labelPhone = new PdfFormattedTextBlock("Phone: ", labelFont);
            PdfFormattedTextBlock phone = new PdfFormattedTextBlock("+1-555-123-4567", contentFont);
            sellerInfo.Paragraphs.Add(labelPhone, phone);

            PdfFormattedTextBlock labelFax = new PdfFormattedTextBlock("Fax: ", labelFont);
            PdfFormattedTextBlock fax = new PdfFormattedTextBlock("+1-555-456-7890", contentFont);
            sellerInfo.Paragraphs.Add(labelFax, fax);

            PdfFormattedTextBlock labelEmail = new PdfFormattedTextBlock("Email: ", labelFont);
            PdfFormattedTextBlock email = new PdfFormattedTextBlock("support@xfiniumpdf.com", contentFont);
            email.TextColor = new PdfBrush(PdfRgbColor.Blue);
            email.Action = new PdfUriAction("mailto:support@xfiniumpdf.com");
            sellerInfo.Paragraphs.Add(labelEmail, email);

            PdfFormattedTextBlock labelWeb = new PdfFormattedTextBlock("Web: ", labelFont);
            PdfFormattedTextBlock web = new PdfFormattedTextBlock("www.xfiniumpdf.com", contentFont);
            web.TextColor = new PdfBrush(PdfRgbColor.Blue);
            web.Action = new PdfUriAction("http://www.xfiniumpdf.com");
            sellerInfo.Paragraphs.Add(labelWeb, web);

            for (int i = 0; i < sellerInfo.Paragraphs.Count; i++)
            {
                sellerInfo.Paragraphs[i].HorizontalAlign = PdfStringHorizontalAlign.Right;
            }
            PdfFlowTextContent sellerInfoText = new PdfFlowTextContent(sellerInfo);

            sellerTable.Rows.AddRowWithCells(flowXO, sellerInfoText);
            return sellerTable;
        }

        private static PdfFlowContent BuildInvoiceInfoSection(PdfAnsiTrueTypeFont verdana, PdfAnsiTrueTypeFont verdanaBold)
        {
            PdfAnsiTrueTypeFont labelFont = new PdfAnsiTrueTypeFont(verdanaBold);
            labelFont.Size = 12;
            PdfAnsiTrueTypeFont contentFont = new PdfAnsiTrueTypeFont(verdana);
            contentFont.Size = 12;

            PdfFlowTableContent invoiceInfoTable = new PdfFlowTableContent(2);
            invoiceInfoTable.Columns[0].Width = 120;
            invoiceInfoTable.Columns[0].WidthIsRelativeToTable = false;
            (invoiceInfoTable.DefaultCell as PdfFlowTableStringCell).Font = contentFont;
            PdfFlowTableRow row = invoiceInfoTable.Rows.AddRowWithCells(" ", " ");
            row = invoiceInfoTable.Rows.AddRowWithCells("Date", "15 March 2016");
            (row.Cells[0] as PdfFlowTableStringCell).Font = labelFont;
            row = invoiceInfoTable.Rows.AddRowWithCells("Invoice number:", "1234567890");
            (row.Cells[0] as PdfFlowTableStringCell).Font = labelFont;
            row = invoiceInfoTable.Rows.AddRowWithCells("Currency:", "USD (US Dollars)");
            (row.Cells[0] as PdfFlowTableStringCell).Font = labelFont;

            return invoiceInfoTable;
        }

        private static PdfFlowContent BuildBuyerSection(PdfAnsiTrueTypeFont verdana, PdfAnsiTrueTypeFont verdanaBold)
        {
            PdfAnsiTrueTypeFont headerFont = new PdfAnsiTrueTypeFont(verdana);
            headerFont.Size = 20;
            PdfAnsiTrueTypeFont labelFont = new PdfAnsiTrueTypeFont(verdanaBold);
            labelFont.Size = 12;
            PdfAnsiTrueTypeFont contentFont = new PdfAnsiTrueTypeFont(verdana);
            contentFont.Size = 12;

            PdfFormattedContent buyerInfo = new PdfFormattedContent();
            buyerInfo.Paragraphs.Add(new PdfFormattedTextBlock(" ", headerFont));
            buyerInfo.Paragraphs.Add(new PdfFormattedTextBlock("Invoice to:", headerFont));
            buyerInfo.Paragraphs.Add(new PdfFormattedTextBlock(" ", headerFont));
            buyerInfo.Paragraphs.Add(new PdfFormattedTextBlock("Contoso LLC", contentFont));
            buyerInfo.Paragraphs.Add(new PdfFormattedTextBlock("1000 Summer Road", contentFont));
            buyerInfo.Paragraphs.Add(new PdfFormattedTextBlock("London", contentFont));
            buyerInfo.Paragraphs.Add(new PdfFormattedTextBlock("1A2 3B4", contentFont));
            buyerInfo.Paragraphs.Add(new PdfFormattedTextBlock("United Kingdom", contentFont));
            buyerInfo.Paragraphs.Add(" ");

            PdfFormattedTextBlock labelVAT = new PdfFormattedTextBlock("Your VAT/Tax number: ", labelFont);
            PdfFormattedTextBlock vat = new PdfFormattedTextBlock("GB1234567890", contentFont);
            buyerInfo.Paragraphs.Add(labelVAT, vat);
            buyerInfo.Paragraphs.Add(new PdfFormattedTextBlock(" ", headerFont));

            PdfFlowTextContent buyerInfoText = new PdfFlowTextContent(buyerInfo);

            return buyerInfoText;
        }

        private static PdfFlowContent BuildInvoiceItemsSection(PdfAnsiTrueTypeFont verdana, PdfAnsiTrueTypeFont verdanaBold)
        {
            PdfAnsiTrueTypeFont labelFont = new PdfAnsiTrueTypeFont(verdanaBold);
            labelFont.Size = 12;
            PdfAnsiTrueTypeFont contentFont = new PdfAnsiTrueTypeFont(verdana);
            contentFont.Size = 12;

            PdfFlowTableContent invoiceInfoTable = new PdfFlowTableContent(5);
            invoiceInfoTable.MinRowHeight = 20;
            invoiceInfoTable.Columns[0].VerticalAlign = PdfGraphicAlign.Center;
            invoiceInfoTable.Columns[0].Width = 250;
            invoiceInfoTable.Columns[0].WidthIsRelativeToTable = false;
            invoiceInfoTable.Columns[1].HorizontalAlign = PdfGraphicAlign.Far;
            invoiceInfoTable.Columns[1].VerticalAlign = PdfGraphicAlign.Center;
            invoiceInfoTable.Columns[1].Width = 50;
            invoiceInfoTable.Columns[1].WidthIsRelativeToTable = false;
            invoiceInfoTable.Columns[2].VerticalAlign = PdfGraphicAlign.Center;
            invoiceInfoTable.Columns[2].HorizontalAlign = PdfGraphicAlign.Far;
            invoiceInfoTable.Columns[2].Width = 80;
            invoiceInfoTable.Columns[2].WidthIsRelativeToTable = false;
            invoiceInfoTable.Columns[3].HorizontalAlign = PdfGraphicAlign.Far;
            invoiceInfoTable.Columns[3].VerticalAlign = PdfGraphicAlign.Center;
            invoiceInfoTable.Columns[3].Width = 80;
            invoiceInfoTable.Columns[3].WidthIsRelativeToTable = false;
            invoiceInfoTable.Columns[4].HorizontalAlign = PdfGraphicAlign.Far;
            invoiceInfoTable.Columns[4].VerticalAlign = PdfGraphicAlign.Center;
            invoiceInfoTable.Columns[4].Width = 80;
            invoiceInfoTable.Columns[4].WidthIsRelativeToTable = false;
            (invoiceInfoTable.DefaultCell as PdfFlowTableStringCell).Font = contentFont;

            PdfFlowTableRow row = invoiceInfoTable.Rows.AddRowWithCells("Description", "Qty", "Price", "Total", "VAT/Tax");
            for (int i = 0; i < row.Cells.Count; i++)
            {
                (row.Cells[i] as PdfFlowTableStringCell).Font = labelFont;
                (row.Cells[i] as PdfFlowTableStringCell).Color = new PdfBrush(PdfRgbColor.White);
            }
            row.Background = new PdfBrush(PdfRgbColor.Black);
            row = invoiceInfoTable.Rows.AddRowWithCells("Sample green item", "1", "100.00", "100.00", "20.00");
            row.Background = new PdfBrush(PdfRgbColor.LightGray);
            row = invoiceInfoTable.Rows.AddRowWithCells("Big pink box", "3", "250.00", "750.00", "150.00");
            row = invoiceInfoTable.Rows.AddRowWithCells("Yellow glass bowl", "2", "200.00", "400.00", "80.00");
            row.Background = new PdfBrush(PdfRgbColor.LightGray);
            row = invoiceInfoTable.Rows.AddRowWithCells("Total", "", "", "1250.00", "250.00");
            row = invoiceInfoTable.Rows.AddRowWithCells("Total (including VAT/Tax)", "", "", "1500.00", "");
            (row.Cells[0] as PdfFlowTableStringCell).Font = labelFont;
            (row.Cells[3] as PdfFlowTableStringCell).Font = labelFont;

            return invoiceInfoTable;
        }

        private static PdfFlowContent BuildEndSection(PdfAnsiTrueTypeFont verdana)
        {
            PdfAnsiTrueTypeFont headerFont = new PdfAnsiTrueTypeFont(verdana);
            headerFont.Size = 20;
            PdfAnsiTrueTypeFont contentFont = new PdfAnsiTrueTypeFont(verdana);
            contentFont.Size = 12;

            PdfFormattedContent endInfo = new PdfFormattedContent();
            endInfo.Paragraphs.Add(new PdfFormattedTextBlock(" ", headerFont));
            endInfo.Paragraphs.Add(new PdfFormattedTextBlock("PAID IN FULL by credit card.", headerFont));
            endInfo.Paragraphs.Add(new PdfFormattedTextBlock(" ", headerFont));

            PdfFormattedTextBlock text1 = new PdfFormattedTextBlock("If you have any queries regarding this Invoice, please contact ", contentFont);
            PdfFormattedTextBlock email = new PdfFormattedTextBlock("sales@xfiniumpdf.com", contentFont);
            PdfFormattedTextBlock text2 = new PdfFormattedTextBlock(" quoting the Invoice Number above.", contentFont);
            email.TextColor = new PdfBrush(PdfRgbColor.Blue);
            email.Action = new PdfUriAction("mailto:sales@xfiniumpdf.com");
            endInfo.Paragraphs.Add(text1, email, text2);

            PdfFlowTextContent endInfoText = new PdfFlowTextContent(endInfo);

            return endInfoText;
        }
    }
}