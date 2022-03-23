using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// Tiff to Pdf conversion sample.
    /// </summary>
    public class TiffToPdf
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream input)
        {
            PdfFixedDocument document = new PdfFixedDocument();
            PdfTiffImage tiff = new PdfTiffImage(input);

            for (int i = 0; i < tiff.FrameCount; i++)
            {
                tiff.ActiveFrame = i;
                PdfPage page = document.Pages.Add();
                page.Graphics.DrawImage(tiff, 0, 0, page.Width, page.Height);
            }

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.tifftopdf.pdf") };
            return output;
        }
    }
}