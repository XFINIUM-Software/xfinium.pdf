using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Actions;
using Xfinium.Pdf.FlowDocument;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Graphics.FormattedContent;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// SuperscriptSubscript sample.
    /// </summary>
    public class SuperscriptSubscript
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream verdanaFontStream)
        {
            PdfAnsiTrueTypeFont verdana = new PdfAnsiTrueTypeFont(verdanaFontStream, 36, true);

            PdfFlowDocument document = new PdfFlowDocument();

            PdfFlowContent superscriptSection = BuildSuperscript(verdana);
            document.AddContent(superscriptSection);

            PdfFlowContent subscriptSection = BuildSubscript(verdana);
            document.AddContent(subscriptSection);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.superscriptsubscript.pdf") };
            return output;
        }

        private static PdfFlowContent BuildSuperscript(PdfAnsiTrueTypeFont font)
        {
            PdfAnsiTrueTypeFont fontSuperscript = new PdfAnsiTrueTypeFont(font, 18);
            PdfAnsiTrueTypeFont fontRegular = new PdfAnsiTrueTypeFont(font, 12);

            PdfFormattedContent content = new PdfFormattedContent();
            PdfFlowTextContent flowText = new PdfFlowTextContent(content);

            PdfFormattedTextBlock titleBlock = new PdfFormattedTextBlock("Superscript text", fontRegular);
            content.Paragraphs.Add(new PdfFormattedParagraph(titleBlock));
            content.Paragraphs.Add(new PdfFormattedParagraph(" "));

            PdfFormattedTextBlock xBlock = new PdfFormattedTextBlock("X", font);
            xBlock.Superscript.Add(new PdfFormattedTextBlock("2", fontSuperscript));
            PdfFormattedTextBlock yBlock = new PdfFormattedTextBlock(" + Y", font);
            yBlock.Superscript.Add(new PdfFormattedTextBlock("2", fontSuperscript));
            PdfFormattedTextBlock zBlock = new PdfFormattedTextBlock(" = Z", font);
            zBlock.Superscript.Add(new PdfFormattedTextBlock("2", fontSuperscript));

            PdfFormattedParagraph paragraph = new PdfFormattedParagraph(xBlock, yBlock, zBlock);
            paragraph.HorizontalAlign = PdfStringHorizontalAlign.Center;
            content.Paragraphs.Add(paragraph);

            return flowText;
        }

        private static PdfFlowContent BuildSubscript(PdfAnsiTrueTypeFont font)
        {
            PdfAnsiTrueTypeFont fontSubscript = new PdfAnsiTrueTypeFont(font, 18);
            PdfAnsiTrueTypeFont fontRegular = new PdfAnsiTrueTypeFont(font, 12);

            PdfFormattedContent content = new PdfFormattedContent();
            PdfFlowTextContent flowText = new PdfFlowTextContent(content);
            flowText.OuterMargins = new PdfFlowContentMargins(0, 0, 36, 0);

            PdfFormattedTextBlock titleBlock = new PdfFormattedTextBlock("Subscript text", fontRegular);
            content.Paragraphs.Add(new PdfFormattedParagraph(titleBlock));
            content.Paragraphs.Add(new PdfFormattedParagraph(" "));

            PdfFormattedTextBlock block1 = new PdfFormattedTextBlock("Y = X", font);
            block1.Subscript.Add(new PdfFormattedTextBlock("1", fontSubscript));
            PdfFormattedTextBlock block2 = new PdfFormattedTextBlock(" + X", font);
            block2.Subscript.Add(new PdfFormattedTextBlock("2", fontSubscript));
            PdfFormattedTextBlock block3 = new PdfFormattedTextBlock(" + X", font);
            block3.Subscript.Add(new PdfFormattedTextBlock("3", fontSubscript));
            PdfFormattedTextBlock blockn = new PdfFormattedTextBlock(" + ... + X", font);
            blockn.Subscript.Add(new PdfFormattedTextBlock("n", fontSubscript));

            PdfFormattedParagraph paragraph = new PdfFormattedParagraph(block1, block2, block3, blockn);
            paragraph.HorizontalAlign = PdfStringHorizontalAlign.Center;
            content.Paragraphs.Add(paragraph);

            return flowText;
        }
    }
}