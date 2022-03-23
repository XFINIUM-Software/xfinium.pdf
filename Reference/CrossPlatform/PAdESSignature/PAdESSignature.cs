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
    /// PAdESSignature sample.
    /// </summary>
    public class PAdESSignature
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream formStream, X509Certificate2 certificate)
        {
            PdfFixedDocument document = new PdfFixedDocument(formStream);

            document.PdfVersion = PdfVersion.Version17;
            document.VersionExtension = new PdfVersionExtension("/ESIC", 2, PdfVersion.Version17);

            PdfSignatureField signField = document.Form.Fields["signhere"] as PdfSignatureField;
            PdfPadesDigitalSignature signature = new PdfPadesDigitalSignature();
            signature.SignatureDigestAlgorithm = PdfDigitalSignatureDigestAlgorithm.Sha256;
            signature.Certificate = certificate;
            signature.ContactInfo = "support@xfiniumpdf.com";
            signature.Location = "XFINIUM.PDF HQ";
            signature.Name = "XFINIUM.PDF Signer";
            signature.Reason = "Demo signature";
            signField.Signature = signature;

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.padessignature.pdf") };
            return output;
        }
    }
}