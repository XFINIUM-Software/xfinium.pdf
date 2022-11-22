using System;
using System.IO;

namespace Xfinium.Pdf.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            string supportPath = "..\\..\\..\\..\\..\\SupportFiles\\";

            string taggedText = TaggedTextExtractor.ExtractText(supportPath + "invoice.pdf");

            Console.WriteLine(taggedText);
        }
    }
}
