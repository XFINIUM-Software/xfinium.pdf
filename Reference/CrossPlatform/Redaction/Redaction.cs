using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Redaction;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// Redaction sample.
    /// </summary>
    public class Redaction
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream input)
        {
            PdfFixedDocument document = new PdfFixedDocument(input);
            PdfContentRedactor crText = new PdfContentRedactor(document.Pages[0]);
            // Redact a rectangular area of 200*100 points and leave the redacted area uncovered.
            crText.RedactArea(new PdfVisualRectangle(50, 50, 200, 100));
            // Redact a rectangular area of 200*100 points and mark the redacted area with red.
            crText.RedactArea(new PdfVisualRectangle(50, 350, 200, 100), PdfRgbColor.Red);

            PdfContentRedactor crImages = new PdfContentRedactor(document.Pages[2]);
            // Initialize the bulk redaction.
            crImages.BeginRedaction();
            // Prepare for redaction a rectangular area of 500*100 points and leave the redacted area uncovered.
            crImages.RedactArea(new PdfVisualRectangle(50, 50, 500, 100));
            // Prepare for redaction a rectangular area of 200*100 points and mark the redacted area with red.
            crImages.RedactArea(new PdfVisualRectangle(50, 350, 500, 100), PdfRgbColor.Red);
            // When images are redacted, the cleared pixels are set to 0. Depending on image colorspace the redacted area can appear black or colored.
            // Finish the redaction
            crImages.ApplyRedaction();

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.redaction.pdf") };
            return output;
        }
    }
}