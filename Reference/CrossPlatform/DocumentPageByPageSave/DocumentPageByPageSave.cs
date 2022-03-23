using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Redaction;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// DocumentPageByPageSave sample.
    /// </summary>
    public class DocumentPageByPageSave
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream output)
        {
            PdfFixedDocument document = new PdfFixedDocument();

            // Init the save operation
            document.BeginSave(output);

            PdfRgbColor color = new PdfRgbColor();
            PdfBrush brush = new PdfBrush(color);
            Random rnd = new Random();

            for (int i = 0; i < 3; i++)
            {
                PdfPage page = document.Pages.Add();
                page.Width = 1000;
                page.Height = 1000;

                for (int y = 1; y <= page.Height; y++)
                {
                    for (int x = 0; x < page.Width; x++)
                    {
                        color.R = (byte)rnd.Next(256);
                        color.G = (byte)rnd.Next(256);
                        color.B = (byte)rnd.Next(256);

                        page.Graphics.DrawRectangle(brush, x, y - 1, 1, 1);
                    }

                    if ((y % 100) == 0)
                    {
                        // Compress the graphics that have been drawn so far and save them.
                        page.Graphics.Compress();
                        page.SaveGraphics();
                    }
                }

                // Close the page graphics and save the page.
                page.Graphics.CompressAndClose();
                page.Save();
            }

            // Finish the document.
            document.EndSave();

            return null;
        }
    }
}