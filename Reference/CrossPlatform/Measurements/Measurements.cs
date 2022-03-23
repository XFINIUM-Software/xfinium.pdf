using System;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Spatial;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// Measurements sample.
    /// </summary>
    public class Measurements
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run()
        {
            // Create the pdf document
            PdfFixedDocument document = new PdfFixedDocument();
            // Create a new page in the document
            PdfPage page = document.Pages.Add();

            PdfPen blackPen = new PdfPen(PdfRgbColor.Black, 1);
            PdfPen redPen = new PdfPen(PdfRgbColor.Red, 4);
            PdfPen greenPen = new PdfPen(PdfRgbColor.Green, 2);
            PdfBrush blackBrush = new PdfBrush(PdfRgbColor.Black);
            PdfStandardFont helvetica = new PdfStandardFont(PdfStandardFontFace.Helvetica, 16);

            // Draw viewport border.
            page.Graphics.DrawRectangle(blackPen, 50, 50, 500, 500);
            // Draw the line to be measured.
            page.Graphics.DrawLine(greenPen, 70, 70, 530, 530);
            // Draw point A (line start) in the viewport.
            page.Graphics.DrawLine(redPen, 60, 70, 80, 70);
            page.Graphics.DrawLine(redPen, 70, 60, 70, 80);
            // Draw point B (line end) in the viewport.
            page.Graphics.DrawLine(redPen, 520, 530, 540, 530);
            page.Graphics.DrawLine(redPen, 530, 520, 530, 540);

            page.Graphics.DrawString("A", helvetica, blackBrush, 85, 65);
            page.Graphics.DrawString("B", helvetica, blackBrush, 505, 525);
            page.Graphics.DrawString("Viewport", helvetica, blackBrush, 50, 560);
            helvetica.Size = 10;
            page.Graphics.DrawString(
                "Open the file with Adobe Acrobat and measure the distance from A to B using the Distance tool.", 
                helvetica, blackBrush, 50, 580);
            page.Graphics.DrawString("The measured distance should be 9 mi 186 ft 1 1/4 in.", 
                helvetica, blackBrush, 50, 590);

            // Create a viewport that matches the rectangle above.
            PdfViewport vp = new PdfViewport();
            vp.Name = "Sample viewport";
            PdfPoint ll = page.ConvertVisualPointToStandardPoint(new PdfPoint(50, 50));
            PdfPoint ur = page.ConvertVisualPointToStandardPoint(new PdfPoint(550, 550));
            vp.Bounds = new PdfStandardRectangle(ll, ur);

            // Add the viewport to the page
            page.Viewports = new PdfViewportCollection();
            page.Viewports.Add(vp);

            // Create a rectilinear measure for the viewport (CAD drawing for example).
            PdfRectilinearMeasure rlm = new PdfRectilinearMeasure();
            // Attach the measure to the viewport.
            vp.Measure = rlm;
            // Set the measure scale: 1 inch (72 points) in PDF corresponds to 1 mile
            rlm.ScaleRatio = "1 in = 1 mi";

            // Create a number format that controls the display of units for X axis.
            PdfNumberFormat xNumberFormat = new PdfNumberFormat();
            xNumberFormat.MeasureUnit = "mi";
            xNumberFormat.ConversionFactor = 1/72.0; // Conversion from user space units to miles
            xNumberFormat.FractionDisplay = PdfFractionDisplay.Decimal;
            xNumberFormat.Precision = 100000;
            rlm.X = new PdfNumberFormatCollection();
            rlm.X.Add(xNumberFormat);

            // Create a chain of number formats that control the display of units for distance.
            rlm.Distance = new PdfNumberFormatCollection();
            PdfNumberFormat miNumberFormat = new PdfNumberFormat();
            miNumberFormat.MeasureUnit = "mi";
            miNumberFormat.ConversionFactor = 1; // Initial unit is miles; no conversion needed
            rlm.Distance.Add(miNumberFormat);
            PdfNumberFormat ftNumberFormat = new PdfNumberFormat();
            ftNumberFormat.MeasureUnit = "ft";
            ftNumberFormat.ConversionFactor = 5280; // Conversion from miles to feet
            rlm.Distance.Add(ftNumberFormat);
            PdfNumberFormat inNumberFormat = new PdfNumberFormat();
            inNumberFormat.MeasureUnit = "in";
            inNumberFormat.ConversionFactor = 12; // Conversion from feet to inches
            inNumberFormat.FractionDisplay = PdfFractionDisplay.Fraction;
            inNumberFormat.Denominator = 8; // Fractions of inches rounded to nearest 1/8
            rlm.Distance.Add(inNumberFormat);

            // Create a number format that controls the display of units area.
            PdfNumberFormat areaNumberFormat = new PdfNumberFormat();
            areaNumberFormat.MeasureUnit = "acres";
            areaNumberFormat.ConversionFactor = 640; // Conversion from square miles to acres
            rlm.Area = new PdfNumberFormatCollection();
            rlm.Area.Add(xNumberFormat);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.measurements.pdf") };
            return output;
        }
    }
}