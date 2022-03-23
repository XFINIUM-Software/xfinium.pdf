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


            FileStream populationStream = new FileStream(supportPath + "population.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
            FileStream verdanaStream = new FileStream(supportPath + "verdana.ttf", FileMode.Open, FileAccess.Read, FileShare.Read);
            FileStream verdanaBoldStream = new FileStream(supportPath + "verdanab.ttf", FileMode.Open, FileAccess.Read, FileShare.Read);
            SampleOutputInfo[] output = Xfinium.Pdf.Samples.TableGroups.Run(verdanaStream, verdanaBoldStream, populationStream);
            populationStream.Dispose();
            verdanaStream.Dispose();
            verdanaBoldStream.Dispose();


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
