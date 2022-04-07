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
    /// SimpleTable sample.
    /// </summary>
    public class SimpleTable
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream verdanaFontStream, Stream verdanaBoldFontStream, Stream data)
        {
            PdfAnsiTrueTypeFont verdana = new PdfAnsiTrueTypeFont(verdanaFontStream, 1, true);
            PdfAnsiTrueTypeFont verdanaBold = new PdfAnsiTrueTypeFont(verdanaBoldFontStream, 1, true);

            PdfFlowDocument document = new PdfFlowDocument();
            document.PageCreated += Document_PageCreated;

            PdfFlowContent header = BuildHeader(verdanaBold);
            document.AddContent(header);

            PdfFlowContent attendantsSection = BuildAttendantsList(verdana, verdanaBold, data);
            document.AddContent(attendantsSection);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.simpletable.pdf") };
            return output;
        }

        private static void Document_PageCreated(object sender, PdfFlowPageCreatedEventArgs e)
        {
            e.PageDefaults.Margins.Left = 18;
            e.PageDefaults.Margins.Right = 18;
            e.PageDefaults.Rotation = 90;
        }

        private static PdfFlowContent BuildHeader(PdfAnsiTrueTypeFont verdanaBold)
        {
            PdfAnsiTrueTypeFont headerFont = new PdfAnsiTrueTypeFont(verdanaBold);
            headerFont.Size = 36;

            PdfFlowTableContent headerTable = new PdfFlowTableContent(1);
            PdfFlowTableRow row = headerTable.Rows.AddRowWithCells("Conference attendants");

            (row.Cells[0] as PdfFlowTableStringCell).Font = headerFont;
            row.Cells[0].HorizontalAlign = PdfGraphicAlign.Center;
            row.MinHeight = 40;

            return headerTable;
        }

        private static PdfFlowContent BuildAttendantsList(PdfAnsiTrueTypeFont verdana, PdfAnsiTrueTypeFont verdanaBold, Stream data)
        {
            PdfAnsiTrueTypeFont textFont = new PdfAnsiTrueTypeFont(verdana);
            textFont.Size = 10;

            PdfAnsiTrueTypeFont headerFont = new PdfAnsiTrueTypeFont(verdanaBold);
            headerFont.Size = 12;

            PdfFlowTableContent attendantsTable = new PdfFlowTableContent(5);
            attendantsTable.Border = new PdfPen(PdfRgbColor.Black, 0.5);
            attendantsTable.MinRowHeight = 15;
            (attendantsTable.DefaultCell as PdfFlowTableStringCell).Font = textFont;
            attendantsTable.Columns[0].VerticalAlign = PdfGraphicAlign.Center;
            attendantsTable.Columns[0].Width = 120;
            attendantsTable.Columns[0].WidthIsRelativeToTable = false;
            attendantsTable.Columns[1].VerticalAlign = PdfGraphicAlign.Center;
            attendantsTable.Columns[1].Width = 210;
            attendantsTable.Columns[1].WidthIsRelativeToTable = false;
            attendantsTable.Columns[2].VerticalAlign = PdfGraphicAlign.Center;
            attendantsTable.Columns[2].Width = 100;
            attendantsTable.Columns[2].WidthIsRelativeToTable = false;
            attendantsTable.Columns[3].VerticalAlign = PdfGraphicAlign.Center;
            attendantsTable.Columns[3].Width = 190;
            attendantsTable.Columns[3].WidthIsRelativeToTable = false;
            attendantsTable.Columns[4].VerticalAlign = PdfGraphicAlign.Center;
            attendantsTable.Columns[4].Width = 130;
            attendantsTable.Columns[4].WidthIsRelativeToTable = false;

            PdfFlowTableRow row = attendantsTable.Rows.AddRowWithCells("Name", "Email", "Phone", "Company", "Country");
            for (int i = 0; i < row.Cells.Count; i++)
            {
                (row.Cells[i] as PdfFlowTableStringCell).Font = headerFont;
                (row.Cells[i] as PdfFlowTableStringCell).Color = new PdfBrush(PdfRgbColor.White);
                row.Cells[i].HorizontalAlign = PdfGraphicAlign.Center;
            }
            row.Background = new PdfBrush(PdfRgbColor.Black);

            int counter = 0;
            PdfBrush alternateBackground = new PdfBrush(PdfRgbColor.LightGray);
            StreamReader sr = new StreamReader(data);
            string line = sr.ReadLine();
            while (line != null)
            {
                string[] items = line.Split('|');

                row = attendantsTable.Rows.AddRowWithCells(items);
                line = sr.ReadLine();

                if (counter % 2 == 0)
                {
                    row.Background = alternateBackground;
                }
                counter++;
            }

            return attendantsTable;
        }
    }
}