using Xfinium.Pdf;
using System;
using System.Collections.Generic;
using System.IO;

namespace ChangeTransparencyAndBlendMode
{
    class Program
    {
        static void Main(string[] args)
        {
            ChangeTransparencyAndBlendModeTransform tr = new ChangeTransparencyAndBlendModeTransform();
            tr.EnableExtendedOperatorInformation = true;

            PdfFixedDocument document = new PdfFixedDocument("Sample.pdf");
            PdfPageTransformer pageTransformer = new PdfPageTransformer(document.Pages[0]);
            pageTransformer.ApplyTransform(tr);
            document.Save("Sample_Transformed.pdf");
        }
    }
}
