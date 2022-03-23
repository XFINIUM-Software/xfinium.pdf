using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.PdfFunctions;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// VectorGraphics sample.
    /// </summary>
    public class VectorGraphics
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        /// <param name="iccStream"></param>
        public static SampleOutputInfo[] Run(Stream iccStream)
        {
            PdfFixedDocument document = new PdfFixedDocument();
            PdfStandardFont helveticaBoldTitle = new PdfStandardFont(PdfStandardFontFace.HelveticaBold, 16);
            PdfStandardFont helveticaSection = new PdfStandardFont(PdfStandardFontFace.Helvetica, 10);

            PdfPage page = document.Pages.Add();
            DrawLines(page, helveticaBoldTitle, helveticaSection);

            page = document.Pages.Add();
            DrawRectangles(page, helveticaBoldTitle, helveticaSection);

            page = document.Pages.Add();
            DrawRoundRectangles(page, helveticaBoldTitle, helveticaSection);

            page = document.Pages.Add();
            DrawEllipses(page, helveticaBoldTitle, helveticaSection);

            page = document.Pages.Add();
            DrawArcsAndPies(page, helveticaBoldTitle, helveticaSection);

            page = document.Pages.Add();
            DrawBezierCurves(page, helveticaBoldTitle, helveticaSection);

            page = document.Pages.Add();
            DrawAffineTransformations(page, helveticaBoldTitle, helveticaSection);

            page = document.Pages.Add();
            DrawColorsAndColorSpaces(page, helveticaBoldTitle, helveticaSection, iccStream);

            page = document.Pages.Add();
            DrawShadings(page, helveticaBoldTitle, helveticaSection);

            page = document.Pages.Add();
            DrawPatterns(page, helveticaBoldTitle, helveticaSection);

            page = document.Pages.Add();
            DrawFormXObjects(page, helveticaBoldTitle, helveticaSection);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.vectorgraphics.pdf") };
            return output;
        }

        private static void DrawLines(PdfPage page, PdfFont titleFont, PdfFont sectionFont)
        {
            PdfBrush brush = new PdfBrush();
            PdfPen blackPen = new PdfPen(PdfRgbColor.Black, 1);
            PdfPen bluePen = new PdfPen(PdfRgbColor.LightBlue, 16);

            page.Graphics.DrawString("Lines", titleFont, brush, 20, 50);

            page.Graphics.DrawString("Line styles:", sectionFont, brush, 20, 70);
            page.Graphics.DrawString("Solid", sectionFont, brush, 20, 90);
            page.Graphics.DrawLine(blackPen, 100, 95, 400, 95);
            page.Graphics.DrawString("Dashed", sectionFont, brush, 20, 110);
            blackPen.DashPattern = new double[] { 3, 3 };
            page.Graphics.DrawLine(blackPen, 100, 115, 400, 115);

            page.Graphics.DrawString("Line cap styles:", sectionFont, brush, 20, 150);
            page.Graphics.DrawString("Flat", sectionFont, brush, 20, 175);
            page.Graphics.DrawLine(bluePen, 100, 180, 400, 180);
            blackPen.DashPattern = null;
            page.Graphics.DrawLine(blackPen, 100, 180, 400, 180);
            page.Graphics.DrawString("Square", sectionFont, brush, 20, 195);
            bluePen.LineCap = PdfLineCap.Square;
            page.Graphics.DrawLine(bluePen, 100, 200, 400, 200);
            blackPen.DashPattern = null;
            page.Graphics.DrawLine(blackPen, 100, 200, 400, 200);
            page.Graphics.DrawString("Round", sectionFont, brush, 20, 215);
            bluePen.LineCap = PdfLineCap.Round;
            page.Graphics.DrawLine(bluePen, 100, 220, 400, 220);
            blackPen.DashPattern = null;
            page.Graphics.DrawLine(blackPen, 100, 220, 400, 220);

            page.Graphics.DrawString("Line join styles:", sectionFont, brush, 20, 250);
            page.Graphics.DrawString("Miter", sectionFont, brush, 20, 280);
            PdfPath miterPath = new PdfPath();
            miterPath.StartSubpath(150, 320);
            miterPath.AddLineTo(250, 260);
            miterPath.AddLineTo(350, 320);
            bluePen.LineCap = PdfLineCap.Flat;
            bluePen.LineJoin = PdfLineJoin.Miter;
            page.Graphics.DrawPath(bluePen, miterPath);

            page.Graphics.DrawString("Bevel", sectionFont, brush, 20, 360);
            PdfPath bevelPath = new PdfPath();
            bevelPath.StartSubpath(150, 400);
            bevelPath.AddLineTo(250, 340);
            bevelPath.AddLineTo(350, 400);
            bluePen.LineCap = PdfLineCap.Flat;
            bluePen.LineJoin = PdfLineJoin.Bevel;
            page.Graphics.DrawPath(bluePen, bevelPath);

            page.Graphics.DrawString("Round", sectionFont, brush, 20, 440);
            PdfPath roundPath = new PdfPath();
            roundPath.StartSubpath(150, 480);
            roundPath.AddLineTo(250, 420);
            roundPath.AddLineTo(350, 480);
            bluePen.LineCap = PdfLineCap.Flat;
            bluePen.LineJoin = PdfLineJoin.Round;
            page.Graphics.DrawPath(bluePen, roundPath);

            page.Graphics.DrawString("Random lines clipped to rectangle", sectionFont, brush, 20, 520);
            PdfPath clipPath = new PdfPath();
            clipPath.AddRectangle(20, 550, 570, 230);

            page.Graphics.SaveGraphicsState();
            page.Graphics.SetClip(clipPath);

            PdfRgbColor randomColor = new PdfRgbColor();
            PdfPen randomPen = new PdfPen(randomColor, 1);
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                randomColor.R = (byte)rnd.Next(256);
                randomColor.G = (byte)rnd.Next(256);
                randomColor.B = (byte)rnd.Next(256);

                page.Graphics.DrawLine(randomPen, rnd.NextDouble() * page.Width, 550 + rnd.NextDouble() * 250, rnd.NextDouble() * page.Width, 550 + rnd.NextDouble() * 250);
            }

            page.Graphics.RestoreGraphicsState();

            page.Graphics.DrawPath(blackPen, clipPath);

            page.Graphics.CompressAndClose();
        }

        private static void DrawRectangles(PdfPage page, PdfFont titleFont, PdfFont sectionFont)
        {
            PdfBrush brush = new PdfBrush();
            PdfPen blackPen = new PdfPen(PdfRgbColor.Black, 1);
            PdfPen redPen = new PdfPen(PdfRgbColor.Red, 1);

            PdfRgbColor randomPenColor = new PdfRgbColor();
            PdfPen randomPen = new PdfPen(randomPenColor, 1);
            PdfRgbColor randomBrushColor = new PdfRgbColor();
            PdfBrush randomBrush = new PdfBrush(randomBrushColor);

            page.Graphics.DrawString("Rectangles", titleFont, brush, 20, 50);

            page.Graphics.DrawLine(blackPen, 20, 150, 300, 150);
            page.Graphics.DrawLine(blackPen, 80, 70, 80, 350);
            page.Graphics.DrawRectangle(redPen, 80, 150, 180, 100);

            page.Graphics.DrawLine(blackPen, 320, 150, 600, 150);
            page.Graphics.DrawLine(blackPen, 380, 70, 380, 350);
            page.Graphics.DrawRectangle(redPen, 380, 150, 180, 100, 30);

            page.Graphics.DrawString("Random rectangles clipped to view", sectionFont, brush, 20, 385);
            PdfPath rectPath = new PdfPath();
            rectPath.AddRectangle(20, 400, 570, 300);

            page.Graphics.SaveGraphicsState();
            page.Graphics.SetClip(rectPath);

            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                randomPenColor.R = (byte)rnd.Next(256);
                randomPenColor.G = (byte)rnd.Next(256);
                randomPenColor.B = (byte)rnd.Next(256);

                randomBrushColor.R = (byte)rnd.Next(256);
                randomBrushColor.G = (byte)rnd.Next(256);
                randomBrushColor.B = (byte)rnd.Next(256);

                int mode = rnd.Next(3);
                double left = rnd.NextDouble() * page.Width;
                double top = 380 + rnd.NextDouble() * 350;
                double width = rnd.NextDouble() * page.Width;
                double height = rnd.NextDouble() * 250;
                double orientation = rnd.Next(360);
                switch (mode)
                {
                    case 0:
                        // Stroke rectangle outline
                        page.Graphics.DrawRectangle(randomPen, left, top, width, height, orientation);
                        break;
                    case 1:
                        // Fill rectangle interior
                        page.Graphics.DrawRectangle(randomBrush, left, top, width, height, orientation);
                        break;
                    case 2:
                        // Stroke and fill rectangle
                        page.Graphics.DrawRectangle(randomPen, randomBrush, left, top, width, height, orientation);
                        break;
                }
            }

            page.Graphics.RestoreGraphicsState();

            page.Graphics.DrawPath(blackPen, rectPath);

            page.Graphics.CompressAndClose();
        }

        private static void DrawRoundRectangles(PdfPage page, PdfFont titleFont, PdfFont sectionFont)
        {
            PdfBrush brush = new PdfBrush();
            PdfPen blackPen = new PdfPen(PdfRgbColor.Black, 1);
            PdfPen redPen = new PdfPen(PdfRgbColor.Red, 1);

            PdfRgbColor randomPenColor = new PdfRgbColor();
            PdfPen randomPen = new PdfPen(randomPenColor, 1);
            PdfRgbColor randomBrushColor = new PdfRgbColor();
            PdfBrush randomBrush = new PdfBrush(randomBrushColor);

            page.Graphics.DrawString("Round rectangles", titleFont, brush, 20, 50);

            page.Graphics.DrawLine(blackPen, 20, 150, 300, 150);
            page.Graphics.DrawLine(blackPen, 80, 70, 80, 350);
            page.Graphics.DrawRoundRectangle(redPen, 80, 150, 180, 100, 20, 20);

            page.Graphics.DrawLine(blackPen, 320, 150, 600, 150);
            page.Graphics.DrawLine(blackPen, 380, 70, 380, 350);
            page.Graphics.DrawRoundRectangle(redPen, 380, 150, 180, 100, 20, 20, 30);

            page.Graphics.DrawString("Random round rectangles clipped to view", sectionFont, brush, 20, 385);
            PdfPath roundRectPath = new PdfPath();
            roundRectPath.AddRoundRectangle(20, 400, 570, 300, 20, 20);

            page.Graphics.SaveGraphicsState();
            page.Graphics.SetClip(roundRectPath);

            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                randomPenColor.R = (byte)rnd.Next(256);
                randomPenColor.G = (byte)rnd.Next(256);
                randomPenColor.B = (byte)rnd.Next(256);

                randomBrushColor.R = (byte)rnd.Next(256);
                randomBrushColor.G = (byte)rnd.Next(256);
                randomBrushColor.B = (byte)rnd.Next(256);

                int mode = rnd.Next(3);
                double left = rnd.NextDouble() * page.Width;
                double top = 380 + rnd.NextDouble() * 350;
                double width = rnd.NextDouble() * page.Width;
                double height = rnd.NextDouble() * 250;
                double orientation = rnd.Next(360);
                switch (mode)
                {
                    case 0:
                        // Stroke rectangle outline
                        page.Graphics.DrawRoundRectangle(randomPen, left, top, width, height, width * 0.1, height * 0.1, orientation);
                        break;
                    case 1:
                        // Fill rectangle interior
                        page.Graphics.DrawRoundRectangle(randomBrush, left, top, width, height, width * 0.1, height * 0.1, orientation);
                        break;
                    case 2:
                        // Stroke and fill rectangle
                        page.Graphics.DrawRoundRectangle(randomPen, randomBrush, left, top, width, height, width * 0.1, height * 0.1, orientation);
                        break;
                }
            }

            page.Graphics.RestoreGraphicsState();

            page.Graphics.DrawPath(blackPen, roundRectPath);

            page.Graphics.CompressAndClose();
        }

        private static void DrawEllipses(PdfPage page, PdfFont titleFont, PdfFont sectionFont)
        {
            PdfBrush brush = new PdfBrush();
            PdfPen blackPen = new PdfPen(PdfRgbColor.Black, 1);
            PdfPen redPen = new PdfPen(PdfRgbColor.Red, 1);

            PdfRgbColor randomPenColor = new PdfRgbColor();
            PdfPen randomPen = new PdfPen(randomPenColor, 1);
            PdfRgbColor randomBrushColor = new PdfRgbColor();
            PdfBrush randomBrush = new PdfBrush(randomBrushColor);

            page.Graphics.DrawString("Ellipses", titleFont, brush, 20, 50);

            page.Graphics.DrawLine(blackPen, 20, 150, 300, 150);
            page.Graphics.DrawLine(blackPen, 80, 70, 80, 350);
            page.Graphics.DrawEllipse(redPen, 80, 150, 180, 100);

            page.Graphics.DrawLine(blackPen, 320, 150, 600, 150);
            page.Graphics.DrawLine(blackPen, 380, 70, 380, 350);
            page.Graphics.DrawEllipse(redPen, 380, 150, 180, 100, 30);

            page.Graphics.DrawString("Random ellipses clipped to view", sectionFont, brush, 20, 385);
            PdfPath ellipsePath = new PdfPath();
            ellipsePath.AddEllipse(20, 400, 570, 300);

            page.Graphics.SaveGraphicsState();
            page.Graphics.SetClip(ellipsePath);

            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                randomPenColor.R = (byte)rnd.Next(256);
                randomPenColor.G = (byte)rnd.Next(256);
                randomPenColor.B = (byte)rnd.Next(256);

                randomBrushColor.R = (byte)rnd.Next(256);
                randomBrushColor.G = (byte)rnd.Next(256);
                randomBrushColor.B = (byte)rnd.Next(256);

                int mode = rnd.Next(3);
                double left = rnd.NextDouble() * page.Width;
                double top = 380 + rnd.NextDouble() * 350;
                double width = rnd.NextDouble() * page.Width;
                double height = rnd.NextDouble() * 250;
                double orientation = rnd.Next(360);
                switch (mode)
                {
                    case 0:
                        // Stroke ellipse outline
                        page.Graphics.DrawEllipse(randomPen, left, top, width, height, orientation);
                        break;
                    case 1:
                        // Fill ellipse interior
                        page.Graphics.DrawEllipse(randomBrush, left, top, width, height, orientation);
                        break;
                    case 2:
                        // Stroke and fill ellipse
                        page.Graphics.DrawEllipse(randomPen, randomBrush, left, top, width, height, orientation);
                        break;
                }
            }

            page.Graphics.RestoreGraphicsState();

            page.Graphics.DrawPath(blackPen, ellipsePath);

            page.Graphics.CompressAndClose();
        }

        private static void DrawArcsAndPies(PdfPage page, PdfFont titleFont, PdfFont sectionFont)
        {
            PdfBrush brush = new PdfBrush();
            PdfPen blackPen = new PdfPen(PdfRgbColor.Black, 1);
            PdfPen redPen = new PdfPen(PdfRgbColor.Red, 1);

            PdfRgbColor randomPenColor = new PdfRgbColor();
            PdfPen randomPen = new PdfPen(randomPenColor, 1);
            PdfRgbColor randomBrushColor = new PdfRgbColor();
            PdfBrush randomBrush = new PdfBrush(randomBrushColor);

            page.Graphics.DrawString("Arcs", titleFont, brush, 20, 50);
            page.Graphics.DrawString("Pies", titleFont, brush, 310, 50);

            page.Graphics.DrawLine(blackPen, 20, 210, 300, 210);
            page.Graphics.DrawLine(blackPen, 160, 70, 160, 350);
            page.Graphics.DrawLine(blackPen, 310, 210, 590, 210);
            page.Graphics.DrawLine(blackPen, 450, 70, 450, 350);

            blackPen.DashPattern = new double[] { 2, 2 };
            page.Graphics.DrawLine(blackPen, 20, 70, 300, 350);
            page.Graphics.DrawLine(blackPen, 20, 350, 300, 70);
            page.Graphics.DrawLine(blackPen, 310, 70, 590, 350);
            page.Graphics.DrawLine(blackPen, 310, 350, 590, 70);

            page.Graphics.DrawArc(redPen, 30, 80, 260, 260, 0, 135);
            page.Graphics.DrawPie(redPen, 320, 80, 260, 260, 45, 270);

            page.Graphics.DrawString("Random arcs and pies clipped to view", sectionFont, brush, 20, 385);
            PdfPath rectPath = new PdfPath();
            rectPath.AddRectangle(20, 400, 570, 300);

            page.Graphics.SaveGraphicsState();
            page.Graphics.SetClip(rectPath);

            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                randomPenColor.R = (byte)rnd.Next(256);
                randomPenColor.G = (byte)rnd.Next(256);
                randomPenColor.B = (byte)rnd.Next(256);

                randomBrushColor.R = (byte)rnd.Next(256);
                randomBrushColor.G = (byte)rnd.Next(256);
                randomBrushColor.B = (byte)rnd.Next(256);

                int mode = rnd.Next(4);
                double left = rnd.NextDouble() * page.Width;
                double top = 380 + rnd.NextDouble() * 350;
                double width = rnd.NextDouble() * page.Width;
                double height = rnd.NextDouble() * 250;
                double startAngle = rnd.Next(360);
                double sweepAngle = rnd.Next(360);
                switch (mode)
                {
                    case 0:
                        // Stroke arc outline
                        page.Graphics.DrawArc(randomPen, left, top, width, height, startAngle, sweepAngle);
                        break;
                    case 1:
                        // Stroke pie outline
                        page.Graphics.DrawPie(randomPen, left, top, width, height, startAngle, sweepAngle);
                        break;
                    case 2:
                        // Fill pie interior
                        page.Graphics.DrawPie(randomBrush, left, top, width, height, startAngle, sweepAngle);
                        break;
                    case 3:
                        // Stroke and fill pie
                        page.Graphics.DrawPie(randomPen, randomBrush, left, top, width, height, startAngle, sweepAngle);
                        break;
                }
            }

            page.Graphics.RestoreGraphicsState();

            blackPen.DashPattern = null;
            page.Graphics.DrawPath(blackPen, rectPath);

            page.Graphics.CompressAndClose();
        }

        private static void DrawBezierCurves(PdfPage page, PdfFont titleFont, PdfFont sectionFont)
        {
            PdfBrush brush = new PdfBrush();
            PdfPen blackPen = new PdfPen(PdfRgbColor.Black, 1);
            PdfPen redPen = new PdfPen(PdfRgbColor.Red, 1);
            PdfBrush blueBrush = new PdfBrush(PdfRgbColor.DarkBlue);

            PdfRgbColor randomPenColor = new PdfRgbColor();
            PdfPen randomPen = new PdfPen(randomPenColor, 1);

            page.Graphics.DrawString("Bezier curves", titleFont, brush, 20, 50);

            page.Graphics.DrawLine(blackPen, 20, 210, 600, 210);
            page.Graphics.DrawLine(blackPen, 306, 70, 306, 350);
            page.Graphics.DrawRectangle(blueBrush, 39, 339, 2, 2);
            page.Graphics.DrawRectangle(blueBrush, 279, 79, 2, 2);
            page.Graphics.DrawRectangle(blueBrush, 499, 299, 2, 2);
            page.Graphics.DrawRectangle(blueBrush, 589, 69, 2, 2);
            page.Graphics.DrawBezier(redPen, 40, 340, 280, 80, 500, 300, 590, 70);

            page.Graphics.DrawString("Random bezier curves clipped to view", sectionFont, brush, 20, 385);
            PdfPath rectPath = new PdfPath();
            rectPath.AddRectangle(20, 400, 570, 300);

            page.Graphics.SaveGraphicsState();
            page.Graphics.SetClip(rectPath);

            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                randomPenColor.R = (byte)rnd.Next(256);
                randomPenColor.G = (byte)rnd.Next(256);
                randomPenColor.B = (byte)rnd.Next(256);

                double x1 = rnd.NextDouble() * page.Width;
                double y1 = 380 + rnd.NextDouble() * 350;
                double x2 = rnd.NextDouble() * page.Width;
                double y2 = 380 + rnd.NextDouble() * 350;
                double x3 = rnd.NextDouble() * page.Width;
                double y3 = 380 + rnd.NextDouble() * 350;
                double x4 = rnd.NextDouble() * page.Width;
                double y4 = 380 + rnd.NextDouble() * 350;

                page.Graphics.DrawBezier(randomPen, x1, y1, x2, y2, x3, y3, x4, y4);
            }

            page.Graphics.RestoreGraphicsState();

            blackPen.DashPattern = null;
            page.Graphics.DrawPath(blackPen, rectPath);

            page.Graphics.CompressAndClose();
        }

        private static void DrawAffineTransformations(PdfPage page, PdfFont titleFont, PdfFont sectionFont)
        {
            PdfBrush brush = new PdfBrush();
            PdfPen blackPen = new PdfPen(PdfRgbColor.Black, 1);
            PdfPen redPen = new PdfPen(PdfRgbColor.Red, 1);
            PdfPen bluePen = new PdfPen(PdfRgbColor.Blue, 1);
            PdfPen greenPen = new PdfPen(PdfRgbColor.Green, 1);

            page.Graphics.DrawString("Affine transformations", titleFont, brush, 20, 50);

            page.Graphics.DrawLine(blackPen, 0, page.Height / 2, page.Width, page.Height / 2);
            page.Graphics.DrawLine(blackPen, page.Width / 2, 0, page.Width / 2, page.Height);

            page.Graphics.SaveGraphicsState();

            // Move the coordinate system in the center of the page.
            page.Graphics.TranslateTransform(page.Width / 2, page.Height / 2);

            // Draw a rectangle with the center at (0, 0)
            page.Graphics.DrawRectangle(redPen, -page.Width / 4, -page.Height / 8, page.Width / 2, page.Height / 4);

            // Rotate the coordinate system with 30 degrees.
            page.Graphics.RotateTransform(30);

            // Draw the same rectangle with the center at (0, 0)
            page.Graphics.DrawRectangle(greenPen, -page.Width / 4, -page.Height / 8, page.Width / 2, page.Height / 4);

            // Scale the coordinate system with 1.5
            page.Graphics.ScaleTransform(1.5, 1.5);

            // Draw the same rectangle with the center at (0, 0)
            page.Graphics.DrawRectangle(bluePen, -page.Width / 4, -page.Height / 8, page.Width / 2, page.Height / 4);

            page.Graphics.RestoreGraphicsState();

            page.Graphics.CompressAndClose();
        }

        private static void DrawColorsAndColorSpaces(PdfPage page, PdfFont titleFont, PdfFont sectionFont, Stream iccStream)
        {
            PdfBrush brush = new PdfBrush();

            page.Graphics.DrawString("Colors and colorspaces", titleFont, brush, 20, 50);

            page.Graphics.DrawString("DeviceRGB", sectionFont, brush, 20, 70);
            PdfPen rgbPen = new PdfPen(PdfRgbColor.DarkRed, 4);
            PdfBrush rgbBrush = new PdfBrush(PdfRgbColor.LightGoldenrodYellow);
            page.Graphics.DrawRectangle(rgbPen, rgbBrush, 20, 85, 250, 100);

            page.Graphics.DrawString("DeviceCMYK", sectionFont, brush, 340, 70);
            PdfPen cmykPen = new PdfPen(new PdfCmykColor(1, 0.5, 0, 0.1), 4);
            PdfBrush cmykBrush = new PdfBrush(new PdfCmykColor(0, 0.5, 0.43, 0));
            page.Graphics.DrawRectangle(cmykPen, cmykBrush, 340, 85, 250, 100);

            page.Graphics.DrawString("DeviceGray", sectionFont, brush, 20, 200);
            PdfPen grayPen = new PdfPen(new PdfGrayColor(0.1), 4);
            PdfBrush grayBrush = new PdfBrush(new PdfGrayColor(0.75));
            page.Graphics.DrawRectangle(grayPen, grayBrush, 20, 215, 250, 100);

            page.Graphics.DrawString("Indexed", sectionFont, brush, 340, 200);
            PdfIndexedColorSpace indexedColorSpace = new PdfIndexedColorSpace();
            indexedColorSpace.ColorCount = 2;
            indexedColorSpace.BaseColorSpace = new PdfRgbColorSpace();
            indexedColorSpace.ColorTable = new byte[] { 192, 0, 0, 0, 0, 128 };
            PdfIndexedColor indexedColor0 = new PdfIndexedColor(indexedColorSpace);
            indexedColor0.ColorIndex = 0;
            PdfIndexedColor indexedColor1 = new PdfIndexedColor(indexedColorSpace);
            indexedColor1.ColorIndex = 1;
            PdfPen indexedPen = new PdfPen(indexedColor0, 4);
            PdfBrush indexedBrush = new PdfBrush(indexedColor1);
            page.Graphics.DrawRectangle(indexedPen, indexedBrush, 340, 215, 250, 100);

            page.Graphics.DrawString("CalGray", sectionFont, brush, 20, 330);
            PdfCalGrayColorSpace calGrayColorSpace = new PdfCalGrayColorSpace();
            PdfCalGrayColor calGrayColor1 = new PdfCalGrayColor(calGrayColorSpace);
            calGrayColor1.Gray = 0.6;
            PdfCalGrayColor calGrayColor2 = new PdfCalGrayColor(calGrayColorSpace);
            calGrayColor2.Gray = 0.2;
            PdfPen calGrayPen = new PdfPen(calGrayColor1, 4);
            PdfBrush calGrayBrush = new PdfBrush(calGrayColor2);
            page.Graphics.DrawRectangle(calGrayPen, calGrayBrush, 20, 345, 250, 100);

            page.Graphics.DrawString("CalRGB", sectionFont, brush, 340, 330);
            PdfCalRgbColorSpace calRgbColorSpace = new PdfCalRgbColorSpace();
            PdfCalRgbColor calRgbColor1 = new PdfCalRgbColor(calRgbColorSpace);
            calRgbColor1.Red = 0.1;
            calRgbColor1.Green = 0.5;
            calRgbColor1.Blue = 0.25;
            PdfCalRgbColor calRgbColor2 = new PdfCalRgbColor(calRgbColorSpace);
            calRgbColor2.Red = 0.6;
            calRgbColor2.Green = 0.1;
            calRgbColor2.Blue = 0.9;
            PdfPen calRgbPen = new PdfPen(calRgbColor1, 4);
            PdfBrush calRgbBrush = new PdfBrush(calRgbColor2);
            page.Graphics.DrawRectangle(calRgbPen, calRgbBrush, 340, 345, 250, 100);

            page.Graphics.DrawString("L*a*b", sectionFont, brush, 20, 460);
            PdfLabColorSpace labColorSpace = new PdfLabColorSpace();
            PdfLabColor labColor1 = new PdfLabColor(labColorSpace);
            labColor1.L = 90;
            labColor1.A = -40;
            labColor1.B = 120;
            PdfLabColor labColor2 = new PdfLabColor(labColorSpace);
            labColor2.L = 45;
            labColor2.A = 90;
            labColor2.B = -34;
            PdfPen labPen = new PdfPen(labColor1, 4);
            PdfBrush labBrush = new PdfBrush(labColor2);
            page.Graphics.DrawRectangle(labPen, labBrush, 20, 475, 250, 100);

            page.Graphics.DrawString("Icc", sectionFont, brush, 340, 460);
            PdfIccColorSpace iccColorSpace = new PdfIccColorSpace();
            byte[] iccData = new byte[iccStream.Length];
            iccStream.Read(iccData, 0, iccData.Length);
            iccColorSpace.IccProfile = iccData;
            iccColorSpace.AlternateColorSpace = new PdfRgbColorSpace();
            iccColorSpace.ColorComponents = 3;
            PdfIccColor iccColor1 = new PdfIccColor(iccColorSpace);
            iccColor1.ColorComponents = new double[] { 0.45, 0.1, 0.22 };
            PdfIccColor iccColor2 = new PdfIccColor(iccColorSpace);
            iccColor2.ColorComponents = new double[] { 0.21, 0.76, 0.31 };
            PdfPen iccPen = new PdfPen(iccColor1, 4);
            PdfBrush iccBrush = new PdfBrush(iccColor2);
            page.Graphics.DrawRectangle(iccPen, iccBrush, 340, 475, 250, 100);

            page.Graphics.DrawString("Separation", sectionFont, brush, 20, 590);
            PdfExponentialFunction tintTransform = new PdfExponentialFunction();
            tintTransform.Domain = new double[] { 0, 1 };
            tintTransform.Range = new double[] { 0, 1, 0, 1, 0, 1, 0, 1 };
            tintTransform.Exponent = 1;
            tintTransform.C0 = new double[] { 0, 0, 0, 0 };
            tintTransform.C1 = new double[] { 1, 0.5, 0, 0.1 };

            PdfSeparationColorSpace separationColorSpace = new PdfSeparationColorSpace();
            separationColorSpace.AlternateColorSpace = new PdfCmykColorSpace();
            separationColorSpace.Colorant = "Custom Blue";
            separationColorSpace.TintTransform = tintTransform;

            PdfSeparationColor separationColor1 = new PdfSeparationColor(separationColorSpace);
            separationColor1.Tint = 0.23;
            PdfSeparationColor separationColor2 = new PdfSeparationColor(separationColorSpace);
            separationColor2.Tint = 0.74;

            PdfPen separationPen = new PdfPen(separationColor1, 4);
            PdfBrush separationBrush = new PdfBrush(separationColor2);
            page.Graphics.DrawRectangle(separationPen, separationBrush, 20, 605, 250, 100);

            page.Graphics.DrawString("Pantone", sectionFont, brush, 340, 590);
            PdfPen pantonePen = new PdfPen(PdfPantoneColor.ReflexBlue, 4);
            PdfBrush pantoneBrush = new PdfBrush(PdfPantoneColor.RhodamineRed);
            page.Graphics.DrawRectangle(pantonePen, pantoneBrush, 340, 605, 250, 100);

            page.Graphics.CompressAndClose();
        }

        private static void DrawShadings(PdfPage page, PdfFont titleFont, PdfFont sectionFont)
        {
            PdfBrush brush = new PdfBrush();
            PdfPen blackPen = new PdfPen(PdfRgbColor.Black, 1);

            PdfRgbColor randomPenColor = new PdfRgbColor();
            PdfPen randomPen = new PdfPen(randomPenColor, 1);
            PdfRgbColor randomBrushColor = new PdfRgbColor();
            PdfBrush randomBrush = new PdfBrush(randomBrushColor);

            page.Graphics.DrawString("Shadings", titleFont, brush, 20, 50);

            page.Graphics.DrawString("Horizontal", sectionFont, brush, 25, 70);

            PdfAxialShading horizontalShading = new PdfAxialShading();
            horizontalShading.StartColor = new PdfRgbColor(255, 0, 0);
            horizontalShading.EndColor = new PdfRgbColor(0, 0, 255);
            horizontalShading.StartPoint = new PdfPoint(25, 90);
            horizontalShading.EndPoint = new PdfPoint(175, 90);

            // Clip the shading to desired area.
            PdfPath hsArea = new PdfPath();
            hsArea.AddRectangle(25, 90, 150, 150);
            page.Graphics.SaveGraphicsState();
            page.Graphics.SetClip(hsArea);
            page.Graphics.DrawShading(horizontalShading);
            page.Graphics.RestoreGraphicsState();

            page.Graphics.DrawString("Vertical", sectionFont, brush, 225, 70);

            PdfAxialShading verticalShading = new PdfAxialShading();
            verticalShading.StartColor = new PdfRgbColor(255, 0, 0);
            verticalShading.EndColor = new PdfRgbColor(0, 0, 255);
            verticalShading.StartPoint = new PdfPoint(225, 90);
            verticalShading.EndPoint = new PdfPoint(225, 240);

            // Clip the shading to desired area.
            PdfPath vsArea = new PdfPath();
            vsArea.AddRectangle(225, 90, 150, 150);
            page.Graphics.SaveGraphicsState();
            page.Graphics.SetClip(vsArea);
            page.Graphics.DrawShading(verticalShading);
            page.Graphics.RestoreGraphicsState();

            page.Graphics.DrawString("Diagonal", sectionFont, brush, 425, 70);

            PdfAxialShading diagonalShading = new PdfAxialShading();
            diagonalShading.StartColor = new PdfRgbColor(255, 0, 0);
            diagonalShading.EndColor = new PdfRgbColor(0, 0, 255);
            diagonalShading.StartPoint = new PdfPoint(425, 90);
            diagonalShading.EndPoint = new PdfPoint(575, 240);

            // Clip the shading to desired area.
            PdfPath dsArea = new PdfPath();
            dsArea.AddRectangle(425, 90, 150, 150);
            page.Graphics.SaveGraphicsState();
            page.Graphics.SetClip(dsArea);
            page.Graphics.DrawShading(diagonalShading);
            page.Graphics.RestoreGraphicsState();

            page.Graphics.DrawString("Extended shading", sectionFont, brush, 25, 260);

            PdfAxialShading extendedShading = new PdfAxialShading();
            extendedShading.StartColor = new PdfRgbColor(255, 0, 0);
            extendedShading.EndColor = new PdfRgbColor(0, 0, 255);
            extendedShading.StartPoint = new PdfPoint(225, 280);
            extendedShading.EndPoint = new PdfPoint(375, 280);
            extendedShading.ExtendStart = true;
            extendedShading.ExtendEnd = true;

            // Clip the shading to desired area.
            PdfPath esArea = new PdfPath();
            esArea.AddRectangle(25, 280, 550, 30);
            page.Graphics.SaveGraphicsState();
            page.Graphics.SetClip(esArea);
            page.Graphics.DrawShading(extendedShading);
            page.Graphics.RestoreGraphicsState();
            page.Graphics.DrawPath(blackPen, esArea);

            page.Graphics.DrawString("Limited shading", sectionFont, brush, 25, 330);

            PdfAxialShading limitedShading = new PdfAxialShading();
            limitedShading.StartColor = new PdfRgbColor(255, 0, 0);
            limitedShading.EndColor = new PdfRgbColor(0, 0, 255);
            limitedShading.StartPoint = new PdfPoint(225, 350);
            limitedShading.EndPoint = new PdfPoint(375, 350);
            limitedShading.ExtendStart = false;
            limitedShading.ExtendEnd = false;

            // Clip the shading to desired area.
            PdfPath lsArea = new PdfPath();
            lsArea.AddRectangle(25, 350, 550, 30);
            page.Graphics.SaveGraphicsState();
            page.Graphics.SetClip(lsArea);
            page.Graphics.DrawShading(limitedShading);
            page.Graphics.RestoreGraphicsState();
            page.Graphics.DrawPath(blackPen, lsArea);

            page.Graphics.DrawString("Multi-stop shading", sectionFont, brush, 25, 400);
            // Multi-stop shadings use a stitching function to combine the functions that define each gradient part.
            // Function for red to blue shading.
            PdfExponentialFunction redToBlueFunc = new PdfExponentialFunction();
            // Linear function
            redToBlueFunc.Exponent = 1;
            redToBlueFunc.Domain = new double[] { 0, 1 };
            // Red color for start
            redToBlueFunc.C0 = new double[] { 1, 0, 0 };
            // Blue color for start
            redToBlueFunc.C1 = new double[] { 0, 0, 1 };
            // Function for blue to green shading.
            PdfExponentialFunction blueToGreenFunc = new PdfExponentialFunction();
            // Linear function
            blueToGreenFunc.Exponent = 1;
            blueToGreenFunc.Domain = new double[] { 0, 1 };
            // Blue color for start
            blueToGreenFunc.C0 = new double[] { 0, 0, 1 };
            // Green color for start
            blueToGreenFunc.C1 = new double[] { 0, 1, 0 };

            //Stitching function for the shading.
            PdfStitchingFunction shadingFunction = new PdfStitchingFunction();
            shadingFunction.Functions.Add(redToBlueFunc);
            shadingFunction.Functions.Add(blueToGreenFunc);
            shadingFunction.Domain = new double[] { 0, 1 };
            shadingFunction.Encode = new double[] { 0, 1, 0, 1 };

            // Entire shading goes from 0 to 1 (100%).
            // We set the first shading (red->blue) to cover 30% (0 - 0.3) and
            // the second shading to cover 70% (0.3 - 1).
            shadingFunction.Bounds = new double[] { 0.3 };
            // The multistop shading
            PdfAxialShading multiStopShading = new PdfAxialShading();
            multiStopShading.StartPoint = new PdfPoint(25, 420);
            multiStopShading.EndPoint = new PdfPoint(575, 420);
            // The colorspace must match the colors specified in C0 & C1
            multiStopShading.ColorSpace = new PdfRgbColorSpace();
            multiStopShading.Function = shadingFunction;

            // Clip the shading to desired area.
            PdfPath mssArea = new PdfPath();
            mssArea.AddRectangle(25, 420, 550, 30);
            page.Graphics.SaveGraphicsState();
            page.Graphics.SetClip(mssArea);
            page.Graphics.DrawShading(multiStopShading);
            page.Graphics.RestoreGraphicsState();
            page.Graphics.DrawPath(blackPen, lsArea);

            page.Graphics.DrawString("Radial shading", sectionFont, brush, 25, 470);

            PdfRadialShading rs1 = new PdfRadialShading();
            rs1.StartColor = new PdfRgbColor(0, 255, 0);
            rs1.EndColor = new PdfRgbColor(255, 0, 255);
            rs1.StartCircleCenter = new PdfPoint(50, 500);
            rs1.StartCircleRadius = 10;
            rs1.EndCircleCenter = new PdfPoint(500, 570);
            rs1.EndCircleRadius = 100;

            page.Graphics.DrawShading(rs1);

            PdfRadialShading rs2 = new PdfRadialShading();
            rs2.StartColor = new PdfRgbColor(0, 255, 0);
            rs2.EndColor = new PdfRgbColor(255, 0, 255);
            rs2.StartCircleCenter = new PdfPoint(80, 600);
            rs2.StartCircleRadius = 10;
            rs2.EndCircleCenter = new PdfPoint(110, 690);
            rs2.EndCircleRadius = 100;

            page.Graphics.DrawShading(rs2);

            page.Graphics.CompressAndClose();
        }

        private static void DrawPatterns(PdfPage page, PdfFont titleFont, PdfFont sectionFont)
        {
            PdfBrush brush = new PdfBrush();
            PdfPen blackPen = new PdfPen(PdfRgbColor.Black, 1);

            PdfPen darkRedPen = new PdfPen(new PdfRgbColor(0xFF, 0x40, 0x40), 0.8);
            PdfPen darkOrangePen = new PdfPen(new PdfRgbColor(0xA6, 0x4B, 0x00), 0.8);
            PdfPen darkCyanPen = new PdfPen(new PdfRgbColor(0x00, 0x63, 0x63), 0.8);
            PdfPen darkGreenPen = new PdfPen(new PdfRgbColor(0x00, 0x85, 0x00), 0.8);
            PdfBrush lightRedBrush = new PdfBrush(new PdfRgbColor(0xFF, 0x73, 0x73));
            PdfBrush lightOrangeBrush = new PdfBrush(new PdfRgbColor(0xFF, 0x96, 0x40));
            PdfBrush lightCyanBrush = new PdfBrush(new PdfRgbColor(0x33, 0xCC, 0xCC));
            PdfBrush lightGreenBrush = new PdfBrush(new PdfRgbColor(0x67, 0xE6, 0x67));

            page.Graphics.DrawString("Patterns", titleFont, brush, 20, 50);

            page.Graphics.DrawString("Colored patterns", sectionFont, brush, 25, 70);

            // Create the pattern visual appearance.
            PdfColoredTilingPattern ctp = new PdfColoredTilingPattern(20, 20);
            // Red circle
            ctp.Graphics.DrawEllipse(darkRedPen, lightRedBrush, 1, 1, 8, 8);
            // Cyan square
            ctp.Graphics.DrawRectangle(darkCyanPen, lightCyanBrush, 11, 1, 8, 8);
            // Green diamond
            PdfPath diamondPath = new PdfPath();
            diamondPath.StartSubpath(1, 15);
            diamondPath.AddPolyLineTo(new PdfPoint[] { new PdfPoint(5, 11), new PdfPoint(9, 15), new PdfPoint(5, 19) });
            diamondPath.CloseSubpath();
            ctp.Graphics.DrawPath(darkGreenPen, lightGreenBrush, diamondPath);
            // Orange triangle
            PdfPath trianglePath = new PdfPath();
            trianglePath.StartSubpath(11, 19);
            trianglePath.AddPolyLineTo(new PdfPoint[] { new PdfPoint(15, 11), new PdfPoint(19, 19) });
            trianglePath.CloseSubpath();
            ctp.Graphics.DrawPath(darkOrangePen, lightOrangeBrush, trianglePath);

            // Create a pattern colorspace from the pattern object.
            PdfPatternColorSpace coloredPatternColorSpace = new PdfPatternColorSpace(ctp);
            // Create a color based on the pattern colorspace.
            PdfPatternColor coloredPatternColor = new PdfPatternColor(coloredPatternColorSpace);
            // The pen and brush use the pattern color like any other color.
            PdfPatternBrush patternBrush = new PdfPatternBrush(coloredPatternColor);
            PdfPatternPen patternPen = new PdfPatternPen(coloredPatternColor, 40);

            page.Graphics.DrawEllipse(patternBrush, 25, 90, 250, 200);
            page.Graphics.DrawRoundRectangle(patternPen, 310, 110, 250, 160, 100, 100);

            page.Graphics.DrawString("Uncolored patterns", sectionFont, brush, 25, 300);

            // Create the pattern visual appearance.
            PdfUncoloredTilingPattern uctp = new PdfUncoloredTilingPattern(20, 20);
            // A pen without color is used to create the pattern content.
            PdfPen noColorPen = new PdfPen(null, 0.8);
            // Circle
            uctp.Graphics.DrawEllipse(noColorPen, 1, 1, 8, 8);
            // Square
            uctp.Graphics.DrawRectangle(noColorPen, 11, 1, 8, 8);
            // Diamond
            diamondPath = new PdfPath();
            diamondPath.StartSubpath(1, 15);
            diamondPath.AddPolyLineTo(new PdfPoint[] { new PdfPoint(5, 11), new PdfPoint(9, 15), new PdfPoint(5, 19) });
            diamondPath.CloseSubpath();
            uctp.Graphics.DrawPath(noColorPen, diamondPath);
            // Triangle
            trianglePath = new PdfPath();
            trianglePath.StartSubpath(11, 19);
            trianglePath.AddPolyLineTo(new PdfPoint[] { new PdfPoint(15, 11), new PdfPoint(19, 19) });
            trianglePath.CloseSubpath();
            uctp.Graphics.DrawPath(noColorPen, trianglePath);

            // Create a pattern colorspace from the pattern object.
            PdfPatternColorSpace uncoloredPatternColorSpace = new PdfPatternColorSpace(uctp);
            // Create a color based on the pattern colorspace.
            PdfPatternColor uncoloredPatternColor = new PdfPatternColor(uncoloredPatternColorSpace);
            // The pen and brush use the pattern color like any other color.
            patternBrush = new PdfPatternBrush(uncoloredPatternColor);

            // Before using the uncolored pattern set the color that will be used to paint the pattern.
            patternBrush.UncoloredPatternPaintColor = new PdfRgbColor(0xFF, 0x40, 0x40);
            page.Graphics.DrawEllipse(patternBrush, 25, 320, 125, 200);
            patternBrush.UncoloredPatternPaintColor = new PdfRgbColor(0xA6, 0x4B, 0x00);
            page.Graphics.DrawEllipse(patternBrush, 175, 320, 125, 200);
            patternBrush.UncoloredPatternPaintColor = new PdfRgbColor(0x00, 0x63, 0x63);
            page.Graphics.DrawEllipse(patternBrush, 325, 320, 125, 200);
            patternBrush.UncoloredPatternPaintColor = new PdfRgbColor(0x00, 0x85, 0x00);
            page.Graphics.DrawEllipse(patternBrush, 475, 320, 125, 200);

            page.Graphics.DrawString("Shading patterns", sectionFont, brush, 25, 550);

            // Create the pattern visual appearance.
            PdfAxialShading horizontalShading = new PdfAxialShading();
            horizontalShading.StartColor = new PdfRgbColor(255, 0, 0);
            horizontalShading.EndColor = new PdfRgbColor(0, 0, 255);
            horizontalShading.StartPoint = new PdfPoint(25, 600);
            horizontalShading.EndPoint = new PdfPoint(575, 600);
            PdfShadingPattern sp = new PdfShadingPattern(horizontalShading);

            // Create a pattern colorspace from the pattern object.
            PdfPatternColorSpace shadingPatternColorSpace = new PdfPatternColorSpace(sp);
            // Create a color based on the pattern colorspace.
            PdfPatternColor shadingPatternColor = new PdfPatternColor(shadingPatternColorSpace);
            // The pen and brush use the pattern color like any other color.
            patternPen = new PdfPatternPen(shadingPatternColor, 40);

            page.Graphics.DrawEllipse(patternPen, 50, 600, 500, 150);

            page.Graphics.CompressAndClose();
        }

        private static void DrawFormXObjects(PdfPage page, PdfFont titleFont, PdfFont sectionFont)
        {
            PdfBrush brush = new PdfBrush();
            PdfPen blackPen = new PdfPen(PdfRgbColor.Black, 1);

            PdfRgbColor randomPenColor = new PdfRgbColor();
            PdfPen randomPen = new PdfPen(randomPenColor, 1);
            PdfRgbColor randomBrushColor = new PdfRgbColor();
            PdfBrush randomBrush = new PdfBrush(randomBrushColor);

            page.Graphics.DrawString("Form XObjects", titleFont, brush, 20, 50);
            page.Graphics.DrawString("Scaling", sectionFont, brush, 20, 70);

            // Create the XObject content - random rectangles
            PdfFormXObject xobject = new PdfFormXObject(300, 300);
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                randomPenColor.R = (byte)rnd.Next(256);
                randomPenColor.G = (byte)rnd.Next(256);
                randomPenColor.B = (byte)rnd.Next(256);

                randomBrushColor.R = (byte)rnd.Next(256);
                randomBrushColor.G = (byte)rnd.Next(256);
                randomBrushColor.B = (byte)rnd.Next(256);

                double left = rnd.NextDouble() * xobject.Width;
                double top = rnd.NextDouble() * xobject.Height;
                double width = rnd.NextDouble() * xobject.Width;
                double height = rnd.NextDouble() * xobject.Height;
                double orientation = rnd.Next(360);
                xobject.Graphics.DrawRectangle(randomPen, randomBrush, left, top, width, height, orientation);
            }

            xobject.Graphics.DrawRectangle(blackPen, 0, 0, xobject.Width, xobject.Height);
            xobject.Graphics.CompressAndClose();

            // Draw the form XObject 3 times on the page at different sizes.
            page.Graphics.DrawFormXObject(xobject, 3, 90, 100, 100);
            page.Graphics.DrawFormXObject(xobject, 106, 90, 200, 200);
            page.Graphics.DrawFormXObject(xobject, 309, 90, 300, 300);

            page.Graphics.DrawString("Flipping", sectionFont, brush, 20, 420);
            page.Graphics.DrawFormXObject(xobject, 20, 440, 150, 150);
            page.Graphics.DrawFormXObject(xobject, 200, 440, 150, 150, 0, PdfFlipDirection.VerticalFlip);
            page.Graphics.DrawFormXObject(xobject, 20, 620, 150, 150, 0, PdfFlipDirection.HorizontalFlip);
            page.Graphics.DrawFormXObject(xobject, 200, 620, 150, 150, 0, PdfFlipDirection.VerticalFlip | PdfFlipDirection.HorizontalFlip);

            page.Graphics.CompressAndClose();
        }
    }
}