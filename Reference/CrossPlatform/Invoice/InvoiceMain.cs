﻿using System;
using System.IO;
using System.Reflection;
using Xfinium.Pdf;

namespace Xfinium.Pdf.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            string supportPath = "..\\..\\..\\..\\..\\..\\SupportFiles\\";

            
            FileStream logoStream = new FileStream(supportPath + "logo.png", FileMode.Open, FileAccess.Read, FileShare.Read);
            FileStream invoiceVerdanaStream = new FileStream(supportPath + "verdana.ttf", FileMode.Open, FileAccess.Read, FileShare.Read);
            FileStream invoiceVerdanaBoldStream = new FileStream(supportPath + "verdanab.ttf", FileMode.Open, FileAccess.Read, FileShare.Read);
            SampleOutputInfo[] output = Xfinium.Pdf.Samples.Invoice.Run(invoiceVerdanaStream, invoiceVerdanaBoldStream, logoStream);
            logoStream.Dispose();
            invoiceVerdanaStream.Dispose();
            invoiceVerdanaBoldStream.Dispose();
            

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
