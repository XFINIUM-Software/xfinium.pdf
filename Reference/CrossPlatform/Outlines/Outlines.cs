using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Content;
using Xfinium.Pdf.Actions;
using Xfinium.Pdf.Destinations;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// Outlines sample.
    /// </summary>
    public class Outlines
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run()
        {
            PdfFixedDocument document = new PdfFixedDocument();
            document.DisplayMode = PdfDisplayMode.UseOutlines;

            PdfStandardFont helvetica = new PdfStandardFont(PdfStandardFontFace.Helvetica, 216);
            PdfBrush blackBrush = new PdfBrush();
            for (int i = 0; i < 10; i++)
            {
                PdfPage page = document.Pages.Add();
                page.Graphics.DrawString((i + 1).ToString(), helvetica, blackBrush, 50, 50);
            }

            PdfOutlineItem root = new PdfOutlineItem();
            root.Title = "Contents";
            root.VisualStyle = PdfOutlineItemVisualStyle.Bold;
            root.Color = new PdfRgbColor(255, 0, 0);
            document.Outline.Add(root);

            for (int i = 0; i < document.Pages.Count; i++)
            {
                // Create a destination to target page.
                PdfPageDirectDestination pageDestination = new PdfPageDirectDestination();
                pageDestination.Page = document.Pages[i];
                pageDestination.Top = 0;
                pageDestination.Left = 0;
                // Inherit current zoom
                pageDestination.Zoom = 0;

                // Create a go to action to be executed when the outline is clicked, 
                // the go to action goes to specified destination.
                PdfGoToAction gotoPage = new PdfGoToAction();
                gotoPage.Destination = pageDestination;

                // Create the outline in the table of contents
                PdfOutlineItem outline = new PdfOutlineItem();
                outline.Title = string.Format("Go to page {0}", i + 1);
                outline.VisualStyle = PdfOutlineItemVisualStyle.Italic;
                outline.Action = gotoPage;
                root.Items.Add(outline);
            }
            root.Expanded = true;

            // Create an outline that will launch a link in the browser.
            PdfUriAction uriAction = new PdfUriAction();
            uriAction.URI = "http://www.xfiniumsoft.com/";

            PdfOutlineItem webOutline = new PdfOutlineItem();
            webOutline.Title = "http://www.xfiniumsoft.com/";
            webOutline.Color = new PdfRgbColor(0, 0, 255);
            webOutline.Action = uriAction;
            document.Outline.Add(webOutline);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.outlines.pdf") };
            return output;
        }
    }
}