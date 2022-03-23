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
    /// TableCellSpans sample.
    /// </summary>
    public class TableCellSpans
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream verdanaFontStream)
        {
            PdfAnsiTrueTypeFont textFont = new PdfAnsiTrueTypeFont(verdanaFontStream, 10, true);
            PdfAnsiTrueTypeFont headerFont = new PdfAnsiTrueTypeFont(textFont);
            headerFont.Size = 16;

            PdfFlowDocument document = new PdfFlowDocument();

            PdfFlowTableContent headerTable = new PdfFlowTableContent(1);
            PdfFlowTableRow row = headerTable.Rows.AddRowWithCells("Store sales by year");
            (row.Cells[0] as PdfFlowTableStringCell).Font = headerFont;
            row.Cells[0].HorizontalAlign = PdfGraphicAlign.Center;
            row.MinHeight = 40;
            document.AddContent(headerTable);

            PdfFlowTableContent itemsTable = new PdfFlowTableContent(4);
            (itemsTable.DefaultCell as PdfFlowTableStringCell).Font = textFont;
            itemsTable.Border = new PdfPen(PdfRgbColor.Black, 0.5);
            itemsTable.MinRowHeight = 15;
            itemsTable.Columns[2].VerticalAlign = PdfGraphicAlign.Center;
            itemsTable.Columns[3].VerticalAlign = PdfGraphicAlign.Center;
            itemsTable.Columns[3].HorizontalAlign = PdfGraphicAlign.Far;

            row = itemsTable.Rows.AddRowWithCells("Tablets", "iPad Air 2", "2013", "213,554");
            row.Cells[0].RowSpan = 12;
            row.Cells[1].RowSpan = 3;
            itemsTable.Rows.AddRowWithCells("2014", "123,443");
            itemsTable.Rows.AddRowWithCells("2015", "64,443");
            row = itemsTable.Rows.AddRowWithCells("iPad Pro", "2013", "342,443");
            row.Cells[0].RowSpan = 3;
            itemsTable.Rows.AddRowWithCells("2014", "56,332");
            itemsTable.Rows.AddRowWithCells("2015", "765,231");
            row = itemsTable.Rows.AddRowWithCells("Nexus 7", "2013", "432,541");
            row.Cells[0].RowSpan = 3;
            itemsTable.Rows.AddRowWithCells("2014", "213,871");
            itemsTable.Rows.AddRowWithCells("2015", "112,332");
            row = itemsTable.Rows.AddRowWithCells("Nexus 9", "2013", "342,434");
            row.Cells[0].RowSpan = 3;
            itemsTable.Rows.AddRowWithCells("2014", "231,778");
            itemsTable.Rows.AddRowWithCells("2015", "119,324");

            row = itemsTable.Rows.AddRowWithCells("Smartphones", "Samsung Galaxy S5", "2013", "1,543,321");
            row.Cells[0].RowSpan = 12;
            row.Cells[1].RowSpan = 3;
            itemsTable.Rows.AddRowWithCells("2014", "1,435,875");
            itemsTable.Rows.AddRowWithCells("2015", "1,876,324");
            row = itemsTable.Rows.AddRowWithCells("Samsung Galaxy S6", "2013", "1,432,134");
            row.Cells[0].RowSpan = 3;
            itemsTable.Rows.AddRowWithCells("2014", "1,232,432");
            itemsTable.Rows.AddRowWithCells("2015", "1,765,112");
            row = itemsTable.Rows.AddRowWithCells("iPhone 6", "2013", "1,433,665");
            row.Cells[0].RowSpan = 3;
            itemsTable.Rows.AddRowWithCells("2014", "2,443,245");
            itemsTable.Rows.AddRowWithCells("2015", "1,656,334");
            row = itemsTable.Rows.AddRowWithCells("iPhone 6 Plus", "2013", "994,123");
            row.Cells[0].RowSpan = 3;
            itemsTable.Rows.AddRowWithCells("2014", "443,546");
            itemsTable.Rows.AddRowWithCells("2015", "765,342");

            document.AddContent(itemsTable);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.tablecellspans.pdf") };
            return output;
        }
    }
}