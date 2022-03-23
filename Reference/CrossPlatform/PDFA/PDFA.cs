using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Graphics.OptionalContent;
using Xfinium.Pdf.Standards;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// PDF/A sample.
    /// </summary>
    public class PDFA
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream iccInput, Stream ttfInput)
        {
            PdfFixedDocument pdfa1bDocument = CreatePdfA1bDocument(iccInput, ttfInput);
            PdfFixedDocument pdfa2uDocument = CreatePdfA2uDocument(iccInput, ttfInput);
            PdfFixedDocument pdfa3uDocument = CreatePdfA3uDocument(iccInput, ttfInput);

            // The document is formatted as PDF/A using the PdfAFormatter class:
            // PdfAFormatter.Save(document, outputStream, PdfAFormat.PdfA1b);
            SampleOutputInfo[] output = new SampleOutputInfo[] 
                {
                    new SampleOutputInfo(pdfa1bDocument, "xfinium.pdf.sample.pdfa1b.pdf"),
                    new SampleOutputInfo(pdfa2uDocument, "xfinium.pdf.sample.pdfa2u.pdf"),
                    new SampleOutputInfo(pdfa3uDocument, "xfinium.pdf.sample.pdfa3u.pdf"),
                };
            return output;
        }

        private static PdfFixedDocument CreatePdfA1bDocument(Stream iccInput, Stream ttfInput)
        {
            iccInput.Position = 0;
            ttfInput.Position = 0;

            PdfFixedDocument document = new PdfFixedDocument();

            // Setup the document by creating a PDF/A output intent, based on a RGB ICC profile, 
            // and document information and metadata
            PdfIccColorSpace icc = new PdfIccColorSpace();
            byte[] profilePayload = new byte[iccInput.Length];
            iccInput.Read(profilePayload, 0, profilePayload.Length);
            icc.IccProfile = profilePayload;
            PdfOutputIntent oi = new PdfOutputIntent();
            oi.Type = PdfOutputIntentType.PdfA1;
            oi.Info = "sRGB IEC61966-2.1";
            oi.OutputConditionIdentifier = "Custom";
            oi.DestinationOutputProfile = icc;
            document.OutputIntents = new PdfOutputIntentCollection();
            document.OutputIntents.Add(oi);

            document.DocumentInformation = new PdfDocumentInformation();
            document.DocumentInformation.Author = "XFINIUM Software";
            document.DocumentInformation.Title = "XFINIUM.PDF PDF/A-1B Demo";
            document.DocumentInformation.Creator = "XFINIUM.PDF PDF/A-1B Demo";
            document.DocumentInformation.Producer = "XFINIUM.PDF";
            document.DocumentInformation.Keywords = "pdf/a";
            document.DocumentInformation.Subject = "PDF/A-1B Sample produced by XFINIUM.PDF";
            document.XmpMetadata = new PdfXmpMetadata();

            PdfPage page = document.Pages.Add();
            page.Rotation = 90;

            // All fonts must be embedded in a PDF/A document.
            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Font = new PdfAnsiTrueTypeFont(ttfInput, 24, true);
            sao.Brush = new PdfBrush(new PdfRgbColor(0, 0, 128));

            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.HorizontalAlign = PdfStringHorizontalAlign.Center;
            slo.VerticalAlign = PdfStringVerticalAlign.Bottom;
            slo.X = page.Width / 2;
            slo.Y = page.Height / 2 - 10;
            page.Graphics.DrawString("XFINIUM.PDF", sao, slo);
            slo.Y = page.Height / 2 + 10;
            slo.VerticalAlign = PdfStringVerticalAlign.Top;
            sao.Font.Size = 16;
            page.Graphics.DrawString("This is a sample PDF/A-1B document that shows the PDF/A-1B capabilities in XFINIUM.PDF library", sao, slo);

            return document;
        }

        private static PdfFixedDocument CreatePdfA2uDocument(Stream iccInput, Stream ttfInput)
        {
            iccInput.Position = 0;
            ttfInput.Position = 0;

            PdfFixedDocument document = new PdfFixedDocument();
            document.OptionalContentProperties = new PdfOptionalContentProperties();

            // Setup the document by creating a PDF/A output intent, based on a RGB ICC profile, 
            // and document information and metadata
            PdfIccColorSpace icc = new PdfIccColorSpace();
            byte[] profilePayload = new byte[iccInput.Length];
            iccInput.Read(profilePayload, 0, profilePayload.Length);
            icc.IccProfile = profilePayload;
            PdfOutputIntent oi = new PdfOutputIntent();
            oi.Type = PdfOutputIntentType.PdfA1;
            oi.Info = "sRGB IEC61966-2.1";
            oi.OutputConditionIdentifier = "Custom";
            oi.DestinationOutputProfile = icc;
            document.OutputIntents = new PdfOutputIntentCollection();
            document.OutputIntents.Add(oi);

            document.DocumentInformation = new PdfDocumentInformation();
            document.DocumentInformation.Author = "XFINIUM Software";
            document.DocumentInformation.Title = "XFINIUM.PDF PDF/A-2B/U Demo";
            document.DocumentInformation.Creator = "XFINIUM.PDF PDF/A-2B/U Demo";
            document.DocumentInformation.Producer = "XFINIUM.PDF";
            document.DocumentInformation.Keywords = "pdf/a";
            document.DocumentInformation.Subject = "PDF/A-2B/U Sample produced by XFINIUM.PDF";
            document.XmpMetadata = new PdfXmpMetadata();

            PdfPage page = document.Pages.Add();
            page.Rotation = 90;

            // All fonts must be embedded in a PDF/A document.
            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Font = new PdfAnsiTrueTypeFont(ttfInput, 24, true);
            sao.Brush = new PdfBrush(new PdfRgbColor(0, 0, 128));

            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.HorizontalAlign = PdfStringHorizontalAlign.Center;
            slo.VerticalAlign = PdfStringVerticalAlign.Bottom;
            slo.X = page.Width / 2;
            slo.Y = page.Height / 2 - 10;
            page.Graphics.DrawString("XFINIUM.PDF", sao, slo);
            slo.Y = page.Height / 2 + 10;
            slo.VerticalAlign = PdfStringVerticalAlign.Top;
            sao.Font.Size = 14;
            page.Graphics.DrawString("This is a sample PDF/A-2B/U document that shows the PDF/A-2B/U capabilities in XFINIUM.PDF library", sao, slo);

            // PDF/A-2 documents support optional content and transparency.
            PdfOptionalContentGroup ocgPage1 = new PdfOptionalContentGroup();
            ocgPage1.Name = "Green Rectangle";
            page.Graphics.BeginOptionalContentGroup(ocgPage1);
            page.Graphics.SaveGraphicsState();
            PdfExtendedGraphicState gs = new PdfExtendedGraphicState();
            gs.FillAlpha = 0.8;
            gs.StrokeAlpha = 0.2;
            gs.BlendMode = PdfBlendMode.Luminosity;
            page.Graphics.SetExtendedGraphicState(gs);

            PdfBrush greenBrush = new PdfBrush(PdfRgbColor.DarkGreen);
            PdfPen bluePen = new PdfPen(PdfRgbColor.DarkBlue, 5);
            page.Graphics.DrawRectangle(bluePen, greenBrush, 20, 20, page.Width - 40, page.Height - 40);

            page.Graphics.RestoreGraphicsState();
            page.Graphics.EndOptionalContentGroup();

            // Build the display tree for the optional content, 
            // how its structure and relationships between optional content groups are presented to the user.
            PdfOptionalContentDisplayTreeNode ocgPage1Node = new PdfOptionalContentDisplayTreeNode(ocgPage1);
            document.OptionalContentProperties.DisplayTree.Nodes.Add(ocgPage1Node);

            return document;
        }

        private static PdfFixedDocument CreatePdfA3uDocument(Stream iccInput, Stream ttfInput)
        {
            iccInput.Position = 0;
            ttfInput.Position = 0;

            PdfFixedDocument document = new PdfFixedDocument();
            document.OptionalContentProperties = new PdfOptionalContentProperties();

            // Setup the document by creating a PDF/A output intent, based on a RGB ICC profile, 
            // and document information and metadata
            PdfIccColorSpace icc = new PdfIccColorSpace();
            byte[] profilePayload = new byte[iccInput.Length];
            iccInput.Read(profilePayload, 0, profilePayload.Length);
            icc.IccProfile = profilePayload;
            PdfOutputIntent oi = new PdfOutputIntent();
            oi.Type = PdfOutputIntentType.PdfA1;
            oi.Info = "sRGB IEC61966-2.1";
            oi.OutputConditionIdentifier = "Custom";
            oi.DestinationOutputProfile = icc;
            document.OutputIntents = new PdfOutputIntentCollection();
            document.OutputIntents.Add(oi);

            document.DocumentInformation = new PdfDocumentInformation();
            document.DocumentInformation.Author = "XFINIUM Software";
            document.DocumentInformation.Title = "XFINIUM.PDF PDF/A-3B/U Demo";
            document.DocumentInformation.Creator = "XFINIUM.PDF PDF/A-3B/U Demo";
            document.DocumentInformation.Producer = "XFINIUM.PDF";
            document.DocumentInformation.Keywords = "pdf/a";
            document.DocumentInformation.Subject = "PDF/A-3B/U Sample produced by XFINIUM.PDF";
            document.XmpMetadata = new PdfXmpMetadata();

            PdfPage page = document.Pages.Add();
            page.Rotation = 90;

            // All fonts must be embedded in a PDF/A document.
            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Font = new PdfAnsiTrueTypeFont(ttfInput, 24, true);
            sao.Brush = new PdfBrush(new PdfRgbColor(0, 0, 128));

            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.HorizontalAlign = PdfStringHorizontalAlign.Center;
            slo.VerticalAlign = PdfStringVerticalAlign.Bottom;
            slo.X = page.Width / 2;
            slo.Y = page.Height / 2 - 10;
            page.Graphics.DrawString("XFINIUM.PDF", sao, slo);
            slo.Y = page.Height / 2 + 10;
            slo.VerticalAlign = PdfStringVerticalAlign.Top;
            sao.Font.Size = 14;
            page.Graphics.DrawString("This is a sample PDF/A-3B/U document that shows the PDF/A-3B/U capabilities in XFINIUM.PDF library", sao, slo);

            // PDF/A-3 documents support optional content and transparency.
            PdfOptionalContentGroup ocgPage1 = new PdfOptionalContentGroup();
            ocgPage1.Name = "Green Rectangle";
            page.Graphics.BeginOptionalContentGroup(ocgPage1);
            page.Graphics.SaveGraphicsState();
            PdfExtendedGraphicState gs = new PdfExtendedGraphicState();
            gs.FillAlpha = 0.8;
            gs.StrokeAlpha = 0.2;
            gs.BlendMode = PdfBlendMode.Luminosity;
            page.Graphics.SetExtendedGraphicState(gs);

            PdfBrush greenBrush = new PdfBrush(PdfRgbColor.DarkGreen);
            PdfPen bluePen = new PdfPen(PdfRgbColor.DarkBlue, 5);
            page.Graphics.DrawRectangle(bluePen, greenBrush, 20, 20, page.Width - 40, page.Height - 40);

            page.Graphics.RestoreGraphicsState();
            page.Graphics.EndOptionalContentGroup();

            // Build the display tree for the optional content, 
            // how its structure and relationships between optional content groups are presented to the user.
            PdfOptionalContentDisplayTreeNode ocgPage1Node = new PdfOptionalContentDisplayTreeNode(ocgPage1);
            document.OptionalContentProperties.DisplayTree.Nodes.Add(ocgPage1Node);

            // PDF/A-3 files also support document attachments.
            // Include the TrueType font as an attachment.
            ttfInput.Position = 0;
            byte[] ttfData = new byte[ttfInput.Length];
            ttfInput.Read(ttfData, 0, ttfData.Length);
            PdfDocumentFileAttachment att = new PdfDocumentFileAttachment(ttfData);
            att.FileName = "verdana.ttf";
            att.Description = "Verdana Regular";
            att.MimeType = "application/octet-stream";
            document.FileAttachments.Add(att);

            return document;
        }
    }
}