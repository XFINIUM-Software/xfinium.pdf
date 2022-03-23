using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Actions;
using Xfinium.Pdf.Graphics.FormattedContent;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// FormattedContent sample.
    /// </summary>
    public class FormattedContent
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run()
        {
            string xfiniumPdfText = "XFINIUM.PDF";
            string paragraph1Block2Text = " library is a .NET/Xamarin library for cross-platform PDF development. Code written for ";
            string paragraph1Block4Text = " can be compiled on all supported platforms without changes. The library features a " +
                "wide range of capabilities, for both beginers and advanced PDF developers.";
            string paragraph2Block1Text = "The development style is based on fixed document model, where each page is created as needed " +
                "and content is placed at fixed position using a grid based layout.\r\n" +
                "This gives you access to all PDF features, whether you want to create a simple file " +
                "or you want to create a transparency knockout group at COS level, and lets you build more complex models on top of the library.";
            string paragraph3Block2Text = " has been developed entirely in C# and it is 100% managed code.";
            string paragraph4Block1Text = "With ";
            string paragraph4Block3Text = " you can port your PDF application logic to other platforms with zero effort which means faster time to market.";
            string paragraph5Block1Text = "Simple licensing per developer with royalty free distribution helps you keep your costs under control.";
            string paragraph6Block1Text = "SUPPORTED PLATFORMS";
            string paragraph7Block1Text = ".NET 4.0 and higher";
            string paragraph8Block1Text = "Windows Forms";
            string paragraph9Block1Text = "ASP.NET Webforms and MVC";
            string paragraph10Block1Text = "Console applications";
            string paragraph11Block1Text = "Windows services";
            string paragraph12Block1Text = "Mono";
            string paragraph13Block1Text = "WPF 4.0 & 4.5";
            string paragraph14Block1Text = ".NET Core 2.x / 3.x";
            string paragraph15Block1Text = ".NET Standard 2.x";
            string paragraph16Block1Text = ".NET 5 / 6";
            string paragraph17Block1Text = "Xamarin.iOS";
            string paragraph18Block1Text = "Xamarin.Android";

            PdfStandardFont textFont = new PdfStandardFont(PdfStandardFontFace.Helvetica, 10);
            PdfFormattedTextBlock xfiniumPdfLinkBlock = new PdfFormattedTextBlock(xfiniumPdfText);
            xfiniumPdfLinkBlock.Font = new PdfStandardFont(PdfStandardFontFace.HelveticaBold, 10);
            xfiniumPdfLinkBlock.Font.Underline = true;
            xfiniumPdfLinkBlock.TextColor = new PdfBrush(PdfRgbColor.Blue);
            xfiniumPdfLinkBlock.Action = new PdfUriAction("http://xfiniumpdf.com/");

            PdfFormattedParagraph paragraph1 = new PdfFormattedParagraph();
            paragraph1.LineSpacingMode = PdfFormattedParagraphLineSpacing.Multiple;
            paragraph1.LineSpacing = 1.2;
            paragraph1.SpacingAfter = 3;
            paragraph1.FirstLineIndent = 10;
            paragraph1.HorizontalAlign = PdfStringHorizontalAlign.Justified;
            paragraph1.Blocks.Add(xfiniumPdfLinkBlock);
            paragraph1.Blocks.Add(new PdfFormattedTextBlock(paragraph1Block2Text, textFont));
            paragraph1.Blocks.Add(xfiniumPdfLinkBlock);
            paragraph1.Blocks.Add(new PdfFormattedTextBlock(paragraph1Block4Text, textFont));

            PdfFormattedParagraph paragraph2 = new PdfFormattedParagraph();
            paragraph2.SpacingAfter = 3;
            paragraph2.FirstLineIndent = 10;
            paragraph2.HorizontalAlign = PdfStringHorizontalAlign.Justified;
            paragraph2.LineSpacingMode = PdfFormattedParagraphLineSpacing.Multiple;
            paragraph2.LineSpacing = 1.2;
            paragraph2.Blocks.Add(new PdfFormattedTextBlock(paragraph2Block1Text, textFont));

            PdfFormattedParagraph paragraph3 = new PdfFormattedParagraph();
            paragraph3.LineSpacingMode = PdfFormattedParagraphLineSpacing.Multiple;
            paragraph3.LineSpacing = 1.2;
            paragraph3.SpacingAfter = 3;
            paragraph3.FirstLineIndent = 10;
            paragraph3.HorizontalAlign = PdfStringHorizontalAlign.Justified;
            paragraph3.Blocks.Add(xfiniumPdfLinkBlock);
            paragraph3.Blocks.Add(new PdfFormattedTextBlock(paragraph3Block2Text, textFont));

            PdfFormattedParagraph paragraph4 = new PdfFormattedParagraph();
            paragraph4.LineSpacingMode = PdfFormattedParagraphLineSpacing.Multiple;
            paragraph4.LineSpacing = 1.2;
            paragraph4.SpacingAfter = 3;
            paragraph4.FirstLineIndent = 10;
            paragraph4.HorizontalAlign = PdfStringHorizontalAlign.Justified;
            paragraph4.Blocks.Add(new PdfFormattedTextBlock(paragraph4Block1Text, textFont));
            paragraph4.Blocks.Add(xfiniumPdfLinkBlock);
            paragraph4.Blocks.Add(new PdfFormattedTextBlock(paragraph4Block3Text, textFont));

            PdfFormattedParagraph paragraph5 = new PdfFormattedParagraph();
            paragraph5.FirstLineIndent = 10;
            paragraph5.HorizontalAlign = PdfStringHorizontalAlign.Justified;
            paragraph5.Blocks.Add(new PdfFormattedTextBlock(paragraph5Block1Text, textFont));

            PdfFormattedParagraph paragraph6 = new PdfFormattedParagraph();
            paragraph6.SpacingBefore = 10;
            paragraph6.Blocks.Add(new PdfFormattedTextBlock(paragraph6Block1Text, textFont));

            PdfFormattedTextBlock bulletBlock = new PdfFormattedTextBlock("\x76 ", new PdfStandardFont(PdfStandardFontFace.ZapfDingbats, 10));

            PdfFormattedParagraph paragraph7 = new PdfFormattedParagraph();
            paragraph7.SpacingBefore = 3;
            paragraph7.Bullet = bulletBlock;
            paragraph7.LeftIndentation = 10;
            paragraph7.Blocks.Add(new PdfFormattedTextBlock(paragraph7Block1Text, textFont));

            PdfFormattedParagraph paragraph8 = new PdfFormattedParagraph();
            paragraph8.SpacingBefore = 3;
            paragraph8.Bullet = bulletBlock;
            paragraph8.LeftIndentation = 10;
            paragraph8.Blocks.Add(new PdfFormattedTextBlock(paragraph8Block1Text, textFont));

            PdfFormattedParagraph paragraph9 = new PdfFormattedParagraph();
            paragraph9.SpacingBefore = 3;
            paragraph9.Bullet = bulletBlock;
            paragraph9.LeftIndentation = 10;
            paragraph9.Blocks.Add(new PdfFormattedTextBlock(paragraph9Block1Text, textFont));

            PdfFormattedParagraph paragraph10 = new PdfFormattedParagraph();
            paragraph10.SpacingBefore = 3;
            paragraph10.Bullet = bulletBlock;
            paragraph10.LeftIndentation = 10;
            paragraph10.Blocks.Add(new PdfFormattedTextBlock(paragraph10Block1Text, textFont));

            PdfFormattedParagraph paragraph11 = new PdfFormattedParagraph();
            paragraph11.SpacingBefore = 3;
            paragraph11.Bullet = bulletBlock;
            paragraph11.LeftIndentation = 10;
            paragraph11.Blocks.Add(new PdfFormattedTextBlock(paragraph11Block1Text, textFont));

            PdfFormattedParagraph paragraph12 = new PdfFormattedParagraph();
            paragraph12.SpacingBefore = 3;
            paragraph12.Bullet = bulletBlock;
            paragraph12.LeftIndentation = 10;
            paragraph12.Blocks.Add(new PdfFormattedTextBlock(paragraph12Block1Text, textFont));

            PdfFormattedParagraph paragraph13 = new PdfFormattedParagraph();
            paragraph13.SpacingBefore = 3;
            paragraph13.Bullet = bulletBlock;
            paragraph13.LeftIndentation = 10;
            paragraph13.Blocks.Add(new PdfFormattedTextBlock(paragraph13Block1Text, textFont));

            PdfFormattedParagraph paragraph14 = new PdfFormattedParagraph();
            paragraph14.SpacingBefore = 3;
            paragraph14.Bullet = bulletBlock;
            paragraph14.LeftIndentation = 10;
            paragraph14.Blocks.Add(new PdfFormattedTextBlock(paragraph14Block1Text, textFont));

            PdfFormattedParagraph paragraph15 = new PdfFormattedParagraph();
            paragraph15.SpacingBefore = 3;
            paragraph15.Bullet = bulletBlock;
            paragraph15.LeftIndentation = 10;
            paragraph15.Blocks.Add(new PdfFormattedTextBlock(paragraph15Block1Text, textFont));

            PdfFormattedParagraph paragraph16 = new PdfFormattedParagraph();
            paragraph16.SpacingBefore = 3;
            paragraph16.Bullet = bulletBlock;
            paragraph16.LeftIndentation = 10;
            paragraph16.Blocks.Add(new PdfFormattedTextBlock(paragraph16Block1Text, textFont));

            PdfFormattedParagraph paragraph17 = new PdfFormattedParagraph();
            paragraph17.SpacingBefore = 3;
            paragraph17.Bullet = bulletBlock;
            paragraph17.LeftIndentation = 10;
            paragraph17.Blocks.Add(new PdfFormattedTextBlock(paragraph17Block1Text, textFont));

            PdfFormattedParagraph paragraph18 = new PdfFormattedParagraph();
            paragraph18.SpacingBefore = 3;
            paragraph18.Bullet = bulletBlock;
            paragraph18.LeftIndentation = 10;
            paragraph18.Blocks.Add(new PdfFormattedTextBlock(paragraph18Block1Text, textFont));

            PdfFormattedContent formattedContent = new PdfFormattedContent();
            formattedContent.Paragraphs.Add(paragraph1);
            formattedContent.Paragraphs.Add(paragraph2);
            formattedContent.Paragraphs.Add(paragraph3);
            formattedContent.Paragraphs.Add(paragraph4);
            formattedContent.Paragraphs.Add(paragraph5);
            formattedContent.Paragraphs.Add(paragraph6);
            formattedContent.Paragraphs.Add(paragraph7);
            formattedContent.Paragraphs.Add(paragraph8);
            formattedContent.Paragraphs.Add(paragraph9);
            formattedContent.Paragraphs.Add(paragraph10);
            formattedContent.Paragraphs.Add(paragraph11);
            formattedContent.Paragraphs.Add(paragraph12);
            formattedContent.Paragraphs.Add(paragraph13);
            formattedContent.Paragraphs.Add(paragraph14);
            formattedContent.Paragraphs.Add(paragraph15);
            formattedContent.Paragraphs.Add(paragraph16);
            formattedContent.Paragraphs.Add(paragraph17);
            formattedContent.Paragraphs.Add(paragraph18);

            PdfFixedDocument document = new PdfFixedDocument();
            PdfPage page = document.Pages.Add();

            page.Graphics.DrawFormattedContent(formattedContent, 50, 50, 500, 700);
            page.Graphics.CompressAndClose();
			
            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.formattedcontent.pdf") };
            return output;
        }
    }
}