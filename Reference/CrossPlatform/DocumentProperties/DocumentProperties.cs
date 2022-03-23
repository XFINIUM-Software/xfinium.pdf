using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// DocumentProperties sample.
    /// </summary>
    public class DocumentProperties
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run()
        {
            PdfFixedDocument document = new PdfFixedDocument();
            document.Pages.Add();
            // Display the document in full screen mode.
            document.DisplayMode = PdfDisplayMode.FullScreen;

            // Fill the document information.
            document.DocumentInformation = new PdfDocumentInformation();
            document.DocumentInformation.Author = "Xfinium Software";
            document.DocumentInformation.CreationDate = DateTime.Now;
            document.DocumentInformation.ModifyDate = DateTime.Now;
            document.DocumentInformation.Creator = "Xfinium.Pdf DocumentProperties sample";
            document.DocumentInformation.Producer = "Xfinium.Pdf";
            document.DocumentInformation.Title = "Xfinium.Pdf DocumentProperties sample";
            document.DocumentInformation.Subject = "Xfinium.Pdf sample code";
            document.DocumentInformation.Keywords = "xfinium.pdf,pdf,sample";

            // Set custom metadata in the XMP metadata.
            document.XmpMetadata = new PdfXmpMetadata();
            // This custom metadata will appear as a child of 'xmpmeta' root node.
            document.XmpMetadata.Metadata = "<custom>Custom metadata</custom>";

            // Set the viewer preferences.
            document.ViewerPreferences = new PdfViewerPreferences();
            document.ViewerPreferences.CenterWindow = true;
            document.ViewerPreferences.DisplayDocumentTitle = true;
            document.ViewerPreferences.HideMenubar = true;
            document.ViewerPreferences.HideToolbar = true;
            document.ViewerPreferences.HideWindowUI = true;
            document.ViewerPreferences.PrintScaling = PdfPrintScaling.None;

            // Set the PDF version.
            document.PdfVersion = PdfVersion.Version15;

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.documentproperties.pdf") };
            return output;
        }
    }
}