using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Xfinium.Pdf.DigitalSignatures;

namespace Xfinium.Pdf.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            string supportPath = "..\\..\\..\\..\\..\\..\\SupportFiles\\";

            X509Certificate2 certificate = new X509Certificate2(supportPath + "XFINIUMPDFSampleCert.pfx", "0123456789", X509KeyStorageFlags.Exportable);
            FileStream formStream = File.OpenRead(supportPath + "formfill.pdf");

            SampleOutputInfo[] output = Xfinium.Pdf.Samples.SignatureWithTimeStamp.Run(formStream, certificate, OnSignatureTimeStamp);

            formStream.Close();

            for (int i = 0; i < output.Length; i++)
            {
                FileStream outStream = new FileStream(output[i].FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                output[i].Document.Save(outStream, output[i].SecurityHandler);
                outStream.Flush();
                outStream.Dispose();
            }

            Console.WriteLine("File(s) saved with success to current folder.");
        }

        private static void OnSignatureTimeStamp(PdfTimeStampEventData eventData)
        {
            HttpWebRequest tsaReq = (HttpWebRequest)WebRequest.Create("<enter your timestamp server address here>");

            tsaReq.ContentType = "application/timestamp-query";
            tsaReq.Method = "POST";
            Stream tsaReqStream = tsaReq.GetRequestStream();
            tsaReqStream.Write(eventData.TimeStampRequest, 0, eventData.TimeStampRequest.Length);

            HttpWebResponse tsaResp = (HttpWebResponse)tsaReq.GetResponse();
            Stream tsaRespStream = tsaResp.GetResponseStream();

            byte[] buffer = new byte[65536];
            int responseSize = tsaRespStream.Read(buffer, 0, buffer.Length);

            eventData.TimeStampResponse = new byte[responseSize];
            Array.Copy(buffer, 0, eventData.TimeStampResponse, 0, responseSize);
        }
    }
}
