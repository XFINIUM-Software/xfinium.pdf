using System;
using System.IO;
using Xfinium.Pdf.Core;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Graphics.FormattedContent;

namespace Xfinium.Pdf.Samples
{
    class SVGFont
    {
        static void Main(string[] args)
        {
            PdfTrueTypeFontFeatures fontFeatures = new PdfTrueTypeFontFeatures();
            fontFeatures.EnableColorGlyphs = true;
            PdfStandardFont titlefont = new PdfStandardFont(PdfStandardFontFace.Helvetica, 12);
            PdfUnicodeTrueTypeFont svgTtf = new PdfUnicodeTrueTypeFont("..\\..\\..\\..\\..\\..\\SupportFiles\\DigitaltsOrange.ttf", 24, true, fontFeatures);
            PdfBrush blackBrush = new PdfBrush(PdfRgbColor.Black);
            PdfBrush darkRedBrush = new PdfBrush(PdfRgbColor.DarkRed);

            PdfFixedDocument document = new PdfFixedDocument();
            PdfPage page = document.Pages.Add();

            PdfType3Font svgType3 = new PdfType3Font(svgTtf);
            svgType3.Size = 24;
            svgType3.CreateGlyphFromUnicodeCodePoint((byte)'C', 'C');
            svgType3.CreateGlyphFromUnicodeCodePoint((byte)'r', 'r');
            svgType3.CreateGlyphFromUnicodeCodePoint((byte)'e', 'e');
            svgType3.CreateGlyphFromUnicodeCodePoint((byte)'a', 'a');
            svgType3.CreateGlyphFromUnicodeCodePoint((byte)'t', 't');
            svgType3.CreateGlyphFromUnicodeCodePoint((byte)'d', 'd');
            svgType3.CreateGlyphFromUnicodeCodePoint((byte)' ', ' ');
            svgType3.CreateGlyphFromUnicodeCodePoint((byte)'w', 'w');
            svgType3.CreateGlyphFromUnicodeCodePoint((byte)'i', 'i');
            svgType3.CreateGlyphFromUnicodeCodePoint((byte)'h', 'h');
            svgType3.CreateGlyphFromUnicodeCodePoint((byte)'X', 'X');
            svgType3.CreateGlyphFromUnicodeCodePoint((byte)'F', 'F');
            svgType3.CreateGlyphFromUnicodeCodePoint((byte)'I', 'I');
            svgType3.CreateGlyphFromUnicodeCodePoint((byte)'N', 'N');
            svgType3.CreateGlyphFromUnicodeCodePoint((byte)'U', 'U');
            svgType3.CreateGlyphFromUnicodeCodePoint((byte)'M', 'M');
            svgType3.CreateGlyphFromUnicodeCodePoint((byte)'.', '.');
            svgType3.CreateGlyphFromUnicodeCodePoint((byte)'P', 'P');
            svgType3.CreateGlyphFromUnicodeCodePoint((byte)'D', 'D');

            // Full SVG glyph appearance
            page.Graphics.DrawString("Full SVG glyph appearance (text color is given in SVG, brush has no effect)", titlefont, blackBrush, 50, 75);
            page.Graphics.DrawString("Created with XFINIUM.PDF", svgType3, darkRedBrush, 50, 90);


            // Standard TrueType glyph appearance
            page.Graphics.DrawString("Standard TrueType glyph appearance (text color is given by the brush)", titlefont, blackBrush, 50, 150);
            page.Graphics.DrawString("Created with XFINIUM.PDF", svgTtf, darkRedBrush, 50, 165);

            document.Save("SVGFont.pdf");

            Console.WriteLine("File saved with success to current folder.");
        }

    }
}
