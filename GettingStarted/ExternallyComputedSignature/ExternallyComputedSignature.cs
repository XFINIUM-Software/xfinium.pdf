using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Xfinium.Pdf.DigitalSignatures;
using Xfinium.Pdf.Forms;

namespace Xfinium.Pdf.Samples
{
    class ExternallyComputedSignature
    {
        static void Main(string[] args)
        {
            string supportPath = "..\\..\\..\\..\\..\\SupportFiles\\";

            X509Certificate2 certificate = new X509Certificate2(supportPath + "XFINIUMPDFSampleCert.pfx", "0123456789", X509KeyStorageFlags.Exportable);

            PdfFixedDocument document = new PdfFixedDocument(supportPath + "formfill.pdf");

            PdfSignatureField signField = document.Form.Fields["signhere"] as PdfSignatureField;
            PdfCmsDigitalSignature signature = new PdfCmsDigitalSignature();
            signature.OnComputeSignature += OnComputeSignature;
            signature.SignatureDigestAlgorithm = PdfDigitalSignatureDigestAlgorithm.Sha256;
            signature.Certificate = certificate;
            signature.ContactInfo = "support@xfiniumpdf.com";
            signature.Location = "XFINIUM.PDF HQ";
            signature.Name = "XFINIUM.PDF Signer";
            signature.Reason = "Demo signature";
            signField.Signature = signature;

            document.Save("xfinium.pdf.sample.simplesignature.pdf");

            Console.WriteLine("File(s) saved with success to current folder.");
        }

        public static void OnComputeSignature(PdfCmsDigitalSignature cmsSignature, PdfComputeSignatureEventData csed)
        {
            byte[] hash = null;
            string hashAlgorithmOid = "";

            switch (cmsSignature.SignatureDigestAlgorithm)
            {
                case PdfDigitalSignatureDigestAlgorithm.Sha256:
                    SHA256Managed sha256 = new SHA256Managed();
                    hash = sha256.ComputeHash(csed.MessageToSign);
                    hashAlgorithmOid = CryptoConfig.MapNameToOID("SHA256");
                    break;
            }

            RSACryptoServiceProvider rsaKey = cmsSignature.Certificate.PrivateKey as RSACryptoServiceProvider;
            if ((rsaKey != null) && rsaKey.CspKeyContainerInfo.Exportable)
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.PersistKeyInCsp = false;
                    rsa.ImportParameters(rsaKey.ExportParameters(true));

                    csed.Signature = rsa.SignHash(hash, hashAlgorithmOid);
                }
            }
        }
    }
}
