using System;
using System.IO;
using System.Reflection;
using Xfinium.Pdf;
using Xfinium.Pdf.Samples;

namespace Xfinium.Pdf.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            string supportPath = "..\\..\\..\\..\\..\\..\\SupportFiles\\";

            
            FileStream documentPageByPageSaveOutput = 
                new FileStream("xfinium.pdf.sample.documentpagebypagesave.pdf", FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
            Xfinium.Pdf.Samples.DocumentPageByPageSave.Run(documentPageByPageSaveOutput);
            documentPageByPageSaveOutput.Flush();
            documentPageByPageSaveOutput.Dispose();

            Console.WriteLine("File(s) saved with success to current folder.");
        }
    }
}
