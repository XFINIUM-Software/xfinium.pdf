using System;
using System.IO;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Rendering;

namespace Xfinium.Pdf.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream pdfStream = File.OpenRead("..\\..\\..\\..\\..\\..\\..\\SupportFiles\\xfinium.pdf");
            PdfFixedDocument document = new PdfFixedDocument(pdfStream);
            pdfStream.Dispose();

            PdfPageRenderer renderer = new PdfPageRenderer(document.Pages[0]);

            // Convert PDF page to fixed image size 1920x1080
            using (FileStream pngStream = File.OpenWrite("xfinium-1920x1080.png"))
            {
                PdfRendererSettings settings = new PdfRendererSettings(96, 96);
                settings.OutputImageSize = new PdfSize(1920, 1080);
                renderer.ConvertPageToImage(pngStream, PdfPageImageFormat.Png, settings);
                pngStream.Flush();
            }

            // Convert PDF page to image size 1920xProportionalHeight
            using (FileStream pngStream = File.OpenWrite("xfinium-1920xProportionalHeight.png"))
            {
                PdfRendererSettings settings = new PdfRendererSettings(96, 96);
                settings.OutputImageSize = new PdfSize(1920, 0);
                renderer.ConvertPageToImage(pngStream, PdfPageImageFormat.Png, settings);
                pngStream.Flush();
            }

            // Convert PDF page to image size ProportionalWidth x 1080
            using (FileStream pngStream = File.OpenWrite("xfinium-ProportionalWidthx1080.png"))
            {
                PdfRendererSettings settings = new PdfRendererSettings(96, 96);
                settings.OutputImageSize = new PdfSize(0, 1080);
                renderer.ConvertPageToImage(pngStream, PdfPageImageFormat.Png, settings);
                pngStream.Flush();
            }

            Console.WriteLine("PDFToFixedImageSize sample completed with success.");
        }
    }
}
