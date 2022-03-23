using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Actions;
using Xfinium.Pdf.Annotations;
using Xfinium.Pdf.Core.Cos;
using Xfinium.Pdf.Forms;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Graphics.FormattedContent;
using Xfinium.Pdf.LogicalStructure;
using Xfinium.Pdf.Standards;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// PDF/A sample.
    /// </summary>
    public class PDFUA
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo Run(Stream verdanaInput, Stream verdanaBoldInput, Stream imageStream)
        {
            PdfAnsiTrueTypeFont verdana = new PdfAnsiTrueTypeFont(verdanaInput, 10, true);
            PdfAnsiTrueTypeFont verdanaBold = new PdfAnsiTrueTypeFont(verdanaBoldInput, 10, true);

            PdfFixedDocument document = new PdfFixedDocument();
            document.Language = "en-US";

            document.DocumentInformation = new PdfDocumentInformation();
            document.DocumentInformation.Author = "XFINIUM Software";
            document.DocumentInformation.Title = "XFINIUM.PDF PDF/UA-1 Demo";
            document.DocumentInformation.Creator = "XFINIUM.PDF PDF/UA-1 Demo";
            document.DocumentInformation.Producer = "XFINIUM.PDF";
            document.DocumentInformation.Keywords = "pdf/ua";
            document.DocumentInformation.Subject = "PDF/UA-1 Sample produced by XFINIUM.PDF";
            document.XmpMetadata = new PdfXmpMetadata();

            document.ViewerPreferences = new PdfViewerPreferences();
            document.ViewerPreferences.DisplayDocumentTitle = true;

            document.MarkInformation = new PdfMarkInformation();
            document.MarkInformation.IsTaggedPdf = true;

            document.StructureTree = new PdfStructureTree();

            PdfStructureElement seDocument = new PdfStructureElement(PdfStandardStructureTypes.Document);
            seDocument.Title = "XFINIUM.PDF PDF/UA-1 Demo";
            document.StructureTree.StructureElements = seDocument;

            SimpleText(document, seDocument, verdana, imageStream);

            FormattedContent(document, seDocument, verdana);

            AnnotationsAndFormFields(document, seDocument, verdana);

            return new SampleOutputInfo(document, "xfinium.pdf.sample.pdfua1.pdf");
        }

        private static void SimpleText(PdfFixedDocument document, PdfStructureElement seParent, PdfAnsiTrueTypeFont font, Stream imageStream)
        {
            PdfAnsiTrueTypeFont headingFont = new PdfAnsiTrueTypeFont(font, 16);
            PdfAnsiTrueTypeFont textFont = new PdfAnsiTrueTypeFont(font, 10);
            PdfBrush blackBrush = new PdfBrush(PdfRgbColor.Black);
            PdfPen blackPen = new PdfPen(PdfRgbColor.Black, 1);
            PdfPage page = document.Pages.Add();

            PdfStructureElement seSection = new PdfStructureElement(PdfStandardStructureTypes.Section);
            seSection.Title = "Simple text";
            seParent.AppendChild(seSection);

            PdfStructureElement seHeading = new PdfStructureElement(PdfStandardStructureTypes.Heading);
            seSection.AppendChild(seHeading);

            page.Graphics.BeginStructureElement(seHeading);
            page.Graphics.DrawString("Page heading", headingFont, blackBrush, 30, 50);
            page.Graphics.EndStructureElement();

            PdfStructureElement seParagraph1 = new PdfStructureElement(PdfStandardStructureTypes.Paragraph);
            seSection.AppendChild(seParagraph1);

            page.Graphics.BeginStructureElement(seParagraph1);
            page.Graphics.DrawString("Sample paragraph. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris in sodales ligula at lobortis.", textFont, blackBrush, 30, 70);
            page.Graphics.EndStructureElement();

            PdfStructureElement seFigure = new PdfStructureElement(PdfStandardStructureTypes.Figure);
            seFigure.ActualText = "XFINIUM.PDF";
            seFigure.AlternateDescription = "XFINIUM.PDF Logo";
            seSection.AppendChild(seFigure);

            page.Graphics.BeginStructureElement(seFigure);
            PdfPngImage logoImage = new PdfPngImage(imageStream);
            page.Graphics.DrawImage(logoImage, 30, 90, 256, 128);
            page.Graphics.EndStructureElement();

            // A structure element with 2 content items and an artifact between them.
            PdfStructureElement seParagraph2 = new PdfStructureElement(PdfStandardStructureTypes.Paragraph);
            seSection.AppendChild(seParagraph2);

            page.Graphics.BeginStructureElement(seParagraph2);
            page.Graphics.DrawString("First line of text.", textFont, blackBrush, 30, 230);
            page.Graphics.EndStructureElement();

            page.Graphics.BeginArtifactMarkedContent();
            page.Graphics.DrawLine(blackPen, 30, 242, 100, 242);
            page.Graphics.EndMarkedContent();

            page.Graphics.BeginStructureElement(seParagraph2);
            page.Graphics.DrawString("Second line of text.", textFont, blackBrush, 30, 245);
            page.Graphics.EndStructureElement();

            PdfStructureElement seParagraph3 = new PdfStructureElement(PdfStandardStructureTypes.Paragraph);
            seSection.AppendChild(seParagraph3);

            string text = "Paragraph with underlined text. The structure element is passed as parameter to DrawString method in order to properly tag graphic artifacts such as underlines. " +
                "Morbi pulvinar eros sit amet diam lacinia, ut feugiat ligula bibendum. Suspendisse ultrices cursus condimentum. " +
                "Pellentesque semper iaculis luctus. Sed ac maximus urna. Aliquam erat volutpat. Vivamus vel sollicitudin dui. Aenean efficitur " +
                "fringilla dui, at varius lacus luctus ac. Quisque tellus dui, lacinia non lectus nec, semper faucibus erat.";
            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Brush = blackBrush;
            sao.Font = textFont;
            sao.Font.Underline = true;
            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.X = 30;
            slo.Y = 260;
            slo.Width = 550;
            page.Graphics.DrawString(text, sao, slo, seParagraph3);

            // A custom structure element that will be mapped to standard Pargraph structure.
            PdfStructureElement seSpecialParagraph = new PdfStructureElement("SpecialParagraph");
            // The structure element specifies an ID that needs to be added to document's IDMap.
            seSpecialParagraph.ID = "specialpara";
            seSection.AppendChild(seSpecialParagraph);

            page.Graphics.BeginStructureElement(seSpecialParagraph);
            textFont.Underline = false;
            textFont.Size = 18;
            page.Graphics.DrawString("A special paragraph with custom structure element type.", textFont, blackBrush, 30, 350);
            page.Graphics.EndStructureElement();

            // Map the custom structure type to a known structure type.
            document.StructureTree.RoleMap = new PdfRoleMap();
            document.StructureTree.RoleMap["SpecialParagraph"] = PdfStandardStructureTypes.Paragraph;

            // Include the ID of the structure element in the document's identifiers map.
            document.StructureTree.IdentifierMap = new PdfIdMap();
            document.StructureTree.IdentifierMap["specialpara"] = seSpecialParagraph;
        }

        private static void FormattedContent(PdfFixedDocument document, PdfStructureElement seParent, PdfAnsiTrueTypeFont font)
        {
            string text1 = "Morbi pulvinar eros sit amet diam lacinia, ut feugiat ligula bibendum. Suspendisse ultrices cursus condimentum. " +
                "Pellentesque semper iaculis luctus. Sed ac maximus urna. Aliquam erat volutpat. ";
            string text2 = "Vivamus vel sollicitudin dui. Aenean efficitur " +
                "fringilla dui, at varius lacus luctus ac. Quisque tellus dui, lacinia non lectus nec, semper faucibus erat.";

            PdfAnsiTrueTypeFont headingFont = new PdfAnsiTrueTypeFont(font, 16);
            PdfAnsiTrueTypeFont textFont = new PdfAnsiTrueTypeFont(font, 10);
            PdfBrush blackBrush = new PdfBrush(PdfRgbColor.Black);
            PdfPen blackPen = new PdfPen(PdfRgbColor.Black, 1);
            PdfPage page = document.Pages.Add();
            page.TabOrder = PdfPageTabOrder.Structure;

            PdfStructureElement seSection = new PdfStructureElement(PdfStandardStructureTypes.Section);
            seSection.Title = "Formatted content";
            seParent.AppendChild(seSection);

            PdfStructureElement seHeading = new PdfStructureElement(PdfStandardStructureTypes.Heading);
            seSection.AppendChild(seHeading);

            page.Graphics.BeginStructureElement(seHeading);
            page.Graphics.DrawString("Another heading", headingFont, blackBrush, 30, 50);
            page.Graphics.EndStructureElement();

            PdfFormattedContent fc = new PdfFormattedContent();

            PdfFormattedParagraph paragraph1 = new PdfFormattedParagraph(text1, textFont);
            paragraph1.LineSpacing = 1.2;
            paragraph1.LineSpacingMode = PdfFormattedParagraphLineSpacing.Multiple;
            paragraph1.StructureElement = new PdfStructureElement(PdfStandardStructureTypes.Paragraph);
            seSection.AppendChild(paragraph1.StructureElement);
            fc.Paragraphs.Add(paragraph1);

            PdfFormattedTextBlock block1 = new PdfFormattedTextBlock("Sample paragraph with a link.\r\n", textFont);
            block1.StructureElement = new PdfStructureElement(PdfStandardStructureTypes.Span);
            PdfFormattedTextBlock block2 = new PdfFormattedTextBlock("https://xfiniumpdf.com/\r\n", textFont);
            block2.TextColor = new PdfBrush(PdfRgbColor.Blue);
            block2.Action = new PdfUriAction("https://xfiniumpdf.com/");
            block2.StructureElement = new PdfStructureElement(PdfStandardStructureTypes.Link);
            PdfFormattedTextBlock block3 = new PdfFormattedTextBlock("https://pdf.dev/", textFont);
            block3.TextColor = new PdfBrush(PdfRgbColor.Blue);
            block3.Action = new PdfUriAction("https://pdf.dev/");
            block3.StructureElement = new PdfStructureElement(PdfStandardStructureTypes.Link);
            PdfFormattedParagraph paragraph2 = new PdfFormattedParagraph(block1, block2, block3);
            paragraph2.LineSpacing = 1.2;
            paragraph2.LineSpacingMode = PdfFormattedParagraphLineSpacing.Multiple;
            paragraph2.StructureElement = new PdfStructureElement(PdfStandardStructureTypes.Paragraph);
            // Do not mark the paragraph in the content stream, instead its blocks will be marked.
            paragraph2.StructureElement.GenerateMarkedContentSequence = false;
            paragraph2.StructureElement.AppendChild(block1.StructureElement);
            paragraph2.StructureElement.AppendChild(block2.StructureElement);
            paragraph2.StructureElement.AppendChild(block3.StructureElement);
            seSection.AppendChild(paragraph2.StructureElement);
            fc.Paragraphs.Add(paragraph2);

            PdfFormattedParagraph paragraph3 = new PdfFormattedParagraph(text2, textFont);
            paragraph3.LineSpacing = 1.2;
            paragraph3.LineSpacingMode = PdfFormattedParagraphLineSpacing.Multiple;
            paragraph3.StructureElement = new PdfStructureElement(PdfStandardStructureTypes.Paragraph);
            seSection.AppendChild(paragraph3.StructureElement);
            fc.Paragraphs.Add(paragraph3);

            page.Graphics.DrawFormattedContent(fc, 30, 70, 550, 0);
        }

        private static void AnnotationsAndFormFields(PdfFixedDocument document, PdfStructureElement seParent, PdfAnsiTrueTypeFont font)
        {
            PdfAnsiTrueTypeFont headingFont = new PdfAnsiTrueTypeFont(font, 16);
            PdfAnsiTrueTypeFont textFont = new PdfAnsiTrueTypeFont(font, 10);
            PdfBrush blackBrush = new PdfBrush(PdfRgbColor.Black);
            PdfPen blackPen = new PdfPen(PdfRgbColor.Black, 1);
            PdfPage page = document.Pages.Add();
            page.TabOrder = PdfPageTabOrder.Structure;

            PdfStructureElement seSection = new PdfStructureElement(PdfStandardStructureTypes.Section);
            seSection.Title = "Annotations and form fields";
            seParent.AppendChild(seSection);

            PdfStructureElement seHeading = new PdfStructureElement(PdfStandardStructureTypes.Heading);
            seSection.AppendChild(seHeading);

            page.Graphics.BeginStructureElement(seHeading);
            page.Graphics.DrawString("Annotations and form fields", headingFont, blackBrush, 30, 50);
            page.Graphics.EndStructureElement();

            PdfStructureElement seParagraph1 = new PdfStructureElement(PdfStandardStructureTypes.Paragraph);
            seSection.AppendChild(seParagraph1);

            page.Graphics.BeginStructureElement(seParagraph1);
            page.Graphics.DrawString("Our website:", textFont, blackBrush, 30, 70);
            page.Graphics.EndStructureElement();

            PdfStructureElement seLink = new PdfStructureElement(PdfStandardStructureTypes.Link);
            seParagraph1.AppendChild(seLink);

            page.Graphics.BeginStructureElement(seLink);
            page.Graphics.DrawString("https://xfiniumpdf.com/", textFont, blackBrush, 100, 70);
            page.Graphics.EndStructureElement();

            PdfLinkAnnotation link = new PdfLinkAnnotation();
            page.Annotations.Add(link);
            link.VisualRectangle = new PdfVisualRectangle(95, 70, 130, 10);
            link.HighlightStyle = PdfLinkAnnotationHighlightStyle.Invert;
            link.Contents = "https://xfiniumpdf.com/";

            PdfObjectReference linkRef = new PdfObjectReference();
            linkRef.Page = page;
            linkRef.Stream = link.CosDictionary as PdfCosDictionaryContainer;
            seLink.AppendChild(linkRef);
            document.StructureTree.MapObjectReference(link, seLink);

            PdfStructureElement seParagraph2 = new PdfStructureElement(PdfStandardStructureTypes.Paragraph);
            seSection.AppendChild(seParagraph2);

            page.Graphics.BeginStructureElement(seParagraph2);
            page.Graphics.DrawString("Enter your name:", textFont, blackBrush, 30, 100);
            page.Graphics.EndStructureElement();

            PdfStructureElement seForm = new PdfStructureElement(PdfStandardStructureTypes.Form);
            seParagraph2.AppendChild(seForm);

            PdfTextBoxField fldName = new PdfTextBoxField("name");
            page.Fields.Add(fldName);
            fldName.AlternateName = "Enter your name";
            fldName.Widgets[0].VisualRectangle = new PdfVisualRectangle(120, 95, 130, 20);
            fldName.Widgets[0].BorderColor = PdfRgbColor.Blue;
            fldName.Widgets[0].BorderWidth = 1;
            fldName.Widgets[0].BackgroundColor = PdfRgbColor.LightBlue;
            fldName.Widgets[0].Font = textFont;

            PdfObjectReference fieldRef = new PdfObjectReference();
            fieldRef.Page = page;
            fieldRef.Stream = fldName.Widgets[0].CosDictionary as PdfCosDictionaryContainer;
            seForm.AppendChild(fieldRef);
            document.StructureTree.MapObjectReference(fldName.Widgets[0], seForm);
        }
    }
}