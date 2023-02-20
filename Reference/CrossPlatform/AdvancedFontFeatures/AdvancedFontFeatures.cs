using System;
using System.IO;
using Xfinium.Pdf.Core;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Graphics.FormattedContent;
using Xfinium.Pdf.Spatial;

namespace Xfinium.Pdf.Samples
{
    class AdvancedFontFeatures
    {
        static void Main(string[] args)
        {
            PdfTrueTypeFontFeatures fontFeatures = new PdfTrueTypeFontFeatures();
            fontFeatures.EnableStandardLigatures = true;
            fontFeatures.EnableVerticalGlyphs = true;
            fontFeatures.EnableSmallCapsForLowercase = true;
            fontFeatures.EnableSmallCapsForUppercase = true;
            fontFeatures.EnableOldStyleFigures = true;
            PdfUnicodeTrueTypeFont ttf = new PdfUnicodeTrueTypeFont("..\\..\\..\\..\\..\\..\\SupportFiles\\calibri.ttf", 24, true, fontFeatures);
            PdfBrush blackBrush = new PdfBrush(PdfRgbColor.Black);

            PdfFixedDocument document = new PdfFixedDocument();

            PdfPage page = document.Pages.Add();
            DisplayStandardLigatures(page, blackBrush, ttf);

            page = document.Pages.Add();
            DisplayVerticalGlyphs(page, blackBrush, ttf);

            page = document.Pages.Add();
            DisplaySmallCapitals(page, blackBrush);

            page = document.Pages.Add();
            DisplayOldStyleFigures(page, blackBrush);

            document.Save("AdvancedFontFeatures.pdf");

            Console.WriteLine("File saved with success to current folder.");
        }

        public static void DisplayStandardLigatures(PdfPage page, PdfBrush blackBrush, PdfUnicodeTrueTypeFont font)
        {
            font.FontFeatures.EnableStandardLigatures = true;
            page.Graphics.DrawString("Standard ligatures enabled:", font, blackBrush, 50, 50);
            page.Graphics.DrawString("f f i - ffi", font, blackBrush, 50, 75);
            page.Graphics.DrawString("f i - fi", font, blackBrush, 50, 100);
            page.Graphics.DrawString("f l - fl", font, blackBrush, 50, 125);

            font.FontFeatures.EnableStandardLigatures = false;
            page.Graphics.DrawString("Standard ligatures disabled:", font, blackBrush, 50, 200);
            page.Graphics.DrawString("f f i - ffi", font, blackBrush, 50, 225);
            page.Graphics.DrawString("f i - fi", font, blackBrush, 50, 250);
            page.Graphics.DrawString("f l - fl", font, blackBrush, 50, 275);
        }

        public static void DisplayVerticalGlyphs(PdfPage page, PdfBrush blackBrush, PdfUnicodeTrueTypeFont font)
        {
            PdfTrueTypeFontFeatures fontFeatures = new PdfTrueTypeFontFeatures();
            fontFeatures.EnableVerticalGlyphs = true;
            // File NotoSansCJKjp-Regular.ttf is very large and it has not been included in the install kit.
            // It can be downloaded here: https://xfiniumpdf.com/downoad/samples/NotoSansCJKjp-Regular.ttf
            PdfUnicodeTrueTypeFont ttf = new PdfUnicodeTrueTypeFont("..\\..\\..\\..\\..\\..\\SupportFiles\\NotoSansCJKjp-Regular.ttf", 48, true, fontFeatures);

            ttf.FontFeatures.EnableVerticalGlyphs = false;
            page.Graphics.DrawString("Horizontal text:", font, blackBrush, 50, 75);
            page.Graphics.DrawString("\uFF08\u3303\uFF09", ttf, blackBrush, 50, 100);

            ttf.FontFeatures.EnableVerticalGlyphs = true;
            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Brush = blackBrush;
            sao.Font = ttf;
            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.X = 50;
            slo.Y = 185;
            slo.Width = 9999;
            slo.Height = 9999;
            page.Graphics.DrawString("Vertical text (vertical glyphs enabled):", font, blackBrush, 50, 175);
            page.Graphics.DrawString("\uFF08\n\u3303\n\uFF09", sao, slo);

            ttf.FontFeatures.EnableVerticalGlyphs = false;
            slo.Y = 375;
            page.Graphics.DrawString("Vertical text (vertical glyphs disabled):", font, blackBrush, 50, 350);
            page.Graphics.DrawString("\uFF08\n\u3303\n\uFF09", sao, slo);
        }

        public static void DisplaySmallCapitals(PdfPage page, PdfBrush blackBrush)
        {
            PdfTrueTypeFontFeatures fontFeatures = new PdfTrueTypeFontFeatures();
            fontFeatures.EnableSmallCapsForLowercase = true;
            fontFeatures.EnableSmallCapsForUppercase = true;
            PdfUnicodeTrueTypeFont font = new PdfUnicodeTrueTypeFont("..\\..\\..\\..\\..\\..\\SupportFiles\\arial.ttf", 24, true, fontFeatures);

            font.FontFeatures.EnableSmallCapsForUppercase = false;
            page.Graphics.DrawString("UPPERCASE - REGULAR", font, blackBrush, 50, 75);
            font.FontFeatures.EnableSmallCapsForUppercase = true;
            page.Graphics.DrawString("UPPERCASE - SMALL CAPS", font, blackBrush, 50, 105);

            font.FontFeatures.EnableSmallCapsForUppercase = false;
            font.FontFeatures.EnableSmallCapsForLowercase = false;
            page.Graphics.DrawString("Lowercase - Regular", font, blackBrush, 50, 150);
            font.FontFeatures.EnableSmallCapsForLowercase = true;
            page.Graphics.DrawString("Lowercase - Small Caps", font, blackBrush, 50, 180);

        }

        public static void DisplayOldStyleFigures(PdfPage page, PdfBrush blackBrush)
        {
            PdfTrueTypeFontFeatures fontFeatures = new PdfTrueTypeFontFeatures();
            fontFeatures.EnableOldStyleFigures = true;
            PdfUnicodeTrueTypeFont font = new PdfUnicodeTrueTypeFont("..\\..\\..\\..\\..\\..\\SupportFiles\\arial.ttf", 24, true, fontFeatures);

            font.FontFeatures.EnableOldStyleFigures = true;
            page.Graphics.DrawString("0123456789 - old style figures", font, blackBrush, 50, 70);
            font.FontFeatures.EnableOldStyleFigures = false;
            page.Graphics.DrawString("0123456789 - default figures", font, blackBrush, 50, 105);
        }

    }
}
