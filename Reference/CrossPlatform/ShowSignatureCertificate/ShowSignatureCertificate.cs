using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Xfinium.Pdf;
using Xfinium.Pdf.DigitalSignatures;
using Xfinium.Pdf.Forms;
using Xfinium.Pdf.Samples;
using Xfinium.Pdf.Utilities;

namespace Xfinium.Pdf.Samples
{
    class ShowSignatureCertificate
    {
        static void Main(string[] args)
        {
            string supportPath = "..\\..\\..\\..\\..\\..\\SupportFiles\\";

            FileStream signedFile = File.OpenRead(supportPath + "xfinium.pdf");
            PdfFixedDocument document = new PdfFixedDocument(signedFile);
            PdfSignatureField signature1Field = document.Form.Fields["Signature2"] as PdfSignatureField;

            PdfComputedDigitalSignature signature1 = signature1Field.Signature as PdfComputedDigitalSignature;
            X509Certificate2 x509 = signature1.Certificate;

            //Print to console information contained in the certificate.
            Console.WriteLine("{0}Subject: {1}{0}", Environment.NewLine, x509.Subject);
            Console.WriteLine("{0}Issuer: {1}{0}", Environment.NewLine, x509.Issuer);
            Console.WriteLine("{0}Version: {1}{0}", Environment.NewLine, x509.Version);
            Console.WriteLine("{0}Valid Date: {1}{0}", Environment.NewLine, x509.NotBefore);
            Console.WriteLine("{0}Expiry Date: {1}{0}", Environment.NewLine, x509.NotAfter);
            Console.WriteLine("{0}Thumbprint: {1}{0}", Environment.NewLine, x509.Thumbprint);
            Console.WriteLine("{0}Serial Number: {1}{0}", Environment.NewLine, x509.SerialNumber);
            Console.WriteLine("{0}Friendly Name: {1}{0}", Environment.NewLine, x509.PublicKey.Oid.FriendlyName);
            Console.WriteLine("{0}Public Key Format: {1}{0}", Environment.NewLine, x509.PublicKey.EncodedKeyValue.Format(true));
            Console.WriteLine("{0}Raw Data Length: {1}{0}", Environment.NewLine, x509.RawData.Length);
            Console.WriteLine("{0}Certificate to string: {1}{0}", Environment.NewLine, x509.ToString(true)); 
        }
    }
}
