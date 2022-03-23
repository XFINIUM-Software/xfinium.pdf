using System;
using System.IO;
using Xfinium.Pdf.Rendering;

namespace Xfinium.Pdf.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream pdfStream = File.OpenRead("..\\..\\..\\..\\..\\..\\SupportFiles\\xfinium.pdf");
            PdfFixedDocument document = new PdfFixedDocument(pdfStream);
            pdfStream.Dispose();

            PdfRendererSettings settings = new PdfRendererSettings();
            settings.DpiX = 96;
            settings.DpiY = 96;

            for (int i = 0; i < document.Pages.Count; i++)
            {
                PdfPageRenderer renderer = new PdfPageRenderer(document.Pages[i]);
                FileStream pngStream = File.OpenWrite(string.Format("xfinium-{0}.png", i));
                renderer.ConvertPageToImage(pngStream, PdfPageImageFormat.Png, settings);
                pngStream.Flush();
                pngStream.Dispose();

                Console.WriteLine("Page {0} converted.", i);
            }
        }
    }
}
