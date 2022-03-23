using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Destinations;
using Xfinium.Pdf.Actions;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// DocumentAppend sample.
    /// </summary>
    public class DocumentAppend
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream file1Input, Stream file2Input)
        {
            PdfFixedDocument document = new PdfFixedDocument();

            // The documents are merged by creating an empty PDF document and appending the file to it.
            // The outlines from the source file are also included in the merged file.
            document.AppendFile(file1Input);
            int count = document.Pages.Count;
            document.AppendFile(file2Input);

            // Create outlines that point to each merged file.
            PdfOutlineItem file1Outline = CreateOutline("First file", document.Pages[0]);
            document.Outline.Add(file1Outline);
            PdfOutlineItem file2Outline = CreateOutline("Second file", document.Pages[count]);
            document.Outline.Add(file2Outline);

            // Optionally we can add a new page at the beginning of the merged document.
            PdfPage page = new PdfPage();
            document.Pages.Insert(0, page);

            PdfOutlineItem blankPageOutline = CreateOutline("Blank page", page);
            document.Outline.Insert(0, blankPageOutline);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.documentappend.pdf") };
            return output;
        }

        private static PdfOutlineItem CreateOutline(string title, PdfPage page)
        {
            PdfPageDirectDestination pageDestination = new PdfPageDirectDestination();
            pageDestination.Page = page;
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
            outline.Title = title;
            outline.VisualStyle = PdfOutlineItemVisualStyle.Italic;
            outline.Action = gotoPage;

            return outline;
        }
    }
}