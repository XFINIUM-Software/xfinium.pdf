using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Content;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// Watermarks sample.
    /// </summary>
    public class Watermarks
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream input)
        {
            // Load the input file.
            PdfFixedDocument document = new PdfFixedDocument(input);

            DrawWatermarkUnderPageContent(document.Pages[0]);

            DrawWatermarkOverPageContent(document.Pages[1]);

            DrawWatermarkWithTransparency(document.Pages[2]);

            // Compress the page graphic content.
            for (int i = 0; i < document.Pages.Count; i++)
            {
                document.Pages[i].Graphics.CompressAndClose();
            }

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.watermarks.pdf") };
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        private static void DrawWatermarkUnderPageContent(PdfPage page)
        {
            PdfBrush redBrush = new PdfBrush(new PdfRgbColor(192, 0, 0));
            PdfStandardFont helvetica = new PdfStandardFont(PdfStandardFontFace.HelveticaBold, 36);

            // Set the page graphics to be located under existing page content.
            page.SetGraphicsPosition(PdfPageGraphicsPosition.UnderExistingPageContent);

            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Brush = redBrush;
            sao.Font = helvetica;
            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.X = 130;
            slo.Y = 670;
            slo.Rotation = 60;
            page.Graphics.DrawString("Sample watermark under page content", sao, slo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        private static void DrawWatermarkOverPageContent(PdfPage page)
        {
            PdfBrush redBrush = new PdfBrush(new PdfRgbColor(192, 0, 0));
            PdfStandardFont helvetica = new PdfStandardFont(PdfStandardFontFace.HelveticaBold, 32);

            // The page graphics is located by default on top of existing page content.
            //page.SetGraphicsPosition(PdfPageGraphicsPosition.OverExistingPageContent);

            // Draw the watermark over page content.
            // Page content under the watermark will be masked.
            page.Graphics.DrawString("Sample watermark over page content", helvetica, redBrush, 20, 335);

            page.Graphics.SaveGraphicsState();

            // Draw the watermark over page content but using the Multiply blend mode.
            // The watermak will appear as if drawn under the page content, useful when watermarking scanned documents.
            // If the watermark is drawn under page content for scanned documents, it will not be visible because the scanned image will block it.
            PdfExtendedGraphicState gs1 = new PdfExtendedGraphicState();
            gs1.BlendMode = PdfBlendMode.Multiply;
            page.Graphics.SetExtendedGraphicState(gs1);
            page.Graphics.DrawString("Sample watermark over page content", helvetica, redBrush, 20, 385);

            // Draw the watermark over page content but using the Luminosity blend mode.
            // Both the page content and the watermark will be visible.
            PdfExtendedGraphicState gs2 = new PdfExtendedGraphicState();
            gs2.BlendMode = PdfBlendMode.Luminosity;
            page.Graphics.SetExtendedGraphicState(gs2);
            page.Graphics.DrawString("Sample watermark over page content", helvetica, redBrush, 20, 435);

            page.Graphics.RestoreGraphicsState();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        private static void DrawWatermarkWithTransparency(PdfPage page)
        {
            PdfBrush redBrush = new PdfBrush(new PdfRgbColor(192, 0, 0));
            PdfStandardFont helvetica = new PdfStandardFont(PdfStandardFontFace.HelveticaBold, 36);

            // The page graphics is located by default on top of existing page content.
            //page.SetGraphicsPosition(PdfPageGraphicsPosition.OverExistingPageContent);

            page.Graphics.SaveGraphicsState();

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
            page.Graphics.SetExtendedGraphicState(gs1);
            page.Graphics.DrawString("Sample watermark over page content", sao, slo);

            page.Graphics.RestoreGraphicsState();
        }
    }
}