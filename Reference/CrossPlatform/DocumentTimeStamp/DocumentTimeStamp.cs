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
    /// DocumentTimeStamp sample.
    /// </summary>
    public class DocumentTimeStamp
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream formStream, SignatureTimestampNeeded onTimeStamp)
        {
            PdfFixedDocument document = new PdfFixedDocument(formStream);

            PdfSignatureField signField = document.Form.Fields["signhere"] as PdfSignatureField;
            signField.Signature = new PdfDocumentTimeStamp();
            (signField.Signature as PdfDocumentTimeStamp).TimestampDigestAlgorithm = PdfDigitalSignatureDigestAlgorithm.Sha256;
            (signField.Signature as PdfDocumentTimeStamp).OnSignatureTimestampNeeded += onTimeStamp;

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.documenttimestamp.pdf") };
            return output;
        }
    }
}