using System;
using System.IO;
using Xfinium.Pdf.Rendering;
using Xfinium.Pdf.Rendering.RenderingSurfaces;

namespace Xfinium.Pdf.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            PdfFixedDocument document = new PdfFixedDocument("..\\..\\..\\..\\..\\..\\..\\SupportFiles\\cluj.pdf");

            PdfRendererSettings settings = new PdfRendererSettings();
            settings.DpiX = 300;
            settings.DpiY = 300;
            PdfPageRenderer renderer = new PdfPageRenderer(document.Pages[0]);
            settings.RenderingSurface = renderer.CreateRenderingSurface<PdfRgbaRenderingSurface<byte>>(settings.DpiX, settings.DpiY);

            settings.RenderThreadCount = 0;
            RenderPage(renderer, settings);

            settings.RenderThreadCount = 2;
            RenderPage(renderer, settings);

            settings.RenderThreadCount = 4;
            RenderPage(renderer, settings);
        }

        private static void RenderPage(PdfPageRenderer renderer, PdfRendererSettings settings)
        {
            DateTime start, end;
            TimeSpan total = new TimeSpan();
            int runCount = 3;

            for (int i = 0; i < runCount; i++)
            {
                start = DateTime.Now;

                renderer.ConvertPageToImage(settings);

                end = DateTime.Now;
                total = total + (end - start);

                settings.RenderingSurface.Save(string.Format($"ThreadCount.{settings.RenderThreadCount}.Pass.{i + 1}.tif"), PdfPageImageFormat.Tiff);
            }

            Console.WriteLine($"Thread count: {settings.RenderThreadCount} - Runs: {runCount} - Average duration: {total / runCount}");

            Console.WriteLine();
        }
    }
}
