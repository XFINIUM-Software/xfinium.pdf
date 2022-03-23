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

            
            FileStream attachmentStream1 = new FileStream(supportPath + "fileattachments_cs.html", FileMode.Open, FileAccess.Read, FileShare.Read);
            FileStream attachmentStream2 = new FileStream(supportPath + "fileattachments_vb.html", FileMode.Open, FileAccess.Read, FileShare.Read);
            SampleOutputInfo[] output = Xfinium.Pdf.Samples.FileAttachments.Run(attachmentStream1, attachmentStream2);
            attachmentStream1.Dispose();
            attachmentStream2.Dispose();
            

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
