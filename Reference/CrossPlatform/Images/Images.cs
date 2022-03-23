using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// Images sample.
    /// </summary>
    public class Images
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        /// <param name="imageStream"></param>
        /// <param name="cmykImageStream"></param>
        /// <param name="softMaskStream"></param>
        /// <param name="stencilMaskStream"></param>
        public static SampleOutputInfo[] Run(Stream imageStream, Stream cmykImageStream, Stream softMaskStream, Stream stencilMaskStream)
        {
            PdfFixedDocument document = new PdfFixedDocument();
            PdfStandardFont helveticaBoldTitle = new PdfStandardFont(PdfStandardFontFace.HelveticaBold, 16);
            PdfStandardFont helveticaSection = new PdfStandardFont(PdfStandardFontFace.Helvetica, 10);

            PdfPage page = document.Pages.Add();
            DrawImages(page, imageStream, helveticaBoldTitle, helveticaSection);

            page = document.Pages.Add();
            DrawImageMasks(page, imageStream, softMaskStream, stencilMaskStream, helveticaBoldTitle, helveticaSection);

            page = document.Pages.Add();
            DrawCmykTiff(page, cmykImageStream, helveticaBoldTitle);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.images.pdf") };
            return output;
        }

        private static void DrawImages(PdfPage page, Stream imageStream, PdfFont titleFont, PdfFont sectionFont)
        {
            PdfBrush brush = new PdfBrush();

            PdfJpegImage jpeg = new PdfJpegImage(imageStream);

            page.Graphics.DrawString("Images", titleFont, brush, 20, 50);

            page.Graphics.DrawString("Scaling:", sectionFont, brush, 20, 70);

            // Draw the image 3 times on the page at different sizes.
            page.Graphics.DrawImage(jpeg, 3, 90, 100, 0);
            page.Graphics.DrawImage(jpeg, 106, 90, 200, 0);
            page.Graphics.DrawImage(jpeg, 309, 90, 300, 0);

            page.Graphics.DrawString("Flipping:", sectionFont, brush, 20, 320);
            page.Graphics.DrawImage(jpeg, 20, 340, 260, 0);
            page.Graphics.DrawImage(jpeg, 310, 340, 260, 0, 0, PdfFlipDirection.VerticalFlip);
            page.Graphics.DrawImage(jpeg, 20, 550, 260, 0, 0, PdfFlipDirection.HorizontalFlip);
            page.Graphics.DrawImage(jpeg, 310, 550, 260, 0, 0, PdfFlipDirection.VerticalFlip | PdfFlipDirection.HorizontalFlip);

            page.Graphics.CompressAndClose();
        }

        private static void DrawImageMasks(PdfPage page, Stream imageStream, Stream softMaskStream, Stream stencilMaskStream, PdfFont titleFont, PdfFont sectionFont)
        {
            PdfBrush brush = new PdfBrush();

            page.Graphics.DrawString("Images Masks", titleFont, brush, 20, 50);

            page.Graphics.DrawString("Soft mask:", sectionFont, brush, 20, 70);
            PdfPngImage softMaskImage = new PdfPngImage(softMaskStream);
            PdfSoftMask softMask = new PdfSoftMask(softMaskImage);
            imageStream.Position = 0;
            PdfJpegImage softMaskJpeg = new PdfJpegImage(imageStream);
            softMaskJpeg.Mask = softMask;
            // Draw the image with a soft mask.
            page.Graphics.DrawImage(softMaskJpeg, 20, 90, 280, 0);

            page.Graphics.DrawString("Stencil mask:", sectionFont, brush, 320, 70);
            PdfPngImage stencilMaskImage = new PdfPngImage(stencilMaskStream);
            PdfStencilMask stencilMask = new PdfStencilMask(stencilMaskImage);
            imageStream.Position = 0;
            PdfJpegImage stencilMaskJpeg = new PdfJpegImage(imageStream);
            stencilMaskJpeg.Mask = stencilMask;
            // Draw the image with a stencil mask.
            page.Graphics.DrawImage(stencilMaskJpeg, 320, 90, 280, 0);

            page.Graphics.DrawString("Stencil mask painting:", sectionFont, brush, 20, 320);
            PdfBrush redBrush = new PdfBrush(PdfRgbColor.DarkRed);
            page.Graphics.DrawStencilMask(stencilMask, redBrush, 20, 340, 280, 0);
            PdfBrush blueBrush = new PdfBrush(PdfRgbColor.DarkBlue);
            page.Graphics.DrawStencilMask(stencilMask, blueBrush, 320, 340, 280, 0);
            PdfBrush greenBrush = new PdfBrush(PdfRgbColor.DarkGreen);
            page.Graphics.DrawStencilMask(stencilMask, greenBrush, 20, 550, 280, 0);
            PdfBrush yellowBrush = new PdfBrush(PdfRgbColor.YellowGreen);
            page.Graphics.DrawStencilMask(stencilMask, yellowBrush, 320, 550, 280, 0);

            page.Graphics.CompressAndClose();
        }

        private static void DrawCmykTiff(PdfPage page, Stream cmykImageStream, PdfFont titleFont)
        {
            PdfBrush brush = new PdfBrush();

            page.Graphics.DrawString("CMYK TIFF", titleFont, brush, 20, 50);

            PdfTiffImage cmykTiff = new PdfTiffImage(cmykImageStream);
            page.Graphics.DrawImage(cmykTiff, 20, 90, 570, 0);

            page.Graphics.CompressAndClose();
        }
    }
}