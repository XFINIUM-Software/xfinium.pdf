using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Destinations;
using Xfinium.Pdf.Actions;
using Xfinium.Pdf.Core;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// DocumentSplit sample.
    /// </summary>
    public class DocumentSplit
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream input)
        {
            // The input file is split by extracting pages from source file and inserting them in new empty PDF documents.
            PdfFile file = new PdfFile(input);
            SampleOutputInfo[] output = new SampleOutputInfo[file.PageCount];

            for (int i = 0; i < file.PageCount; i++)
            {
                PdfFixedDocument document = new PdfFixedDocument();
                PdfPage page = file.ExtractPage(i);
                document.Pages.Add(page);

                output[i] = new SampleOutputInfo(document, string.Format("xfinium.pdf.sample.documentsplit.{0}.pdf", i + 1));
            }

            return output;
        }
    }
}