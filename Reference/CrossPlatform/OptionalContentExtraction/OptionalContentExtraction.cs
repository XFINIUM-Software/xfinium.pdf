using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Core;
using Xfinium.Pdf.Content;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// OptionalContentExtraction sample.
    /// </summary>
    public class OptionalContentExtraction
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream input)
        {
            PdfFile file = new PdfFile(input);

            PdfFixedDocument document = new PdfFixedDocument();
            PdfPage page = document.Pages.Add();

            PdfPageOptionalContent oc1 = file.ExtractPageOptionalContentGroup(4, "1");
            page.Graphics.DrawFormXObject(oc1, 0, 0, page.Width / 2, page.Height / 2);
            PdfPageOptionalContent oc2 = file.ExtractPageOptionalContentGroup(4, "2");
            page.Graphics.DrawFormXObject(oc2, page.Width / 2, 0, page.Width / 2, page.Height / 2);
            PdfPageOptionalContent oc3 = file.ExtractPageOptionalContentGroup(4, "3");
            page.Graphics.DrawFormXObject(oc3, 0, page.Height / 2, page.Width / 2, page.Height / 2);
            PdfPageOptionalContent oc4 = file.ExtractPageOptionalContentGroup(4, "4");
            page.Graphics.DrawFormXObject(oc4, page.Width / 2, page.Height / 2, page.Width / 2, page.Height / 2);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.optionalcontentextraction.pdf") };
            return output;
        }
    }
}