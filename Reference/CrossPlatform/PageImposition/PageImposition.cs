using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Core;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// PageImposition sample.
    /// </summary>
    public class PageImposition
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream input)
        {
            PdfFile file = new PdfFile(input);
            PdfPageContent[] content = file.ExtractPageContent(0, file.PageCount - 1);
            file = null;

            PdfFixedDocument document = new PdfFixedDocument();
            PdfPage page1 = document.Pages.Add();
            // Draw the same page content 4 times on the new page, the content is scaled to half and flipped.
            page1.Graphics.DrawFormXObject(content[0], 
                0, 0, page1.Width / 2, page1.Height / 2);
            page1.Graphics.DrawFormXObject(content[0], 
                page1.Width / 2, 0, page1.Width / 2, page1.Height / 2, 0, PdfFlipDirection.VerticalFlip);
            page1.Graphics.DrawFormXObject(content[0],
                0, page1.Height / 2, page1.Width / 2, page1.Height / 2, 0, PdfFlipDirection.HorizontalFlip);
            page1.Graphics.DrawFormXObject(content[0], 
                page1.Width / 2, page1.Height / 2, page1.Width / 2, page1.Height / 2,
                0, PdfFlipDirection.VerticalFlip | PdfFlipDirection.HorizontalFlip);

            PdfPage page2 = document.Pages.Add();
            // Draw 3 pages on the new page.
            page2.Graphics.DrawFormXObject(content[0],
                0, 0, page2.Width / 2, page2.Height / 2);
            page2.Graphics.DrawFormXObject(content[1],
                page2.Width / 2, 0, page2.Width / 2, page2.Height / 2);
            page2.Graphics.DrawFormXObject(content[2],
                0, page2.Height, page2.Height / 2, page2.Width, 90);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.pageimposition.pdf") };
            return output;
        }
    }
}