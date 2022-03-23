using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Content;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// BatesNumbers sample.
    /// </summary>
    public class BatesNumbers
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream input)
        {
            // Load the input file.
            PdfFixedDocument document = new PdfFixedDocument(input);

            PdfBatesNumberAppearance bna = new PdfBatesNumberAppearance();
            bna.Location = new PdfPoint(25, 5);
            bna.Brush = new PdfBrush(PdfRgbColor.DarkRed);

            PdfBatesNumberProvider bnp = new PdfBatesNumberProvider();
            bnp.Prefix = "XFINIUM";
            bnp.Suffix = "PDF";
            bnp.StartNumber = 1;

            PdfBatesNumber.WriteBatesNumber(document, bnp, bna);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.batesnumbers.pdf") };
            return output;
        }
    }
}