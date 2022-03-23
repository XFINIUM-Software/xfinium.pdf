using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Graphics.Svg;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// Svg to Pdf conversion sample.
    /// </summary>
    public class SvgToPdf
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream input)
        {
            PdfFixedDocument document = new PdfFixedDocument();
            PdfPage page = document.Pages.Add();

            PdfSvgDrawing svg = new PdfSvgDrawing(input);
            page.Graphics.DrawFormXObject(svg, 20, 20, page.Width - 40, page.Width - 40);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.svgtopdf.pdf") };
            return output;
        }
    }
}