using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// FileAttachments sample.
    /// </summary>
    public class FileAttachments
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream s1, Stream s2)
        {
            PdfFixedDocument document = new PdfFixedDocument();
            document.DisplayMode = PdfDisplayMode.UseAttachments;

            PdfStandardFont helvetica = new PdfStandardFont(PdfStandardFontFace.Helvetica, 16);
            PdfBrush blackBrush = new PdfBrush();
            PdfPage page = document.Pages.Add();
            page.Graphics.DrawString("This document contains 2 file attachments:", helvetica, blackBrush, 50, 50);
            page.Graphics.DrawString("1. fileattachments.cs.html", helvetica, blackBrush, 50, 70);
            page.Graphics.DrawString("2. fileattachments.vb.html", helvetica, blackBrush, 50, 90);

            byte[] fileData1 = new byte[s1.Length];
            s1.Read(fileData1, 0, fileData1.Length);
            PdfDocumentFileAttachment fileAttachment1 = new PdfDocumentFileAttachment();
            fileAttachment1.Payload = fileData1;
            fileAttachment1.FileName = "fileattachments.cs.html";
            fileAttachment1.Description = "C# Source Code for FileAttachments sample";
            document.FileAttachments.Add(fileAttachment1);

            byte[] fileData2 = new byte[s2.Length];
            s2.Read(fileData2, 0, fileData2.Length);
            PdfDocumentFileAttachment fileAttachment2 = new PdfDocumentFileAttachment();
            fileAttachment2.Payload = fileData1;
            fileAttachment2.FileName = "fileattachments.vb.html";
            fileAttachment2.Description = "VB.NET Source Code for FileAttachments sample";
            document.FileAttachments.Add(fileAttachment2);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.fileattachments.pdf") };
            return output;
        }
    }
}