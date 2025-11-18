using System;
using System.IO;
using Xfinium.Pdf.Graphics;

namespace Xfinium.Pdf.Samples
{
    class Transparency
    {
        static void Main(string[] args)
        {
            PdfFixedDocument document = new PdfFixedDocument();
            PdfPage page = document.Pages.Add();

            PdfPen redPen = new PdfPen(PdfRgbColor.Red, 20);
            PdfBrush blueBrush = new PdfBrush(PdfRgbColor.DarkBlue);

            page.Graphics.DrawRectangle(blueBrush, 290, 20, 20, 60);

            // Transparent strokes
            PdfExtendedGraphicState gs1 = new PdfExtendedGraphicState();
            gs1.StrokeAlpha = 0.5;

            page.Graphics.SaveGraphicsState();
            page.Graphics.SetExtendedGraphicState(gs1);
            page.Graphics.DrawLine(redPen, 50, 50, 550, 50);
            page.Graphics.RestoreGraphicsState();

            page.Graphics.DrawLine(redPen, 300, 100, 300, 200);

            // Transparent fills
            PdfExtendedGraphicState gs2 = new PdfExtendedGraphicState();
            gs2.FillAlpha = 0.5;

            page.Graphics.SaveGraphicsState();
            page.Graphics.SetExtendedGraphicState(gs2);
            page.Graphics.DrawRectangle(blueBrush, 50, 100, 500, 100);
            page.Graphics.RestoreGraphicsState();

            page.Graphics.DrawRectangle(blueBrush, 50, 350, 500, 100);
            // Transparent images
            page.Graphics.SaveGraphicsState();
            page.Graphics.SetExtendedGraphicState(gs2);
            using (FileStream tiffStream = File.OpenRead("..\\..\\..\\..\\..\\..\\SupportFiles\\cmyk.tif"))
            {
                page.Graphics.DrawImage(new PdfTiffImage(tiffStream), 50, 250, 500, 400);
            }
            page.Graphics.RestoreGraphicsState();

            document.Save("Transparency.pdf");

            Console.WriteLine("File saved with success to current folder.");
        }
    }
}
