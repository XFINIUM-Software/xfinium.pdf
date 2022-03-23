using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Transforms;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// RemoveReplaceImages sample.
    /// </summary>
    public class RemoveReplaceImages
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream input)
        {
            // Load the input file.
            PdfFixedDocument document = new PdfFixedDocument(input);

            PdfReplaceImageTransform replaceImageTransform = new PdfReplaceImageTransform();
            replaceImageTransform.ReplaceImage += new EventHandler<PdfReplaceImageEventArgs>(HandleReplaceImage);
            PdfPageTransformer pageTransformer = new PdfPageTransformer(document.Pages[2]);
            pageTransformer.ApplyTransform(replaceImageTransform);
            replaceImageTransform.ReplaceImage -= new EventHandler<PdfReplaceImageEventArgs>(HandleReplaceImage);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.removereplaceimages.pdf") };
            return output;
        }

        private static void HandleReplaceImage(object sender, PdfReplaceImageEventArgs e)
        {
            if (e.OldImageID.Value == "/Im1")
            {
                // Replace the existing image with a checkers board.
                MemoryStream checkers = new MemoryStream(new byte[] { 0, 255, 0, 255, 0, 255, 0, 255, 0, 255, 0, 255, 0, 255, 0, 255, 0, 255, 0, 255, 0, 255, 0, 255, 0 });
                PdfRawImage rawImage = new PdfRawImage(checkers);
                rawImage.Width = 5;
                rawImage.Height = 5;
                rawImage.BitsPerComponent = 8;
                rawImage.ColorSpace = new PdfGrayColorSpace();

                e.NewImage = rawImage;
            }
            else
            {
                if (e.OldImageID.Value == "/Im2")
                {
                    // Remove the image from the page by setting the new image (the replacement image) to null.
                    e.NewImage = null;
                }
            }
        }
    }
}