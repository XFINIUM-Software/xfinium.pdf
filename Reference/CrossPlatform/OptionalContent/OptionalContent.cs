using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Graphics.OptionalContent;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// OptionalContent sample.
    /// </summary>
    public class OptionalContent
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run()
        {
            PdfFixedDocument document = new PdfFixedDocument();
            document.OptionalContentProperties = new PdfOptionalContentProperties();

            PdfStandardFont helveticaBold  = new PdfStandardFont(PdfStandardFontFace.HelveticaBold, 18);
            PdfBrush blackBrush = new PdfBrush();
            PdfBrush greenBrush = new PdfBrush(PdfRgbColor.DarkGreen);
            PdfBrush yellowBrush = new PdfBrush(PdfRgbColor.Yellow);
            PdfPen bluePen = new PdfPen(PdfRgbColor.DarkBlue, 5);
            PdfPen redPen = new PdfPen(PdfRgbColor.DarkRed, 5);

            PdfPage page = document.Pages.Add();
            page.Graphics.DrawString("Simple optional content: the green rectangle", helveticaBold, blackBrush, 20, 50);

            PdfOptionalContentGroup ocgPage1 = new PdfOptionalContentGroup();
            ocgPage1.Name = "Page 1 - Green Rectangle";
            page.Graphics.BeginOptionalContentGroup(ocgPage1);
            page.Graphics.DrawRectangle(bluePen, greenBrush, 20, 100, 570, 400);
            page.Graphics.EndOptionalContentGroup();

            page = document.Pages.Add();
            page.Graphics.DrawString("Multipart optional content: the green rectangles", helveticaBold, blackBrush, 20, 50);

            PdfOptionalContentGroup ocgPage2 = new PdfOptionalContentGroup();
            ocgPage2.Name = "Page 2 - Green Rectangles";
            page.Graphics.BeginOptionalContentGroup(ocgPage2);
            page.Graphics.DrawRectangle(bluePen, greenBrush, 20, 200, 570, 200);
            page.Graphics.EndOptionalContentGroup();

            page.Graphics.DrawRectangle(redPen, yellowBrush, 250, 90, 110, 680);

            page.Graphics.BeginOptionalContentGroup(ocgPage2);
            page.Graphics.DrawRectangle(bluePen, greenBrush, 20, 500, 570, 200);
            page.Graphics.EndOptionalContentGroup();

            page = document.Pages.Add();
            page.Graphics.DrawString("Imbricated optional content: the green and yellow rectangles", helveticaBold, blackBrush, 20, 50);

            PdfOptionalContentGroup ocgPage31 = new PdfOptionalContentGroup();
            ocgPage31.Name = "Page 3 - Green Rectangle";
            page.Graphics.BeginOptionalContentGroup(ocgPage31);
            page.Graphics.DrawRectangle(bluePen, greenBrush, 20, 100, 570, 600);

            PdfOptionalContentGroup ocgPage32 = new PdfOptionalContentGroup();
            ocgPage32.Name = "Page 3 - Yellow Rectangle";
            page.Graphics.BeginOptionalContentGroup(ocgPage32);
            page.Graphics.DrawRectangle(redPen, yellowBrush, 100, 200, 400, 300);
            page.Graphics.EndOptionalContentGroup(); // ocgPage32

            page.Graphics.EndOptionalContentGroup(); // ocgPage31

            page = document.Pages.Add();
            page.Graphics.DrawString("Multipage optional content: the green rectangles on page 4 & 5", helveticaBold, blackBrush, 20, 50);

            PdfOptionalContentGroup ocgPage45 = new PdfOptionalContentGroup();
            ocgPage45.Name = "Page 4 & 5 - Green Rectangles";
            page.Graphics.BeginOptionalContentGroup(ocgPage45);
            page.Graphics.DrawRectangle(bluePen, greenBrush, 20, 200, 570, 200);
            page.Graphics.EndOptionalContentGroup();

            page.Graphics.DrawRectangle(redPen, yellowBrush, 250, 90, 110, 680);

            page.Graphics.BeginOptionalContentGroup(ocgPage45);
            page.Graphics.DrawRectangle(bluePen, greenBrush, 20, 500, 570, 200);
            page.Graphics.EndOptionalContentGroup();

            page = document.Pages.Add();
            page.Graphics.DrawString("Multipage optional content: continued", helveticaBold, blackBrush, 20, 50);

            page.Graphics.BeginOptionalContentGroup(ocgPage45);
            page.Graphics.DrawRectangle(bluePen, greenBrush, 20, 200, 570, 200);
            page.Graphics.EndOptionalContentGroup();

            page.Graphics.DrawRectangle(redPen, yellowBrush, 250, 90, 110, 680);

            page.Graphics.BeginOptionalContentGroup(ocgPage45);
            page.Graphics.DrawRectangle(bluePen, greenBrush, 20, 500, 570, 200);
            page.Graphics.EndOptionalContentGroup();

            // Build the display tree for the optional content, 
            // how its structure and relationships between optional content groups are presented to the user.
            PdfOptionalContentDisplayTreeNode ocgPage1Node = new PdfOptionalContentDisplayTreeNode(ocgPage1);
            document.OptionalContentProperties.DisplayTree.Nodes.Add(ocgPage1Node);
            PdfOptionalContentDisplayTreeNode ocgPage2Node = new PdfOptionalContentDisplayTreeNode(ocgPage2);
            document.OptionalContentProperties.DisplayTree.Nodes.Add(ocgPage2Node);
            PdfOptionalContentDisplayTreeNode ocgPage31Node = new PdfOptionalContentDisplayTreeNode(ocgPage31);
            document.OptionalContentProperties.DisplayTree.Nodes.Add(ocgPage31Node);
            PdfOptionalContentDisplayTreeNode ocgPage32Node = new PdfOptionalContentDisplayTreeNode(ocgPage32);
            ocgPage31Node.Nodes.Add(ocgPage32Node);
            PdfOptionalContentDisplayTreeNode ocgPage45Node = new PdfOptionalContentDisplayTreeNode(ocgPage45);
            document.OptionalContentProperties.DisplayTree.Nodes.Add(ocgPage45Node);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.optionalcontent.pdf") };
            return output;
        }
    }
}