using System;
using System.IO;

namespace Xfinium.Pdf.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            string supportPath = "..\\..\\..\\..\\..\\SupportFiles\\";

            EmbedTrueTypeFontFile.Run(supportPath + "content.pdf", "Sample_EmbedTrueTypeFontFile.pdf", "Verdana", supportPath + "verdana.ttf");

            Console.WriteLine("Font has been embedded with success");
        }
    }
}
