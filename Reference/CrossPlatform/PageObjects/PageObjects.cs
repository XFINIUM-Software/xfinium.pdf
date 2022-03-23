using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Core;
using Xfinium.Pdf.Content;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// PageObjects sample.
    /// </summary>
    public class PageObjects
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream input)
        {
            PdfBrush brush = new PdfBrush();
            PdfPen redPen = new PdfPen(PdfRgbColor.Red, 1);
            PdfStandardFont helvetica = new PdfStandardFont(PdfStandardFontFace.Helvetica, 10);

            PdfFixedDocument document = new PdfFixedDocument(input);

            PdfContentExtractor ce = new PdfContentExtractor(document.Pages[0]);
            PdfVisualObjectCollection voc = ce.ExtractVisualObjects(false);

            PdfPath contour = null;
            for (int i = 0; i < voc.Count; i++)
            {
                switch (voc[i].Type)
                {
                    case PdfVisualObjectType.Image:
                        PdfImageVisualObject ivo = voc[i] as PdfImageVisualObject;
                        contour = new PdfPath();
                        contour.StartSubpath(ivo.Image.ImageCorners[0].X - 5, ivo.Image.ImageCorners[0].Y + 5);
                        contour.AddLineTo(ivo.Image.ImageCorners[1].X + 5, ivo.Image.ImageCorners[1].Y + 5);
                        contour.AddLineTo(ivo.Image.ImageCorners[2].X + 5, ivo.Image.ImageCorners[2].Y - 5);
                        contour.AddLineTo(ivo.Image.ImageCorners[3].X - 5, ivo.Image.ImageCorners[3].Y - 5);
                        contour.CloseSubpath();
                        document.Pages[0].Graphics.DrawPath(redPen, contour);

                        document.Pages[0].Graphics.DrawString("Image", helvetica, brush,
                            ivo.Image.ImageCorners[0].X - 5, ivo.Image.ImageCorners[0].Y + 5);
                        break;
                    case PdfVisualObjectType.Text:
                        PdfTextVisualObject tvo = voc[i] as PdfTextVisualObject;
                        contour = new PdfPath();
                        contour.StartSubpath(tvo.TextFragment.FragmentCorners[0].X - 5, tvo.TextFragment.FragmentCorners[0].Y + 5);
                        contour.AddLineTo(tvo.TextFragment.FragmentCorners[1].X + 5, tvo.TextFragment.FragmentCorners[1].Y + 5);
                        contour.AddLineTo(tvo.TextFragment.FragmentCorners[2].X + 5, tvo.TextFragment.FragmentCorners[2].Y - 5);
                        contour.AddLineTo(tvo.TextFragment.FragmentCorners[3].X - 5, tvo.TextFragment.FragmentCorners[3].Y - 5);
                        contour.CloseSubpath();
                        document.Pages[0].Graphics.DrawPath(redPen, contour);

                        document.Pages[0].Graphics.DrawString("Text", helvetica, brush,
                            tvo.TextFragment.FragmentCorners[0].X - 5, tvo.TextFragment.FragmentCorners[0].Y + 5);
                        break;
                    case PdfVisualObjectType.Path:
                        PdfPathVisualObject pvo = voc[i] as PdfPathVisualObject;
                        // Examine all the path points and determine the minimum rectangle that bounds the path.
                        double minX = 999999, minY = 999999, maxX = -999999, maxY = -999999;
                        for (int j = 0; j < pvo.PathItems.Count; j++)
                        {
                            PdfPathItem pi = pvo.PathItems[j];
                            if (pi.Points != null)
                            {
                                for (int k = 0; k < pi.Points.Length; k++)
                                {
                                    if (minX >= pi.Points[k].X)
                                    {
                                        minX = pi.Points[k].X;
                                    }
                                    if (minY >= pi.Points[k].Y)
                                    {
                                        minY = pi.Points[k].Y;
                                    }
                                    if (maxX <= pi.Points[k].X)
                                    {
                                        maxX = pi.Points[k].X;
                                    }
                                    if (maxY <= pi.Points[k].Y)
                                    {
                                        maxY = pi.Points[k].Y;
                                    }
                                }
                            }
                        }

                        contour = new PdfPath();
                        contour.StartSubpath(minX - 5, minY - 5);
                        contour.AddLineTo(maxX + 5, minY - 5);
                        contour.AddLineTo(maxX + 5, maxY + 5);
                        contour.AddLineTo(minX - 5, maxY + 5);
                        contour.CloseSubpath();
                        document.Pages[0].Graphics.DrawPath(redPen, contour);

                        document.Pages[0].Graphics.DrawString("Path", helvetica, brush, minX - 5, maxY + 5);
                        // Skip the rest of path objects, they are the evaluation message
                        i = voc.Count;
                        break;
                }
            }

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.pageobjects.pdf") };
            return output;
        }
    }
}