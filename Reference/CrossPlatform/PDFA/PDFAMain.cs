using System;
using System.IO;
using System.Reflection;
using Xfinium.Pdf;
using Xfinium.Pdf.Samples;
using Xfinium.Pdf.Standards;

namespace Xfinium.Pdf.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            string supportPath = "..\\..\\..\\..\\..\\..\\SupportFiles\\";

            
            FileStream iccInput = new FileStream(supportPath + "rgb.icc", FileMode.Open, FileAccess.Read, FileShare.Read);
            FileStream ttfInput = new FileStream(supportPath + "verdana.ttf", FileMode.Open, FileAccess.Read, FileShare.Read);
            SampleOutputInfo[] output = Xfinium.Pdf.Samples.PDFA.Run(iccInput, ttfInput);
            iccInput.Dispose();
            ttfInput.Dispose();
            

            PdfAFormat[] pdfaFormats = new PdfAFormat[] { PdfAFormat.PdfA1b, PdfAFormat.PdfA2u, PdfAFormat.PdfA3u };
            for (int i = 0; i < output.Length; i++)
            {
				FileStream outStream = File.OpenWrite(output[i].FileName);
                PdfAFormatter.Save(output[i].Document as PdfFixedDocument, outStream, pdfaFormats[i]);
                outStream.Flush();
				outStream.Dispose();
            }

            Console.WriteLine("File(s) saved with success to current folder.");
        }
    }
}
