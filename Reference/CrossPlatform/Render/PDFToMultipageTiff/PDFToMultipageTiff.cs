using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Rendering;
using Xfinium.Pdf.Rendering.Imaging;
using Xfinium.Pdf.Rendering.RenderingSurfaces;

namespace Xfinium.Pdf.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream pdfStream = File.OpenRead("..\\..\\..\\..\\..\\..\\SupportFiles\\xfinium.pdf");
            PdfFixedDocument document = new PdfFixedDocument(pdfStream);
            pdfStream.Dispose();
            PdfDocumentRenderer documentRenderer = new PdfDocumentRenderer(document);

            FileStream tiffStream = File.OpenWrite("xfinium.tif");
            PdfRendererSettings settings = new PdfRendererSettings(96, 96);
            // Specify a Black and White rendering surface to convert the pages to 1bpp B/W TIFF images with CCITT G4 compression.
            PdfBlackWhiteRenderingSurface rs = new PdfBlackWhiteRenderingSurface();
            rs.BinarizationFilter = new PdfFloydSteinbergDitheringFilter();
            settings.RenderingSurface = rs;
            documentRenderer.ConvertToMultipageImage(settings, tiffStream);

            tiffStream.Flush();
            tiffStream.Dispose();
        }
    }
}
