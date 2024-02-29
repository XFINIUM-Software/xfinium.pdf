using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Rendering;
using Xfinium.Pdf.Rendering.RenderingSurfaces;

namespace Xfinium.Pdf.Samples
{
    class Layers
    {
        public static void Main(string[] args)
        {
            FileStream pdfStream = File.OpenRead("..\\..\\..\\..\\..\\..\\..\\SupportFiles\\layers.pdf");
            PdfFixedDocument document = new PdfFixedDocument(pdfStream);
            pdfStream.Dispose();

            PdfPageRenderer pageRenderer = new PdfPageRenderer(document.Pages[0]);

            PdfRendererSettings settings = new PdfRendererSettings(96, 96);
            settings.RenderingSurface = pageRenderer.CreateRenderingSurface<PdfArgbRenderingSurface<int>>(settings.DpiX, settings.DpiY);

            FileStream imageStream = File.Create("Layers.AllContent.tiff");
            // By default all page content is rendered, layers visibility is ignored.
            pageRenderer.ConvertPageToImage(imageStream, PdfPageImageFormat.Tiff, settings);
            imageStream.Flush();
            imageStream.Close();

            // Render only the layers that are displayed when the document is viewed.
            settings.LayerRenderTarget = PdfLayerRenderTarget.View;
            imageStream = File.Create("Layers.View.tiff");
            pageRenderer.ConvertPageToImage(imageStream, PdfPageImageFormat.Tiff, settings);
            imageStream.Flush();
            imageStream.Close();

            // Render only the layers that are displayed when the document is printed.
            settings.LayerRenderTarget = PdfLayerRenderTarget.Print;
            imageStream = File.Create("Layers.Print.tiff");
            pageRenderer.ConvertPageToImage(imageStream, PdfPageImageFormat.Tiff, settings);
            imageStream.Flush();
            imageStream.Close();

            Console.WriteLine("Layers sample completed.");
        }
    }
}
