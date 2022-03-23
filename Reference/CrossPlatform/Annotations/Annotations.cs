using System;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Annotations;
using Xfinium.Pdf.Actions;
using Xfinium.Pdf.Destinations;
using System.IO;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// Annotations sample.
    /// </summary>
    public class Annotations
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream flashStream, Stream u3dStream)
        {
            // Create a PDF document with 10 pages.
            PdfFixedDocument document = new PdfFixedDocument();
            PdfStandardFont helvetica = new PdfStandardFont(PdfStandardFontFace.Helvetica, 12);

            CreateTextAnnotations(document, helvetica);

            CreateSquareCircleAnnotations(document, helvetica);

            CreateFileAttachmentAnnotations(document, helvetica);

            CreateInkAnnotations(document, helvetica);

            CreateLineAnnotations(document, helvetica);

            CreatePolygonAnnotations(document, helvetica);

            CreatePolylineAnnotations(document, helvetica);

            CreateRubberStampAnnotations(document, helvetica);

            CreateTextMarkupAnnotations(document, helvetica);

            CreateRichMediaAnnotations(document, helvetica, flashStream);

            Create3DAnnotations(document, helvetica, u3dStream);

            CreateRedactionAnnotations(document, helvetica, u3dStream);

            // Compress the page graphic content.
            for (int i = 0; i < document.Pages.Count; i++)
            {
                document.Pages[i].Graphics.CompressAndClose();
            }

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.annotations.pdf") };
            return output;
        }

        private static void CreateTextAnnotations(PdfFixedDocument document, PdfFont font)
        {
            PdfBrush blackBrush = new PdfBrush();

            PdfPage page = document.Pages.Add();

            string[] textAnnotationNames = new string[] 
                { 
                    "Comment", "Check", "Circle", "Cross", "Help", "Insert", "Key", "NewParagraph", 
                    "Note", "Paragraph", "RightArrow", "RightPointer", "Star", "UpArrow", "UpLeftArrow" 
                };

            page.Graphics.DrawString("B/W text annotations", font, blackBrush, 50, 50);
            for (int i = 0; i < textAnnotationNames.Length; i++)
            {
                PdfTextAnnotation ta = new PdfTextAnnotation();
                ta.Author = "Xfinium.Pdf";
                ta.Contents = "I am a " + textAnnotationNames[i] + " annotation.";
                ta.IconName = textAnnotationNames[i];
                page.Annotations.Add(ta);
                ta.Location = new PdfPoint(50, 100 + 40 * i);
                page.Graphics.DrawString(textAnnotationNames[i], font, blackBrush, 90, 100 + 40 * i);
            }

            Random rnd = new Random();
            byte[] rgb = new byte[3];
            page.Graphics.DrawString("Color text annotations", font, blackBrush, 300, 50);
            for (int i = 0; i < textAnnotationNames.Length; i++)
            {
                PdfTextAnnotation ta = new PdfTextAnnotation();
                ta.Author = "Xfinium.Pdf";
                ta.Contents = "I am a " + textAnnotationNames[i] + " annotation.";
                ta.IconName = textAnnotationNames[i];
                rnd.NextBytes(rgb);
                ta.OutlineColor = new PdfRgbColor(rgb[0], rgb[1], rgb[2]);
                rnd.NextBytes(rgb);
                ta.InteriorColor = new PdfRgbColor(rgb[0], rgb[1], rgb[2]);
                page.Annotations.Add(ta);
                ta.Location = new PdfPoint(300, 100 + 40 * i);
                page.Graphics.DrawString(textAnnotationNames[i], font, blackBrush, 340, 100 + 40 * i);
            }

            page.Graphics.DrawString("Text annotations with custom icons", font, blackBrush, 50, 700);
            PdfTextAnnotation customTextAnnotation = new PdfTextAnnotation();
            customTextAnnotation.Author = "Xfinium.Pdf";
            customTextAnnotation.Contents = "Text annotation with custom icon.";
            page.Annotations.Add(customTextAnnotation);
            customTextAnnotation.IconName = "Custom icon appearance";
            customTextAnnotation.Location = new PdfPoint(50, 720);

            PdfAnnotationAppearance customAppearance = new PdfAnnotationAppearance(50, 50);
            PdfPen redPen = new PdfPen(new PdfRgbColor(255, 0, 0), 10);
            PdfPen bluePen = new PdfPen(new PdfRgbColor(0, 0, 192), 10);
            customAppearance.Graphics.DrawRectangle(redPen, 5, 5, 40, 40);
            customAppearance.Graphics.DrawLine(bluePen, 0, 0, customAppearance.Width, customAppearance.Height);
            customAppearance.Graphics.DrawLine(bluePen, 0, customAppearance.Height, customAppearance.Width, 0);
            customAppearance.Graphics.CompressAndClose();
            customTextAnnotation.NormalAppearance = customAppearance;
        }

        private static void CreateSquareCircleAnnotations(PdfFixedDocument document, PdfFont font)
        {
            PdfBrush blackBrush = new PdfBrush();

            PdfPage page = document.Pages.Add();

            page.Graphics.DrawString("Square annotations", font, blackBrush, 50, 50);

            PdfSquareAnnotation square1 = new PdfSquareAnnotation();
            page.Annotations.Add(square1);
            square1.Author = "Xfinium.pdf";
            square1.Contents = "Square annotation with red border";
            square1.BorderColor = new PdfRgbColor(255, 0, 0);
            square1.BorderWidth = 3;
            square1.VisualRectangle = new PdfVisualRectangle(50, 70, 250, 150);

            PdfSquareAnnotation square2 = new PdfSquareAnnotation();
            page.Annotations.Add(square2);
            square2.Author = "Xfinium.pdf";
            square2.Contents = "Square annotation with blue interior";
            square2.BorderColor = null;
            square2.BorderWidth = 0;
            square2.InteriorColor = new PdfRgbColor(0, 0, 192);
            square2.VisualRectangle = new PdfVisualRectangle(50, 270, 250, 150);

            PdfSquareAnnotation square3 = new PdfSquareAnnotation();
            page.Annotations.Add(square3);
            square3.Author = "Xfinium.pdf";
            square3.Contents = "Square annotation with yellow border and green interior";
            square3.BorderColor = new PdfRgbColor(255, 255, 0);
            square3.BorderWidth = 3;
            square3.InteriorColor = new PdfRgbColor(0, 192, 0);
            square3.VisualRectangle = new PdfVisualRectangle(50, 470, 250, 150);

            page.Graphics.DrawString("Circle annotations", font, blackBrush, 50, 350);

            PdfCircleAnnotation circle1 = new PdfCircleAnnotation();
            page.Annotations.Add(circle1);
            circle1.Author = "Xfinium.pdf";
            circle1.Contents = "Circle annotation with red border";
            circle1.BorderColor = new PdfRgbColor(255, 0, 0);
            circle1.BorderWidth = 3;
            circle1.VisualRectangle = new PdfVisualRectangle(350, 70, 250, 150);

            PdfCircleAnnotation circle2 = new PdfCircleAnnotation();
            page.Annotations.Add(circle2);
            circle2.Author = "Xfinium.pdf";
            circle2.Contents = "Circle annotation with blue interior";
            circle2.BorderColor = null;
            circle2.BorderWidth = 0;
            circle2.InteriorColor = new PdfRgbColor(0, 0, 192);
            circle2.VisualRectangle = new PdfVisualRectangle(350, 270, 250, 150);

            PdfCircleAnnotation circle3 = new PdfCircleAnnotation();
            page.Annotations.Add(circle3);
            circle3.Author = "Xfinium.pdf";
            circle3.Contents = "Circle annotation with yellow border and green interior";
            circle3.BorderColor = new PdfRgbColor(255, 255, 0);
            circle3.BorderWidth = 3;
            circle3.InteriorColor = new PdfRgbColor(0, 192, 0);
            circle3.VisualRectangle = new PdfVisualRectangle(350, 470, 250, 150);
        }

        private static void CreateFileAttachmentAnnotations(PdfFixedDocument document, PdfFont font)
        {
            PdfBrush blackBrush = new PdfBrush();
            Random rnd = new Random();
            // Random binary data to be used a file content for file attachment annotations.
            byte[] fileData = new byte[256];

            PdfPage page = document.Pages.Add();

            string[] fileAttachmentAnnotationNames = new string[] 
                { 
                    "Graph", "Paperclip", "PushPin", "Tag"
                };

            page.Graphics.DrawString("B/W file attachment annotations", font, blackBrush, 50, 50);
            for (int i = 0; i < fileAttachmentAnnotationNames.Length; i++)
            {
                rnd.NextBytes(fileData);

                PdfFileAttachmentAnnotation faa = new PdfFileAttachmentAnnotation();
                faa.Author = "Xfinium.Pdf";
                faa.Contents = "I am a " + fileAttachmentAnnotationNames[i] + " annotation.";
                faa.IconName = fileAttachmentAnnotationNames[i];
                faa.Payload = fileData;
                faa.FileName = "BlackAndWhite" + fileAttachmentAnnotationNames[i] + ".dat";
                page.Annotations.Add(faa);
                faa.Location = new PdfPoint(50, 100 + 40 * i);
                page.Graphics.DrawString(fileAttachmentAnnotationNames[i], font, blackBrush, 90, 100 + 40 * i);
            }

            byte[] rgb = new byte[3];
            page.Graphics.DrawString("Color file attachment annotations", font, blackBrush, 300, 50);
            for (int i = 0; i < fileAttachmentAnnotationNames.Length; i++)
            {
                rnd.NextBytes(fileData);

                PdfFileAttachmentAnnotation faa = new PdfFileAttachmentAnnotation();
                faa.Author = "Xfinium.Pdf";
                faa.Contents = "I am a " + fileAttachmentAnnotationNames[i] + " annotation.";
                faa.IconName = fileAttachmentAnnotationNames[i];
                faa.Payload = fileData;
                faa.FileName = "Color" + fileAttachmentAnnotationNames[i] + ".dat";
                rnd.NextBytes(rgb);
                faa.OutlineColor = new PdfRgbColor(rgb[0], rgb[1], rgb[2]);
                rnd.NextBytes(rgb);
                faa.InteriorColor = new PdfRgbColor(rgb[0], rgb[1], rgb[2]);
                page.Annotations.Add(faa);
                faa.Location = new PdfPoint(300, 100 + 40 * i);
                page.Graphics.DrawString(fileAttachmentAnnotationNames[i], font, blackBrush, 340, 100 + 40 * i);
            }

            page.Graphics.DrawString("File attachment annotations with custom icons", font, blackBrush, 50, 700);
            PdfFileAttachmentAnnotation customFileAttachmentAnnotation = new PdfFileAttachmentAnnotation();
            customFileAttachmentAnnotation.Author = "Xfinium.Pdf";
            customFileAttachmentAnnotation.Contents = "File attachment annotation with custom icon.";
            page.Annotations.Add(customFileAttachmentAnnotation);
            customFileAttachmentAnnotation.IconName = "Custom icon appearance";
            customFileAttachmentAnnotation.Location = new PdfPoint(50, 720);

            PdfAnnotationAppearance customAppearance = new PdfAnnotationAppearance(50, 50);
            PdfPen redPen = new PdfPen(new PdfRgbColor(255, 0, 0), 10);
            PdfPen bluePen = new PdfPen(new PdfRgbColor(0, 0, 192), 10);
            customAppearance.Graphics.DrawRectangle(redPen, 5, 5, 40, 40);
            customAppearance.Graphics.DrawLine(bluePen, 0, 0, customAppearance.Width, customAppearance.Height);
            customAppearance.Graphics.DrawLine(bluePen, 0, customAppearance.Height, customAppearance.Width, 0);
            customAppearance.Graphics.CompressAndClose();
            customFileAttachmentAnnotation.NormalAppearance = customAppearance;
        }

        private static void CreateInkAnnotations(PdfFixedDocument document, PdfFont font)
        {
            PdfBrush blackBrush = new PdfBrush();
            Random rnd = new Random();

            PdfPage page = document.Pages.Add();

            page.Graphics.DrawString("Ink annotations", font, blackBrush, 50, 50);

            // The ink annotation will contain 3 lines, each one with 10 points.
            PdfPoint[][] points = new PdfPoint[3][];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new PdfPoint[10];
                for (int j = 0; j < points[i].Length; j++)
                {
                    points[i][j] = new PdfPoint(rnd.NextDouble() * page.Width, rnd.NextDouble() * page.Height);
                }
            }

            PdfInkAnnotation ia = new PdfInkAnnotation();
            page.Annotations.Add(ia);
            ia.Contents = "I am an ink annotation.";
            ia.InkColor = new PdfRgbColor(255, 0, 255);
            ia.InkWidth = 5;
            ia.Points = points;
        }

        private static void CreateLineAnnotations(PdfFixedDocument document, PdfFont font)
        {
            PdfBrush blackBrush = new PdfBrush();

            PdfPage page = document.Pages.Add();

            page.Graphics.DrawString("Line annotations", font, blackBrush, 50, 50);

            PdfLineEndSymbol[] les = new PdfLineEndSymbol[] 
                { 
                    PdfLineEndSymbol.Circle, PdfLineEndSymbol.ClosedArrow, PdfLineEndSymbol.None, PdfLineEndSymbol.OpenArrow
                };

            for (int i = 0; i < les.Length; i++)
            {
                PdfLineAnnotation la = new PdfLineAnnotation();
                page.Annotations.Add(la);
                la.Author = "Xfinium.Pdf";
                la.Contents = "I am a line annotation with " + les[i].ToString() + " ending.";
                la.StartPoint = new PdfPoint(50, 100 + 40 * i);
                la.EndPoint = new PdfPoint(250, 100 + 40 * i);
                la.EndLineSymbol = les[i];
                page.Graphics.DrawString("Line end symbol: " + les[i].ToString(), font, blackBrush, 270, 100 + 40 * i);
            }
        }

        private static void CreatePolygonAnnotations(PdfFixedDocument document, PdfFont font)
        {
            PdfBrush blackBrush = new PdfBrush();

            PdfPage page = document.Pages.Add();

            page.Graphics.DrawString("Polygon annotations", font, blackBrush, 50, 50);

            int[] vertices = new int[]{ 3, 4, 5, 6 };
            double centerY = 125, centerX = 150;
            double radius = 50;

            for (int i = 0; i < vertices.Length; i++)
            {
                PdfPoint[] points = new PdfPoint[vertices[i]];
                double angle = 90;
                double rotation = 360 / vertices[i];

                for (int j = 0; j < vertices[i]; j++)
                {
                    points[j] = new PdfPoint();
                    points[j].X = centerX + radius * Math.Cos(Math.PI * angle / 180);
                    points[j].Y = centerY - radius * Math.Sin(Math.PI * angle / 180);
                    angle = angle + rotation;
                }

                PdfPolygonAnnotation polygon = new PdfPolygonAnnotation();
                page.Annotations.Add(polygon);
                polygon.Author = "Xfinium.Pdf";
                polygon.Contents = "Polygon annotation with " + vertices[i] + " vertices.";
                polygon.Points = points;
                polygon.LineColor = new PdfRgbColor(192, 0, 0);
                polygon.LineWidth = 3;
                polygon.InteriorColor = new PdfRgbColor(0, 0, 192);

                centerY = centerY + 150;
            }
        }

        private static void CreatePolylineAnnotations(PdfFixedDocument document, PdfFont font)
        {
            PdfBrush blackBrush = new PdfBrush();

            PdfPage page = document.Pages.Add();

            page.Graphics.DrawString("Polyline annotations", font, blackBrush, 50, 50);

            int[] vertices = new int[] { 3, 4, 5, 6 };
            double centerY = 125, centerX = 150;
            double radius = 50;

            for (int i = 0; i < vertices.Length; i++)
            {
                PdfPoint[] points = new PdfPoint[vertices[i]];
                double angle = 90;
                double rotation = 360 / vertices[i];

                for (int j = 0; j < vertices[i]; j++)
                {
                    points[j] = new PdfPoint();
                    points[j].X = centerX + radius * Math.Cos(Math.PI * angle / 180);
                    points[j].Y = centerY - radius * Math.Sin(Math.PI * angle / 180);
                    angle = angle + rotation;
                }

                PdfPolylineAnnotation polyline = new PdfPolylineAnnotation();
                page.Annotations.Add(polyline);
                polyline.Author = "Xfinium.Pdf";
                polyline.Contents = "Polyline annotation with " + vertices[i] + " vertices.";
                polyline.Points = points;
                polyline.LineColor = new PdfRgbColor(192, 0, 0);
                polyline.LineWidth = 3;

                centerY = centerY + 150;
            }
        }

        private static void CreateRubberStampAnnotations(PdfFixedDocument document, PdfFont font)
        {
            PdfBrush blackBrush = new PdfBrush();

            PdfPage page = document.Pages.Add();

            string[] rubberStampAnnotationNames = new string[] 
                { 
                    "Approved", "AsIs", "Confidential", "Departmental", "Draft", "Experimental", "Expired", "Final", 
                    "ForComment", "ForPublicRelease", "NotApproved", "NotForPublicRelease", "Sold", "TopSecret" 
                };

            page.Graphics.DrawString("Rubber stamp annotations", font, blackBrush, 50, 50);
            for (int i = 0; i < rubberStampAnnotationNames.Length; i++)
            {
                PdfRubberStampAnnotation rsa = new PdfRubberStampAnnotation();
                rsa.Author = "Xfinium.Pdf";
                rsa.Contents = "I am a " + rubberStampAnnotationNames[i] + " rubber stamp annotation.";
                rsa.StampName = rubberStampAnnotationNames[i];
                page.Annotations.Add(rsa);
                rsa.VisualRectangle = new PdfVisualRectangle(50, 70 + 50 * i, 200, 40);
                page.Graphics.DrawString(rubberStampAnnotationNames[i], font, blackBrush, 270, 85 + 50 * i);
            }

            page.Graphics.DrawString("Stamp annotations with custom appearance", font, blackBrush, 350, 50);
            PdfRubberStampAnnotation customRubberStampAnnotation = new PdfRubberStampAnnotation();
            customRubberStampAnnotation.Contents = "Rubber stamp annotation with custom appearance.";
            customRubberStampAnnotation.StampName = "Custom";
            page.Annotations.Add(customRubberStampAnnotation);
            customRubberStampAnnotation.VisualRectangle = new PdfVisualRectangle(350, 70, 200, 40);

            PdfAnnotationAppearance customAppearance = new PdfAnnotationAppearance(50, 50);
            PdfPen redPen = new PdfPen(new PdfRgbColor(255, 0, 0), 10);
            PdfPen bluePen = new PdfPen(new PdfRgbColor(0, 0, 192), 10);
            customAppearance.Graphics.DrawRectangle(redPen, 5, 5, 40, 40);
            customAppearance.Graphics.DrawLine(bluePen, 0, 0, customAppearance.Width, customAppearance.Height);
            customAppearance.Graphics.DrawLine(bluePen, 0, customAppearance.Height, customAppearance.Width, 0);
            customAppearance.Graphics.CompressAndClose();
            customRubberStampAnnotation.NormalAppearance = customAppearance;
        }

        private static void CreateTextMarkupAnnotations(PdfFixedDocument document, PdfFont font)
        {
            PdfBrush blackBrush = new PdfBrush();

            PdfPage page = document.Pages.Add();

            page.Graphics.DrawString("Text markup annotations", font, blackBrush, 50, 50);

            PdfTextMarkupAnnotationType[] tmat = new PdfTextMarkupAnnotationType[] 
                { 
                    PdfTextMarkupAnnotationType.Highlight, PdfTextMarkupAnnotationType.Squiggly, PdfTextMarkupAnnotationType.StrikeOut, PdfTextMarkupAnnotationType.Underline
                };

            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Brush = blackBrush;
            sao.Font = font;

            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.HorizontalAlign = PdfStringHorizontalAlign.Center;
            slo.VerticalAlign = PdfStringVerticalAlign.Bottom;
            for (int i = 0; i < tmat.Length; i++)
            {
                PdfTextMarkupAnnotation tma = new PdfTextMarkupAnnotation();
                page.Annotations.Add(tma);
                tma.VisualRectangle = new PdfVisualRectangle(50, 100 + 50 * i, 200, font.Size + 2);
                tma.TextMarkupType = tmat[i];

                slo.X = 150;
                slo.Y = 100 + 50 * i + font.Size;
                
                page.Graphics.DrawString(tmat[i].ToString() + " annotation.", sao, slo);
            }
        }

        private static void CreateRichMediaAnnotations(PdfFixedDocument document, PdfFont font, Stream flashStream)
        {
            PdfBrush blackBrush = new PdfBrush();

            PdfPage page = document.Pages.Add();

            page.Graphics.DrawString("Rich media annotations", font, blackBrush, 50, 50);

            byte[] flashContent = new byte[flashStream.Length];
            flashStream.Read(flashContent, 0, flashContent.Length);

            PdfRichMediaAnnotation rma = new PdfRichMediaAnnotation();
            page.Annotations.Add(rma);
            rma.VisualRectangle = new PdfVisualRectangle(100, 100, 400, 400);
            rma.FlashPayload = flashContent;
            rma.FlashFile = "clock.swf";
            rma.ActivationCondition = PdfRichMediaActivationCondition.PageVisible;
        }

        private static void Create3DAnnotations(PdfFixedDocument document, PdfFont font, Stream u3dStream)
        {
            PdfBrush blackBrush = new PdfBrush();

            PdfPage page = document.Pages.Add();
            page.Rotation = 90;

            page.Graphics.DrawString("3D annotations", font, blackBrush, 50, 50);

            byte[] u3dContent = new byte[u3dStream.Length];
            u3dStream.Read(u3dContent, 0, u3dContent.Length);

            Pdf3DView view0 = new Pdf3DView();
            view0.CameraToWorldMatrix = new double[] { 1, 0, 0, 0, 0, -1, 0, 1, 0, -0.417542, -0.881257, -0.125705 };
            view0.CenterOfOrbit = 0.123106;
            view0.ExternalName = "Default";
            view0.InternalName = "Default";
            view0.Projection = new Pdf3DProjection();
            view0.Projection.FieldOfView = 30;

            Pdf3DView view1 = new Pdf3DView();
            view1.CameraToWorldMatrix = new double[] { -0.999888, 0.014949, 0, 0.014949, 0.999887, 0.00157084, 0.0000234825, 0.00157066, -0.999999, -0.416654, -0.761122, -0.00184508 };
            view1.CenterOfOrbit = 0.123106;
            view1.ExternalName = "Top";
            view1.InternalName = "Top";
            view1.Projection = new Pdf3DProjection();
            view1.Projection.FieldOfView = 14.8096;

            Pdf3DView view2 = new Pdf3DView();
            view2.CameraToWorldMatrix = new double[] { -1.0, -0.0000411671, 0.0000000000509201, -0.00000101387, 0.0246288, 0.999697, -0.0000411546, 0.999697, -0.0246288, -0.417072, -0.881787, -0.121915 };
            view2.CenterOfOrbit = 0.123106;
            view2.ExternalName = "Side";
            view2.InternalName = "Side";
            view2.Projection = new Pdf3DProjection();
            view2.Projection.FieldOfView = 12.3794;

            Pdf3DView view3 = new Pdf3DView();
            view3.CameraToWorldMatrix = new double[] { -0.797002, -0.603977, -0.0000000438577, -0.294384, 0.388467, 0.873173, -0.527376, 0.695921, -0.48741, -0.3518, -0.844506, -0.0675629 };
            view3.CenterOfOrbit = 0.123106;
            view3.ExternalName = "Isometric";
            view3.InternalName = "Isometric";
            view3.Projection = new Pdf3DProjection();
            view3.Projection.FieldOfView = 15.1226;

            Pdf3DView view4 = new Pdf3DView();
            view4.CameraToWorldMatrix = new double[] { 0.00737633, -0.999973, -0.0000000000147744, -0.0656414, -0.000484206, 0.997843, -0.997816, -0.00736042, -0.0656432, -0.293887, -0.757928, -0.119485 };
            view4.CenterOfOrbit = 0.123106;
            view4.ExternalName = "Front";
            view4.InternalName = "Front";
            view4.Projection = new Pdf3DProjection();
            view4.Projection.FieldOfView = 15.1226;

            Pdf3DView view5 = new Pdf3DView();
            view5.CameraToWorldMatrix = new double[] { 0.0151008, 0.999886, 0.0000000000261366, 0.0492408, -0.000743662, 0.998787, 0.998673, -0.0150825, -0.0492464, -0.540019, -0.756862, -0.118884 };
            view5.CenterOfOrbit = 0.123106;
            view5.ExternalName = "Back";
            view5.InternalName = "Back";
            view5.Projection = new Pdf3DProjection();
            view5.Projection.FieldOfView = 12.3794;

            Pdf3DStream _3dStream = new Pdf3DStream();
            _3dStream.Views.Add(view0);
            _3dStream.Views.Add(view1);
            _3dStream.Views.Add(view2);
            _3dStream.Views.Add(view3);
            _3dStream.Views.Add(view4);
            _3dStream.Views.Add(view5);
            _3dStream.Content = u3dContent;
            _3dStream.DefaultViewIndex = 0;
            Pdf3DAnnotation _3da = new Pdf3DAnnotation();
            _3da.Stream = _3dStream;

            PdfAnnotationAppearance appearance = new PdfAnnotationAppearance(200, 200);
            appearance.Graphics.DrawString("Click to activate", font, blackBrush, 50, 50);
            _3da.NormalAppearance = appearance;

            page.Annotations.Add(_3da);
            _3da.VisualRectangle = new PdfVisualRectangle(36, 36, 720, 540);

            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Font = font;
            sao.Brush = blackBrush;
            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.Y = 585 + 18 / 2;
            slo.HorizontalAlign = PdfStringHorizontalAlign.Center;
            slo.VerticalAlign = PdfStringVerticalAlign.Middle;

            PdfPen blackPen = new PdfPen(new PdfRgbColor(0, 0, 0), 1);

            page.Graphics.DrawRectangle(blackPen, 50, 585, 120, 18);
            slo.X = 50 + 120 / 2;
            page.Graphics.DrawString("Top", sao, slo);

            PdfGoTo3DViewAction gotoTopView = new PdfGoTo3DViewAction();
            gotoTopView.ViewIndex = 1;
            gotoTopView.TargetAnnotation = _3da;
            PdfLinkAnnotation linkGotoTopView = new PdfLinkAnnotation();
            page.Annotations.Add(linkGotoTopView);
            linkGotoTopView.VisualRectangle = new PdfVisualRectangle(50, 585, 120, 18);
            linkGotoTopView.Action = gotoTopView;

            page.Graphics.DrawRectangle(blackPen, 190, 585, 120, 18);
            slo.X = 190 + 120 / 2;
            page.Graphics.DrawString("Side", sao, slo);

            PdfGoTo3DViewAction gotoSideView = new PdfGoTo3DViewAction();
            gotoSideView.ViewIndex = 2;
            gotoSideView.TargetAnnotation = _3da;
            PdfLinkAnnotation linkGotoSideView = new PdfLinkAnnotation();
            page.Annotations.Add(linkGotoSideView);
            linkGotoSideView.VisualRectangle = new PdfVisualRectangle(190, 585, 120, 18);
            linkGotoSideView.Action = gotoSideView;

            page.Graphics.DrawRectangle(blackPen, 330, 585, 120, 18);
            slo.X = 330 + 120 / 2;
            page.Graphics.DrawString("Isometric", sao, slo);

            PdfGoTo3DViewAction gotoIsometricView = new PdfGoTo3DViewAction();
            gotoIsometricView.ViewIndex = 3;
            gotoIsometricView.TargetAnnotation = _3da;
            PdfLinkAnnotation linkGotoIsometricView = new PdfLinkAnnotation();
            page.Annotations.Add(linkGotoIsometricView);
            linkGotoIsometricView.VisualRectangle = new PdfVisualRectangle(330, 585, 120, 18);
            linkGotoIsometricView.Action = gotoIsometricView;

            page.Graphics.DrawRectangle(blackPen, 470, 585, 120, 18);
            slo.X = 470 + 120 / 2;
            page.Graphics.DrawString("Front", sao, slo);

            PdfGoTo3DViewAction gotoFrontView = new PdfGoTo3DViewAction();
            gotoFrontView.ViewIndex = 4;
            gotoFrontView.TargetAnnotation = _3da;
            PdfLinkAnnotation linkGotoFrontView = new PdfLinkAnnotation();
            page.Annotations.Add(linkGotoFrontView);
            linkGotoFrontView.VisualRectangle = new PdfVisualRectangle(470, 585, 120, 18);
            linkGotoFrontView.Action = gotoFrontView;

            page.Graphics.DrawRectangle(blackPen, 610, 585, 120, 18);
            slo.X = 610 + 120 / 2;
            page.Graphics.DrawString("Back", sao, slo);

            PdfGoTo3DViewAction gotoBackView = new PdfGoTo3DViewAction();
            gotoBackView.ViewIndex = 5;
            gotoBackView.TargetAnnotation = _3da;
            PdfLinkAnnotation linkGotoBackView = new PdfLinkAnnotation();
            page.Annotations.Add(linkGotoBackView);
            linkGotoBackView.VisualRectangle = new PdfVisualRectangle(610, 585, 120, 18);
            linkGotoBackView.Action = gotoBackView;
        }

        private static void CreateRedactionAnnotations(PdfFixedDocument document, PdfFont font, Stream flashStream)
        {
            PdfBrush blackBrush = new PdfBrush();

            PdfPage page = document.Pages.Add();

            page.Graphics.DrawString("Redaction annotations", font, blackBrush, 50, 50);

            PdfStandardFont helvetica = new PdfStandardFont(PdfStandardFontFace.Helvetica, 10);
            page.Graphics.DrawString(
                "Open the file with Adobe Acrobat, right click on the annotation and select \"Apply redactions\". The text under the annotation will be redacted.", 
                helvetica, blackBrush, 50, 110);

            PdfFormXObject redactionAppearance = new PdfFormXObject(250, 150);
            redactionAppearance.Graphics.DrawRectangle(new PdfBrush(PdfRgbColor.LightGreen),
                0, 0, redactionAppearance.Width, redactionAppearance.Height);
            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Brush = new PdfBrush(PdfRgbColor.DarkRed);
            sao.Font = new PdfStandardFont(PdfStandardFontFace.HelveticaBold, 32);
            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.Width = redactionAppearance.Width;
            slo.Height = redactionAppearance.Height;
            slo.X = 0;
            slo.Y = 0;
            slo.HorizontalAlign = PdfStringHorizontalAlign.Center;
            slo.VerticalAlign = PdfStringVerticalAlign.Middle;
            redactionAppearance.Graphics.DrawString("This content has been redacted", sao, slo);

            PdfRedactionAnnotation redactionAnnotation = new PdfRedactionAnnotation();
            page.Annotations.Add(redactionAnnotation);
            redactionAnnotation.Author = "XFINIUM.PDF";
            redactionAnnotation.BorderColor = new PdfRgbColor(192, 0, 0);
            redactionAnnotation.BorderWidth = 1;
            redactionAnnotation.OverlayAppearance = redactionAppearance;
            redactionAnnotation.VisualRectangle = new PdfVisualRectangle(50, 100, 250, 150);
        }
    }
}