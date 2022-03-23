using System;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Annotations;
using Xfinium.Pdf.Actions;
using Xfinium.Pdf.Destinations;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Xfinium.Pdf.Forms;
using Xfinium.Pdf.DigitalSignatures;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// SimpleSignature sample.
    /// </summary>
    public class SimpleSignature
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream formStream, X509Certificate2 certificate)
        {
            PdfFixedDocument document = new PdfFixedDocument(formStream);

            PdfSignatureField signField = document.Form.Fields["signhere"] as PdfSignatureField;
            PdfCmsDigitalSignature signature = new PdfCmsDigitalSignature();
            signature.SignatureDigestAlgorithm = PdfDigitalSignatureDigestAlgorithm.Sha256;
            signature.Certificate = certificate;
            signature.ContactInfo = "support@xfiniumpdf.com";
            signature.Location = "XFINIUM.PDF HQ";
            signature.Name = "XFINIUM.PDF Signer";
            signature.Reason = "Demo signature";
            signField.Signature = signature;

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.simplesignature.pdf") };
            return output;
        }
    }
}