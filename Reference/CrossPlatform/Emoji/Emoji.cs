using System;
using System.IO;
using Xfinium.Pdf.Core;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Graphics.FormattedContent;

namespace Xfinium.Pdf.Samples
{
    class Emoji
    {
        static void Main(string[] args)
        {
            PdfTrueTypeFontFeatures fontFeatures = new PdfTrueTypeFontFeatures();
            fontFeatures.EnableColorGlyphs = true;
            PdfUnicodeTrueTypeFont emojiTtf = new PdfUnicodeTrueTypeFont("..\\..\\..\\..\\..\\..\\SupportFiles\\SegoeUIEmoji.ttf", 24, true, fontFeatures);

            PdfFixedDocument document = new PdfFixedDocument();
            PdfPage page = document.Pages.Add();

            PdfType3Font emojiType3 = new PdfType3Font(emojiTtf);
            emojiType3.Size = 24;
            emojiType3.CreateGlyphFromUnicodeCodePoint((byte)'A', 0x1F382); // Birthday cake
            emojiType3.CreateGlyphFromUnicodeCodePoint((byte)'B', 0x1F389); // Party Popper
            emojiType3.CreateGlyphFromUnicodeCodePoint((byte)'C', 0x1F973); // Face With Party Horn And Party Hat
            emojiType3.CreateGlyphFromUnicodeCodePoint((byte)'D', 0x1F37E); // Bottle With Popping Cork

            // Full emoji appearance
            PdfFormattedContent fc1 = BuildTextContent(emojiTtf, emojiType3, "A", "BCD");
            page.Graphics.DrawFormattedContent(fc1, 0, 50, page.Width, page.Height);

            // Standard TrueType emoji appearance
            PdfFormattedContent fc2 = BuildTextContent(emojiTtf, emojiTtf, 
                char.ConvertFromUtf32(0x1F382), char.ConvertFromUtf32(0x1F389) + char.ConvertFromUtf32(0x1F973) + char.ConvertFromUtf32(0x1F37E));
            page.Graphics.DrawFormattedContent(fc2, 0, 200, page.Width, page.Height);

            document.Save("Emoji.pdf");

            Console.WriteLine("File saved with success to current folder.");
        }

        private static PdfFormattedContent BuildTextContent(PdfFont standardTextFont, PdfFont emojiFont, string emojiText1, string emojiText2)
        {
            PdfFormattedContent fc = new PdfFormattedContent();
            fc.Paragraphs.Add(new PdfFormattedTextBlock(emojiText1, emojiFont));
            fc.Paragraphs[0].HorizontalAlign = PdfStringHorizontalAlign.Center;
            fc.Paragraphs[0].SpacingAfter = 6;
            fc.Paragraphs.Add(new PdfFormattedTextBlock("Happy Birthday!", standardTextFont));
            fc.Paragraphs[1].HorizontalAlign = PdfStringHorizontalAlign.Center;
            fc.Paragraphs[1].SpacingAfter = 6;
            fc.Paragraphs.Add(new PdfFormattedTextBlock(emojiText2, emojiFont));
            fc.Paragraphs[2].HorizontalAlign = PdfStringHorizontalAlign.Center;

            return fc;
        }

    }
}
