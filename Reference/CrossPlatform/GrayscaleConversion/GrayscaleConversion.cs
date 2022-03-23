using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Transforms;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// GrayscaleConversion sample.
    /// </summary>
    public class GrayscaleConversion
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream input)
        {
            // Load the input file.
            PdfFixedDocument document = new PdfFixedDocument(input);

            PdfConvertToGrayTransform grayTransform = new PdfConvertToGrayTransform();
            PdfPageTransformer pageTransformer = new PdfPageTransformer(document.Pages[3]);
            pageTransformer.ApplyTransform(grayTransform);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.grayscaleconversion.pdf") };
            return output;
        }
    }
}