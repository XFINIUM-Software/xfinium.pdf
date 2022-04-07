using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Core;

namespace Xfinium.Pdf.Samples
{
    class ResourceOptimization
    {
        static void Main(string[] args)
        {
            string fileName = "..\\..\\..\\..\\..\\..\\SupportFiles\\content.pdf";

            PDFMergeWithoutResourceOptimization(fileName);
            PDFMergeWithResourceOptimization(fileName);
        }

        private static void PDFMergeWithoutResourceOptimization(string fileName)
        {
            PdfFixedDocument document = new PdfFixedDocument();

            for (int i = 0; i < 5; i++)
            {
                using (FileStream pdfStream = File.OpenRead(fileName))
                {
                    PdfFile pdfFile = new PdfFile(pdfStream);

                    PdfPage[] pages = pdfFile.ExtractPages(0, 4);
                    for (int j = 0; j < pages.Length; j++)
                    {
                        document.Pages.Add(pages[j]);
                    }
                }
            }

            document.Save("PDFMergeWithoutResourceOptimization.pdf");

            FileInfo fileInfo = new FileInfo("PDFMergeWithoutResourceOptimization.pdf");
            Console.WriteLine("PDF merge without resource optimization - output file size: {0}", fileInfo.Length);
        }

        private static void PDFMergeWithResourceOptimization(string fileName)
        {
            PdfFixedDocument document = new PdfFixedDocument();

            for (int i = 0; i < 5; i++)
            {
                using (FileStream pdfStream = File.OpenRead(fileName))
                {
                    PdfFile pdfFile = new PdfFile(pdfStream);

                    PdfPage[] pages = pdfFile.ExtractPages(0, 4);
                    for (int j = 0; j < pages.Length; j++)
                    {
                        document.Pages.Add(pages[j]);
                    }
                }
            }

            PdfResourceOptimizer resourceOptimizer = new PdfResourceOptimizer(document);
            resourceOptimizer.MergeFonts();
            resourceOptimizer.MergeImages();
            document.Save("PDFMergeWithResourceOptimization.pdf");

            FileInfo fileInfo = new FileInfo("PDFMergeWithResourceOptimization.pdf");
            Console.WriteLine("PDF merge with resource optimization - output file size: {0}", fileInfo.Length);
        }
    }
}
