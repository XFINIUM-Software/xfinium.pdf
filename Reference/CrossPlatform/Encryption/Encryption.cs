using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Content;
using Xfinium.Pdf.Core.Security;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// Encryption sample.
    /// </summary>
    public class Encryption
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream input)
        {
            PdfRc4SecurityHandler rc4_40 = new PdfRc4SecurityHandler();
            PdfFixedDocument document1 = EncryptRC4(40, rc4_40);
            PdfRc4SecurityHandler rc4_128 = new PdfRc4SecurityHandler();
            PdfFixedDocument document2 = EncryptRC4(128, rc4_128);

            PdfAesSecurityHandler aes128 = new PdfAesSecurityHandler();
            PdfFixedDocument document3 = EncryptAES(128, aes128);
            PdfAesSecurityHandler aes256 = new PdfAesSecurityHandler();
            PdfFixedDocument document4 = EncryptAES(256, aes256);
            PdfAesSecurityHandler aes256e = new PdfAesSecurityHandler();
            aes256e.UseEnhancedPasswordValidation = true;
            PdfFixedDocument document5 = EncryptAES(256, aes256e);
            PdfFixedDocument document6 = Decrypt(input);

            SampleOutputInfo[] output = new SampleOutputInfo[] 
                { 
                    new SampleOutputInfo(document1, "xfinium.pdf.sample.encryption.rc4.40bit.pdf", rc4_40),
                    new SampleOutputInfo(document2, "xfinium.pdf.sample.encryption.rc4.128bit.pdf", rc4_128),
                    new SampleOutputInfo(document3, "xfinium.pdf.sample.encryption.aes.128bit.pdf", aes128),
                    new SampleOutputInfo(document4, "xfinium.pdf.sample.encryption.aes.256bit.pdf", aes256),
                    new SampleOutputInfo(document5, "xfinium.pdf.sample.encryption.aes.256bit.enhanced.pdf", aes256e),
                    new SampleOutputInfo(document6, "xfinium.pdf.sample.encryption.decrypted.pdf"),
                };
            return output;
        }

        /// <summary>
        /// Generates a PDF document that is encrypted using RC4 method.
        /// </summary>
        /// <param name="keySize">The encryption key size.</param>
        /// <param name="rc4"></param>
        /// <returns></returns>
        private static PdfFixedDocument EncryptRC4(int keySize, PdfRc4SecurityHandler rc4)
        {
            PdfFixedDocument doc = new PdfFixedDocument();
            rc4.EnableContentExtractionForAccessibility = false;
            rc4.EnableDocumentAssembly = false;
            rc4.EnableDocumentChange = false;
            rc4.EnableContentExtraction = false;
            rc4.EnableFormsFill = false;
            rc4.EnableAnnotationsAndFieldsEdit = false;
            rc4.EnablePrint = false;
            rc4.EncryptMetadata = true;
            rc4.KeySize = keySize;
            rc4.UserPassword = "userpass";
            rc4.OwnerPassword = "ownerpass";

            PdfPage page = doc.Pages.Add();
            PdfStandardFont helvetica = new PdfStandardFont(PdfStandardFontFace.HelveticaBoldItalic, 16);
            PdfBrush blackBrush = new PdfBrush();
            page.Graphics.DrawString(string.Format("Encryption: RC4 {0} bit", keySize), helvetica, blackBrush, 50, 100);

            return doc;
        }

        /// <summary>
        /// Generates a PDF document that is encrypted using AES method.
        /// </summary>
        /// <param name="keySize">The encryption key size.</param>
        /// <param name="aes"></param>
        /// <returns></returns>
        private static PdfFixedDocument EncryptAES(int keySize, PdfAesSecurityHandler aes)
        {
            PdfFixedDocument doc = new PdfFixedDocument();
            aes.EnableContentExtractionForAccessibility = false;
            aes.EnableDocumentAssembly = false;
            aes.EnableDocumentChange = false;
            aes.EnableContentExtraction = false;
            aes.EnableFormsFill = false;
            aes.EnableAnnotationsAndFieldsEdit = false;
            aes.EnablePrint = false;
            aes.EncryptMetadata = true;
            aes.KeySize = keySize;
            aes.UserPassword = "userpass";
            aes.OwnerPassword = "ownerpass";

            PdfPage page = doc.Pages.Add();
            PdfStandardFont helvetica = new PdfStandardFont(PdfStandardFontFace.HelveticaBoldItalic, 16);
            PdfBrush blackBrush = new PdfBrush();

            string text = string.Format("Encryption: AES {0} bit", keySize);
            if ((aes.KeySize == 256) && aes.UseEnhancedPasswordValidation)
            {
                text = text + " with enhanced password validation (Acrobat X or later)";
            }
            page.Graphics.DrawString(text, helvetica, blackBrush, 50, 100);

            return doc;
        }

        /// <summary>
        /// Decrypts a PDF file
        /// </summary>
        /// <param name="input">Input stream.</param>
        /// <returns></returns>
        private static PdfFixedDocument Decrypt(Stream input)
        {
            PdfFixedDocument doc = new PdfFixedDocument(input, "xfiniumopen");

            PdfPage page = doc.Pages[0];
            PdfStandardFont helvetica = new PdfStandardFont(PdfStandardFontFace.HelveticaBoldItalic, 16);
            PdfBrush blackBrush = new PdfBrush();
            page.Graphics.DrawString("Decrypted document", helvetica, blackBrush, 5, 5);

            return doc;
        }
    }
}