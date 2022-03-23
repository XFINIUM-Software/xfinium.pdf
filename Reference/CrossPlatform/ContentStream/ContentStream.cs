using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Core.Cos;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Graphics.Text;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// ContentStream sample.
    /// </summary>
    public class ContentStream
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        /// <param name="stream"></param>
        public static SampleOutputInfo[] Run()
        {
            // Create the pdf document
            PdfFixedDocument document = new PdfFixedDocument();
            // Create a new page in the document
            PdfPage page = document.Pages.Add();

            PdfBrush brush = new PdfBrush(PdfRgbColor.DarkRed);
            PdfStandardFont helvetica = new PdfStandardFont(PdfStandardFontFace.Helvetica, 36);
            page.Graphics.DrawString("Hello World", helvetica, brush, 100, 100);

            PdfContentStream cs = new PdfContentStream(page.Graphics);
            // Sets the stroke and fill colorspaces to DeviceRGB
            cs.SetStrokeColorSpace(new PdfRgbColorSpace());
            cs.SetFillColorSpace(new PdfRgbColorSpace());
            // Set stroke color to blue
            cs.SetStrokeColor(new double[] { 0, 0, 1 });
            // Set fill color to green
            cs.SetFillColor(new double[] { 0, 1, 0 });

            // Draw a line from (0, 0) to (page.Width/2, page.Height/2)
            // It will be drawn from top left corner to center of the page.
            cs.MoveTo(0, 0);
            cs.LineTo(page.Width / 2, page.Height / 2);
            cs.StrokePath();

            // Begin a text section
            cs.BeginText();
            cs.SetTextRendering(PdfTextRenderingMode.FillText);
            cs.SetTextMatrix(1, 0, 0, 1, 100, 150);
            cs.SetTextFontAndSize(helvetica, helvetica.Size);

            // This text will appear inverted because the coordinate system is in visual mode.
            byte[] binaryText = helvetica.EncodeString("Hello World");
            cs.ShowText(new PdfCosBinaryString(binaryText));
            cs.EndText();

            // Reset coordinate system and the current graphics state to default PDF
            cs.ResetPdfCoordinateSystem();
            // Sets the stroke and fill colorspaces to DeviceRGB
            cs.SetStrokeColorSpace(new PdfRgbColorSpace());
            cs.SetFillColorSpace(new PdfRgbColorSpace());
            // Set stroke color to blue
            cs.SetStrokeColor(new double[] { 0, 0, 1 });
            // Set fill color to green
            cs.SetFillColor(new double[] { 0, 1, 0 });

            // Draw a line from (0, 0) to (page.Width/2, page.Height/2)
            // It will be drawn from bottom left corner to center of the page because the coordinate system has been reset to default PDF.
            cs.MoveTo(0, 0);
            cs.LineTo(page.Width / 2, page.Height / 2);
            cs.StrokePath();

            // Draw the text again
            cs.BeginText();
            cs.SetTextRendering(PdfTextRenderingMode.FillText);
            cs.SetTextMatrix(1, 0, 0, 1, 100, 150);
            cs.SetTextFontAndSize(helvetica, helvetica.Size);

            // This text will appear ok.
            binaryText = helvetica.EncodeString("Hello World");
            cs.ShowText(new PdfCosBinaryString(binaryText));
            cs.EndText();

            // Restore the visual coordinate system
            cs.RestoreVisualCoordinateSystem();

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.contentstream.pdf") };
            return output;
        }
    }
}