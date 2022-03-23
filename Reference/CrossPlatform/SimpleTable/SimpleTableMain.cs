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

            
            FileStream peopleStream = new FileStream(supportPath + "people1.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
            FileStream simpleTableVerdanaStream = new FileStream(supportPath + "verdana.ttf", FileMode.Open, FileAccess.Read, FileShare.Read);
            FileStream simpleTableVerdanaBoldStream = new FileStream(supportPath + "verdanab.ttf", FileMode.Open, FileAccess.Read, FileShare.Read);
            SampleOutputInfo[] output = Xfinium.Pdf.Samples.SimpleTable.Run(simpleTableVerdanaStream, simpleTableVerdanaBoldStream, peopleStream);
            peopleStream.Dispose();
            simpleTableVerdanaStream.Dispose();
            simpleTableVerdanaBoldStream.Dispose();
            

            for (int i = 0; i < output.Length; i++)
            {
				FileStream outStream = File.OpenWrite(output[i].FileName);
                output[i].Document.Save(outStream, output[i].SecurityHandler);
				outStream.Flush();
				outStream.Dispose();
            }

            Console.WriteLine("File(s) saved with success to current folder.");
        }
    }
}
