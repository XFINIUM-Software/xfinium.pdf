using System;
using System.IO;
using Xfinium.Pdf.Core;
using Xfinium.Pdf.Graphics;

namespace Xfinium.Pdf.Samples
{
    class WatermarksLowOverhead
    {
        static void Main(string[] args)
        {
            FileStream pdfStream = File.OpenRead("..\\..\\..\\..\\..\\..\\SupportFiles\\content.pdf");
            PdfFileEx pdfFile = new PdfFileEx(pdfStream);
			pdfStream.Close();

            PdfPageGraphics pageCanvas = pdfFile.GetPageGraphics(0, PdfPageGraphicsPosition.UnderExistingPageContent);
            DrawWatermarkUnderPageContent(pageCanvas);
            pageCanvas.CompressAndClose();

            pageCanvas = pdfFile.GetPageGraphics(1, PdfPageGraphicsPosition.OverExistingPageContent);
            DrawWatermarkOverPageContent(pageCanvas);
            pageCanvas.CompressAndClose();

            pageCanvas = pdfFile.GetPageGraphics(2, PdfPageGraphicsPosition.OverExistingPageContent);
            DrawWatermarkWithTransparency(pageCanvas);
            pageCanvas.CompressAndClose();

            pdfFile.Save("WatermarksLowOverhead.pdf");

            Console.WriteLine("File saved with success to current folder.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageCanvas"></param>
        private static void DrawWatermarkUnderPageContent(PdfPageGraphics pageCanvas)
        {
            PdfBrush redBrush = new PdfBrush(new PdfRgbColor(192, 0, 0));
            PdfStandardFont helvetica = new PdfStandardFont(PdfStandardFontFace.HelveticaBold, 36);

            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Brush = redBrush;
            sao.Font = helvetica;
            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.X = 130;
            slo.Y = 670;
            slo.Rotation = 60;
            pageCanvas.DrawString("Sample watermark under page content", sao, slo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageCanvas"></param>
        private static void DrawWatermarkOverPageContent(PdfPageGraphics pageCanvas)
        {
            PdfBrush redBrush = new PdfBrush(new PdfRgbColor(192, 0, 0));
            PdfStandardFont helvetica = new PdfStandardFont(PdfStandardFontFace.HelveticaBold, 32);

            // Draw the watermark over page content.
            // Page content under the watermark will be masked.
            pageCanvas.DrawString("Sample watermark over page content", helvetica, redBrush, 20, 335);

            pageCanvas.SaveGraphicsState();

            // Draw the watermark over page content but using the Multiply blend mode.
            // The watermak will appear as if drawn under the page content, useful when watermarking scanned documents.
            // If the watermark is drawn under page content for scanned documents, it will not be visible because the scanned image will block it.
            PdfExtendedGraphicState gs1 = new PdfExtendedGraphicState();
            gs1.BlendMode = PdfBlendMode.Multiply;
            pageCanvas.SetExtendedGraphicState(gs1);
            pageCanvas.DrawString("Sample watermark over page content", helvetica, redBrush, 20, 385);

            // Draw the watermark over page content but using the Luminosity blend mode.
            // Both the page content and the watermark will be visible.
            PdfExtendedGraphicState gs2 = new PdfExtendedGraphicState();
            gs2.BlendMode = PdfBlendMode.Luminosity;
            pageCanvas.SetExtendedGraphicState(gs2);
            pageCanvas.DrawString("Sample watermark over page content", helvetica, redBrush, 20, 435);

            pageCanvas.RestoreGraphicsState();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageCanvas"></param>
        private static void DrawWatermarkWithTransparency(PdfPageGraphics pageCanvas)
        {
            PdfBrush redBrush = new PdfBrush(new PdfRgbColor(192, 0, 0));
            PdfStandardFont helvetica = new PdfStandardFont(PdfStandardFontFace.HelveticaBold, 36);

            pageCanvas.SaveGraphicsState();

            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Brush = redBrush;
            sao.Font = helvetica;
            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.X = 130;
            slo.Y = 670;
            slo.Rotation = 60;

            // Draw the watermark over page content but setting the transparency to a value lower than 1.
            // The page content will be partially visible through the watermark.
            PdfExtendedGraphicState gs1 = new PdfExtendedGraphicState();
            gs1.FillAlpha = 0.3;
            pageCanvas.SetExtendedGraphicState(gs1);
            pageCanvas.DrawString("Sample watermark over page content", sao, slo);

            pageCanvas.RestoreGraphicsState();
        }

    }
}
