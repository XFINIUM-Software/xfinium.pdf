using System;
using System.IO;
using Xfinium.Pdf.Rendering;

namespace Xfinium.Pdf.Samples
{
    class HighDpiPDFToImage
    {
        static void Main(string[] args)
        {
            FileStream pdfStream = File.OpenRead("..\\..\\..\\..\\..\\..\\SupportFiles\\xfinium.pdf");
            PdfFixedDocument document = new PdfFixedDocument(pdfStream);
            pdfStream.Dispose();

            PdfPageRenderer pageRenderer = new PdfPageRenderer(document.Pages[0]);

            PdfRendererSettings settings = new PdfRendererSettings(5200, 5200);
            settings.RenderingSurface = pageRenderer.CreateRenderingSurface<PdfArgbStripRenderingSurface<int>>(settings.DpiX, settings.DpiY);

            // Output will be a 32bit 60840x43160 pixels RGBA TIFF
            FileStream pngStream = File.Create("XFINIUM.PDF.Page.0.tiff");
            pageRenderer.ConvertPageToImage(pngStream, PdfPageImageFormat.Tiff, settings);
            pngStream.Flush();
            pngStream.Close();
            
            Console.WriteLine("Page 0 converted.");
        }
    }
}
