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

            
            File.Copy(supportPath + "content.pdf", "xfinium.pdf.sample.documentincrementalupdate.pdf", true);
            FileStream incrementalUpdateInput = 
                new FileStream("xfinium.pdf.sample.documentincrementalupdate.pdf", FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            Xfinium.Pdf.Samples.DocumentIncrementalUpdate.Run(incrementalUpdateInput);
            incrementalUpdateInput.Flush();
            incrementalUpdateInput.Dispose();

            Console.WriteLine("File(s) saved with success to current folder.");
        }
    }
}
