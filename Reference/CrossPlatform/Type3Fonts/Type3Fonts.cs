using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// Type3 fonts sample.
    /// </summary>
    public class Type3Fonts
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run()
        {
            PdfFixedDocument document = new PdfFixedDocument();
            PdfPage page = document.Pages.Add();

            PdfStandardFont helvetica = new PdfStandardFont(PdfStandardFontFace.Helvetica, 20);
            PdfBrush blackBrush = new PdfBrush(PdfRgbColor.Black);
            page.Graphics.DrawString("The digits below, from 0 to 9, are drawn using a Type3 font.", helvetica, blackBrush, 50, 100);

            PdfType3Font t3 = new PdfType3Font("DemoT3");
            t3.Size = 24;
            t3.FirstChar = (byte)' ';
            t3.LastChar = (byte)'9';
            t3.FontMatrix = new PdfMatrix(0.01, 0, 0, 0.01, 0, 0);
            double[] widths = new double[] { 100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };
            t3.Widths = widths;

            PdfPen hollowPen = new PdfPen(null, 8);
            PdfBrush hollowBrush = new PdfBrush(null);
            // space
            PdfType3Glyph t3s = new PdfType3Glyph(0x20, new PdfSize(100, 100));
            t3.Glyphs.Add(t3s);
            // 0
            PdfType3Glyph t30 = new PdfType3Glyph(0x30, new PdfSize(100, 100));
            t30.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90);
            t30.Graphics.CompressAndClose();
            t3.Glyphs.Add(t30);
            // 1
            PdfType3Glyph t31 = new PdfType3Glyph(0x31, new PdfSize(100, 100));
            t31.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90);
            t31.Graphics.DrawEllipse(hollowBrush, 40, 40, 20, 20);
            t31.Graphics.CompressAndClose();
            t3.Glyphs.Add(t31);
            // 2
            PdfType3Glyph t32 = new PdfType3Glyph(0x32, new PdfSize(100, 100));
            t32.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90);
            t32.Graphics.DrawEllipse(hollowBrush, 15, 15, 20, 20);
            t32.Graphics.DrawEllipse(hollowBrush, 65, 65, 20, 20);
            t32.Graphics.CompressAndClose();
            t3.Glyphs.Add(t32);
            // 3
            PdfType3Glyph t33 = new PdfType3Glyph(0x33, new PdfSize(100, 100));
            t33.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90);
            t33.Graphics.DrawEllipse(hollowBrush, 15, 15, 20, 20);
            t33.Graphics.DrawEllipse(hollowBrush, 40, 40, 20, 20);
            t33.Graphics.DrawEllipse(hollowBrush, 65, 65, 20, 20);
            t33.Graphics.CompressAndClose();
            t3.Glyphs.Add(t33);
            // 4
            PdfType3Glyph t34 = new PdfType3Glyph(0x34, new PdfSize(100, 100));
            t34.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90);
            t34.Graphics.DrawEllipse(hollowBrush, 15, 15, 20, 20);
            t34.Graphics.DrawEllipse(hollowBrush, 65, 15, 20, 20);
            t34.Graphics.DrawEllipse(hollowBrush, 15, 65, 20, 20);
            t34.Graphics.DrawEllipse(hollowBrush, 65, 65, 20, 20);
            t34.Graphics.CompressAndClose();
            t3.Glyphs.Add(t34);
            // 5
            PdfType3Glyph t35 = new PdfType3Glyph(0x35, new PdfSize(100, 100));
            t35.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90);
            t35.Graphics.DrawEllipse(hollowBrush, 15, 15, 20, 20);
            t35.Graphics.DrawEllipse(hollowBrush, 65, 15, 20, 20);
            t35.Graphics.DrawEllipse(hollowBrush, 40, 40, 20, 20);
            t35.Graphics.DrawEllipse(hollowBrush, 15, 65, 20, 20);
            t35.Graphics.DrawEllipse(hollowBrush, 65, 65, 20, 20);
            t35.Graphics.CompressAndClose();
            t3.Glyphs.Add(t35);
            // 6
            PdfType3Glyph t36 = new PdfType3Glyph(0x36, new PdfSize(100, 100));
            t36.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90);
            t36.Graphics.DrawEllipse(hollowBrush, 15, 15, 20, 20);
            t36.Graphics.DrawEllipse(hollowBrush, 65, 15, 20, 20);
            t36.Graphics.DrawEllipse(hollowBrush, 15, 40, 20, 20);
            t36.Graphics.DrawEllipse(hollowBrush, 65, 40, 20, 20);
            t36.Graphics.DrawEllipse(hollowBrush, 15, 65, 20, 20);
            t36.Graphics.DrawEllipse(hollowBrush, 65, 65, 20, 20);
            t36.Graphics.CompressAndClose();
            t3.Glyphs.Add(t36);
            // 7
            PdfType3Glyph t37 = new PdfType3Glyph(0x37, new PdfSize(100, 100));
            t37.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90);
            t37.Graphics.DrawEllipse(hollowBrush, 15, 15, 20, 20);
            t37.Graphics.DrawEllipse(hollowBrush, 65, 15, 20, 20);
            t37.Graphics.DrawEllipse(hollowBrush, 15, 40, 20, 20);
            t37.Graphics.DrawEllipse(hollowBrush, 40, 40, 20, 20);
            t37.Graphics.DrawEllipse(hollowBrush, 65, 40, 20, 20);
            t37.Graphics.DrawEllipse(hollowBrush, 15, 65, 20, 20);
            t37.Graphics.DrawEllipse(hollowBrush, 65, 65, 20, 20);
            t37.Graphics.CompressAndClose();
            t3.Glyphs.Add(t37);
            // 8
            PdfType3Glyph t38 = new PdfType3Glyph(0x38, new PdfSize(100, 100));
            t38.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90);
            t38.Graphics.DrawEllipse(hollowBrush, 15, 15, 20, 20);
            t38.Graphics.DrawEllipse(hollowBrush, 40, 15, 20, 20);
            t38.Graphics.DrawEllipse(hollowBrush, 65, 15, 20, 20);
            t38.Graphics.DrawEllipse(hollowBrush, 15, 40, 20, 20);
            t38.Graphics.DrawEllipse(hollowBrush, 65, 40, 20, 20);
            t38.Graphics.DrawEllipse(hollowBrush, 15, 65, 20, 20);
            t38.Graphics.DrawEllipse(hollowBrush, 40, 65, 20, 20);
            t38.Graphics.DrawEllipse(hollowBrush, 65, 65, 20, 20);
            t38.Graphics.CompressAndClose();
            t3.Glyphs.Add(t38);
            // 9
            PdfType3Glyph t39 = new PdfType3Glyph(0x39, new PdfSize(100, 100));
            t39.Graphics.DrawRectangle(hollowPen, 5, 5, 90, 90);
            t39.Graphics.DrawEllipse(hollowBrush, 15, 15, 20, 20);
            t39.Graphics.DrawEllipse(hollowBrush, 40, 15, 20, 20);
            t39.Graphics.DrawEllipse(hollowBrush, 65, 15, 20, 20);
            t39.Graphics.DrawEllipse(hollowBrush, 15, 40, 20, 20);
            t39.Graphics.DrawEllipse(hollowBrush, 40, 40, 20, 20);
            t39.Graphics.DrawEllipse(hollowBrush, 65, 40, 20, 20);
            t39.Graphics.DrawEllipse(hollowBrush, 15, 65, 20, 20);
            t39.Graphics.DrawEllipse(hollowBrush, 40, 65, 20, 20);
            t39.Graphics.DrawEllipse(hollowBrush, 65, 65, 20, 20);
            t39.Graphics.CompressAndClose();
            t3.Glyphs.Add(t39);

            PdfBrush paleVioletRedbrush = new PdfBrush(PdfRgbColor.PaleVioletRed);
            page.Graphics.DrawString("0 1 2 3 4 5 6 7 8 9", t3, paleVioletRedbrush, 50, 150);
            PdfBrush midnightBluebrush = new PdfBrush(PdfRgbColor.MidnightBlue);
            page.Graphics.DrawString("0 1 2 3 4 5 6 7 8 9", t3, midnightBluebrush, 50, 200);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.type3fonts.pdf") };
            return output;
        }
    }
}