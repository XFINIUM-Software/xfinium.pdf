using System;
using Xfinium.Pdf;
using Xfinium.Pdf.Core.Security;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// Represents the output of a sample.
    /// </summary>
    public class SampleOutputInfo
    {
        /// <summary>
        /// Initializes a new <see cref="SampleOutputInfo"/> object.
        /// </summary>
        /// <param name="document">The document that represents the sample output</param>
        /// <param name="fileName">The file name of the sample output</param>
        public SampleOutputInfo(PdfDocument document, string fileName) : this(document, fileName, null)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="SampleOutputInfo"/> object.
        /// </summary>
        /// <param name="document">The document that represents the sample output</param>
        /// <param name="fileName">The file name of the sample output</param>
        /// <param name="securityHandler">Security handler for encrypting the output document</param>
        public SampleOutputInfo(PdfDocument document, string fileName, PdfSecurityHandler securityHandler)
        {
            this.document = document;
            this.fileName = fileName;
            this.securityHandler = securityHandler;
        }

        private PdfDocument document;
        /// <summary>
        /// Gets or sets the document that represents the sample output.
        /// </summary>
        public PdfDocument Document
        {
            get { return document; }
            set { document = value; }
        }

        private string fileName;
        /// <summary>
        /// Gets or sets the file name the sample is saved to disk.
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        private PdfSecurityHandler securityHandler;
        /// <summary>
        /// Gets or sets the security handler used to encrypt the output file.
        /// </summary>
        public PdfSecurityHandler SecurityHandler
        {
            get { return securityHandler; }
            set { securityHandler = value; }
        }
    }
}