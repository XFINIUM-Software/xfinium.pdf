using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Portfolios;
using Xfinium.Pdf.Core.Cos;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// Portfolios sample.
    /// </summary>
    public class Portfolios
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        /// <param name="imageStream"></param>
        /// <param name="pdfStream"></param>
        /// <param name="csStream"></param>
        /// <param name="vbStream"></param>
        public static SampleOutputInfo[] Run(Stream imageStream, Stream pdfStream, Stream csStream, Stream vbStream)
        {
            PdfPortfolio portfolio = new PdfPortfolio();

            // Build the structure that describes how to files and folders in the portfolio are presented to the user.
            PdfPortfolioAttributeDefinitions portfolioAttributeDefinitions = new PdfPortfolioAttributeDefinitions();
            PdfPortfolioAttributeDefinition nameAttribute = new PdfPortfolioAttributeDefinition();
            nameAttribute.Name = "Name";
            nameAttribute.Type = PdfPortfolioAttributeDefinitionType.String;
            portfolioAttributeDefinitions["name"] = nameAttribute;
            PdfPortfolioAttributeDefinition typeAttribute = new PdfPortfolioAttributeDefinition();
            typeAttribute.Name = "Type";
            typeAttribute.Type = PdfPortfolioAttributeDefinitionType.String;
            portfolioAttributeDefinitions["type"] = typeAttribute;

            portfolio.AttributeDefinitions = portfolioAttributeDefinitions;

            // Setup the folders structure
            PdfPortfolioFolder root = new PdfPortfolioFolder();
            root.Name = "All files";
            root.PortfolioAttributes = new PdfPortfolioItemAttributes();
            root.PortfolioAttributes["name"] = new PdfCosString("All files");

            PdfPortfolioFolder imagesFolder = new PdfPortfolioFolder();
            imagesFolder.Name = "Images";
            imagesFolder.PortfolioAttributes = new PdfPortfolioItemAttributes();
            imagesFolder.PortfolioAttributes["name"] = new PdfCosString("Images (1)");
            root.Folders.Add(imagesFolder);

            PdfPortfolioFolder pdfFolder = new PdfPortfolioFolder();
            pdfFolder.Name = "PDFs";
            pdfFolder.PortfolioAttributes = new PdfPortfolioItemAttributes();
            pdfFolder.PortfolioAttributes["name"] = new PdfCosString("PDFs (1)");
            root.Folders.Add(pdfFolder);

            PdfPortfolioFolder htmlFolder = new PdfPortfolioFolder();
            htmlFolder.Name = "HTML";
            htmlFolder.PortfolioAttributes = new PdfPortfolioItemAttributes();
            htmlFolder.PortfolioAttributes["name"] = new PdfCosString("HTML (2)");
            root.Folders.Add(htmlFolder);

            portfolio.Folders.Add(root);

            // Setup the portfolio items
            PdfPortfolioItem imageFile = new PdfPortfolioItem();
            imageFile.Folder = imagesFolder;
            byte[] data = new byte[imageStream.Length];
            imageStream.Read(data, 0, data.Length);
            imageFile.Payload = data;
            imageFile.FileName = "image.jpg";
            imageFile.Attributes = new PdfPortfolioItemAttributes();
            imageFile.Attributes["name"] = new PdfCosString("image.jpg");
            imageFile.Attributes["type"] = new PdfCosString("JPEG image");
            portfolio.Items.Add(imageFile);

            PdfPortfolioItem pdfFile = new PdfPortfolioItem();
            pdfFile.Folder = pdfFolder;
            data = new byte[pdfStream.Length];
            pdfStream.Read(data, 0, data.Length);
            pdfFile.Payload = data;
            pdfFile.FileName = "content.pdf";
            pdfFile.Attributes = new PdfPortfolioItemAttributes();
            pdfFile.Attributes["name"] = new PdfCosString("content.pdf");
            pdfFile.Attributes["type"] = new PdfCosString("PDF file");
            portfolio.Items.Add(pdfFile);

            PdfPortfolioItem csFile = new PdfPortfolioItem();
            csFile.Folder = htmlFolder;
            data = new byte[csStream.Length];
            csStream.Read(data, 0, data.Length);
            csFile.Payload = data;
            csFile.FileName = "portfolios.cs.html";
            csFile.Attributes = new PdfPortfolioItemAttributes();
            csFile.Attributes["name"] = new PdfCosString("portfolios.cs.html");
            csFile.Attributes["type"] = new PdfCosString("HTML file");
            portfolio.Items.Add(csFile);

            PdfPortfolioItem vbFile = new PdfPortfolioItem();
            vbFile.Folder = htmlFolder;
            data = new byte[vbStream.Length];
            vbStream.Read(data, 0, data.Length);
            vbFile.Payload = data;
            vbFile.FileName = "portfolios.vb.html";
            vbFile.Attributes = new PdfPortfolioItemAttributes();
            vbFile.Attributes["name"] = new PdfCosString("portfolios.vb.html");
            vbFile.Attributes["type"] = new PdfCosString("HTML file");
            portfolio.Items.Add(vbFile);

            portfolio.StartupDocument = pdfFile;

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(portfolio, "xfinium.pdf.sample.portfolios.pdf") };
            return output;
        }
    }
}