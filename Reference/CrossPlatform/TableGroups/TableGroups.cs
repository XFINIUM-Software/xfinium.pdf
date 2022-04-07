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
    /// TableGroups sample.
    /// </summary>
    public class TableGroups
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream verdanaFontStream, Stream verdanaBoldFontStream, Stream data)
        {
            PdfAnsiTrueTypeFont verdana = new PdfAnsiTrueTypeFont(verdanaFontStream, 1, true);
            PdfAnsiTrueTypeFont verdanaBold = new PdfAnsiTrueTypeFont(verdanaBoldFontStream, 1, true);

            PdfFlowDocument document = new PdfFlowDocument();

            PdfFlowContent header = BuildHeader(verdanaBold);
            document.AddContent(header);

            PdfFlowContent countriesSection = BuildCountriesList(verdana, verdanaBold, data);
            document.AddContent(countriesSection);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.tablegroups.pdf") };
            return output;
        }

        private static PdfFlowContent BuildHeader(PdfAnsiTrueTypeFont verdanaBold)
        {
            PdfAnsiTrueTypeFont headerFont = new PdfAnsiTrueTypeFont(verdanaBold);
            headerFont.Size = 16;

            PdfFlowTableContent headerTable = new PdfFlowTableContent(1);
            PdfFlowTableRow row = headerTable.Rows.AddRowWithCells("Continents, countries and populations");

            (row.Cells[0] as PdfFlowTableStringCell).Font = headerFont;
            row.Cells[0].HorizontalAlign = PdfGraphicAlign.Center;
            row.MinHeight = 40;

            return headerTable;
        }

        private static PdfFlowContent BuildCountriesList(PdfAnsiTrueTypeFont verdana, PdfAnsiTrueTypeFont verdanaBold, Stream data)
        {
            PdfAnsiTrueTypeFont textFont = new PdfAnsiTrueTypeFont(verdana);
            textFont.Size = 10;

            PdfAnsiTrueTypeFont headerFont = new PdfAnsiTrueTypeFont(verdanaBold);
            headerFont.Size = 12;

            PdfFlowTableContent countriesTable = new PdfFlowTableContent(2);
            countriesTable.Border = new PdfPen(PdfRgbColor.Black, 0.5);
            countriesTable.MinRowHeight = 16;
            (countriesTable.DefaultCell as PdfFlowTableStringCell).Font = textFont;
            countriesTable.Columns[0].VerticalAlign = PdfGraphicAlign.Center;
            countriesTable.Columns[1].VerticalAlign = PdfGraphicAlign.Center;
            countriesTable.Columns[1].HorizontalAlign = PdfGraphicAlign.Far;

            PdfFlowTableRow headerRow = countriesTable.HeaderRows.AddRowWithCells("Country", "Population");
            headerRow.Background = new PdfBrush(PdfRgbColor.LightGray);
            headerRow.Cells[0].HorizontalAlign = PdfGraphicAlign.Center;
            (headerRow.Cells[0] as PdfFlowTableStringCell).Font = headerFont;
            headerRow.Cells[1].HorizontalAlign = PdfGraphicAlign.Center;
            (headerRow.Cells[1] as PdfFlowTableStringCell).Font = headerFont;

            string continent = "";
            long total = 0;
            PdfFlowTableRow row = null;
            StreamReader sr = new StreamReader(data);
            string line = sr.ReadLine();
            while (line != null)
            {
                string[] items = line.Split('|');
                long pop = long.Parse(items[2], System.Globalization.CultureInfo.InvariantCulture);
                total = total + pop;

                if (continent != items[0])
                {
                    // Add group footer
                    if (continent != "")
                    {
                        row = countriesTable.Rows.AddRowWithCells("Total population for " + continent + ": " + total.ToString("#,##0"));
                        row.Cells[0].ColSpan = 2;
                        row.Cells[0].HorizontalAlign = PdfGraphicAlign.Far;
                        (row.Cells[0] as PdfFlowTableStringCell).Font = headerFont;
                    }
                    continent = items[0];
                    total = 0;

                    // Add group header
                    row = countriesTable.Rows.AddRowWithCells(continent);
                    row.Cells[0].ColSpan = 2;
                    row.Background = new PdfBrush(PdfRgbColor.LightGray);
                    (row.Cells[0] as PdfFlowTableStringCell).Font = headerFont;
                }
                row = countriesTable.Rows.AddRowWithCells(items[1], pop.ToString("#,##0"));
                line = sr.ReadLine();
            }
            row = countriesTable.Rows.AddRowWithCells("Total population for " + continent + ": " +  total.ToString("#,##0"));
            row.Cells[0].ColSpan = 2;
            row.Cells[0].HorizontalAlign = PdfGraphicAlign.Far;
            (row.Cells[0] as PdfFlowTableStringCell).Font = headerFont;

            return countriesTable;
        }
    }
}