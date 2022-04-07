using System;
using System.IO;
using Xfinium.Pdf.Core;

namespace Xfinium.Pdf.Samples
{
    class FormFillLowOverhead
    {
        static void Main(string[] args)
        {
            FileStream pdfStream = File.OpenRead("..\\..\\..\\..\\..\\..\\SupportFiles\\formfill.pdf");
            PdfFileEx pdfFile = new PdfFileEx(pdfStream);
			pdfStream.Close();
			
            // For text fields the value is the field text			
            pdfFile.SetFieldValue("firstname", "John");
            pdfFile.SetFieldValue("lastname", "Doe");

            // For radio buttons the value is the export value of the selected widget
            pdfFile.SetFieldValue("sex", "M");

            // For drop down lists and listboxes the value is the index of the selected item
            pdfFile.SetFieldValue("firstcar", 0);
            pdfFile.SetFieldValue("secondcar", 1);

            // For checkboxes the value is true for checked and false for unchecked
            pdfFile.SetFieldValue("agree", true);

            pdfFile.FlattenFormFields();
            pdfFile.Save("FormFillLowOverhead.pdf");

            Console.WriteLine("File saved with success to current folder.");
        }
    }
}
