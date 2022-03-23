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
            
            FileStream logoStream = new FileStream(supportPath + "logo.png", FileMode.Open, FileAccess.Read, FileShare.Read);
            FileStream verdanaStream = new FileStream(supportPath + "verdana.ttf", FileMode.Open, FileAccess.Read, FileShare.Read);
            FileStream verdanaBoldStream = new FileStream(supportPath + "verdanab.ttf", FileMode.Open, FileAccess.Read, FileShare.Read);
            SampleOutputInfo output = Xfinium.Pdf.Samples.PDFUA.Run(verdanaStream, verdanaBoldStream, logoStream);
            logoStream.Dispose();
            verdanaStream.Dispose();
            verdanaBoldStream.Dispose();

			FileStream outStream = File.OpenWrite(output.FileName);
            PdfUAFormatter.Save(output.Document as PdfFixedDocument, outStream, PdfUAFormat.PdfUA1);
            outStream.Flush();
			outStream.Dispose();

            Console.WriteLine("File(s) saved with success to current folder.");
        }
    }
}
