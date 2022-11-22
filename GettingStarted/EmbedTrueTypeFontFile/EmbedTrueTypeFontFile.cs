using System;
using System.IO;
using Xfinium.Pdf.Core;
using Xfinium.Pdf.Core.Cos;
using Xfinium.Pdf.Core.Filters;

namespace Xfinium.Pdf.Samples
{
    public class EmbedTrueTypeFontFile
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputFile">Input PDF file</param>
        /// <param name="outputFile">Output PDF file</param>
        /// <param name="fontName">The font name to embed, must match the name specified in the PDF file</param>
        /// <param name="fontFile">Path to full font file that will be embedded</param>
        public static void Run(string inputFile, string outputFile, string fontName, string fontFile)
        {
            using (FileStream fontStream = File.OpenRead(fontFile))
            {
                PdfFileEx pdfFile = new PdfFileEx(inputFile);

                for (int i = 0; i < pdfFile.PageCount; i++)
                {
                    PdfCosDictionary cosPageDict = pdfFile.GetPageCosDictionary(i);

                    PdfCosDictionary cosResourcesDict = cosPageDict[PdfNames.Resources] as PdfCosDictionary;
                    if (cosResourcesDict != null)
                    {
                        EmbedTrueTypeFontInResources(cosResourcesDict, fontName, fontStream);
                    }
                }

                pdfFile.Save(outputFile);
            }
        }

        private static void EmbedTrueTypeFontInResources(PdfCosDictionary cosResourcesDict, string fontName, Stream fontStream) 
        { 
            PdfCosDictionary cosFontResourceDict = cosResourcesDict[PdfNames.Font] as PdfCosDictionary;
            if (cosFontResourceDict != null) 
            {
                string[] fontIDs = cosFontResourceDict.Keys;
                foreach (string fontID in fontIDs) 
                {
                    PdfCosDictionary cosFontDict = cosFontResourceDict[fontID] as PdfCosDictionary;
                    if (cosFontDict != null) 
                    {
                        PdfCosDictionary cosOriginalFontDict = cosFontDict;
                        PdfCosArray cosDescendantFontsArray = cosFontDict[PdfNames.DescendantFonts] as PdfCosArray;  
                        if ((cosDescendantFontsArray != null) && (cosDescendantFontsArray.Length == 1) && (cosDescendantFontsArray[0] is PdfCosDictionary))
                        {
                            cosFontDict = cosDescendantFontsArray[0] as PdfCosDictionary;
                        }

                        string subtype = cosFontDict.GetKeyValueAsString(PdfNames.Subtype, "");
                        if ((subtype == "/TrueType") || (subtype == "/CIDFontType2"))
                        {
                            string cosFontName = cosFontDict.GetKeyValueAsString(PdfNames.BaseFont, "");
                            if (cosFontName.StartsWith("/"))
                            {
                                cosFontName = cosFontName.Substring(1);
                            }
                            int plusIndex = cosFontName.IndexOf('+');
                            if (plusIndex > 0)
                            {
                                cosFontName = cosFontName.Substring(plusIndex + 1);
                            }
                            if (cosFontName == fontName)
                            {
                                PdfCosDictionary cosFontDescriptorDict = cosFontDict[PdfNames.FontDescriptor] as PdfCosDictionary;
                                if (cosFontDescriptorDict != null)
                                {
                                    PdfCosStream cosFontFile2Stream = cosFontDescriptorDict[PdfNames.FontFile2] as PdfCosStream;
                                    if (cosFontFile2Stream == null)
                                    {
                                        cosFontFile2Stream = new PdfCosStream();
                                        cosFontDescriptorDict[PdfNames.FontFile2] = cosFontFile2Stream;
                                    }

                                    cosFontFile2Stream.SetStreamContent(fontStream, PdfFilterType.FlateDecode);
                                    cosFontFile2Stream[PdfNames.Length1] = new PdfCosNumber(fontStream.Length);

                                    cosOriginalFontDict[PdfNames.BaseFont] = cosFontDict[PdfNames.BaseFont] = cosFontDescriptorDict[PdfNames.FontName] = new PdfCosName("/" + fontName);
                                }
                            }
                        }
                    }
                }
            }

            PdfCosDictionary cosXObjectResourcesDict = cosResourcesDict[PdfNames.XObject] as PdfCosDictionary; 
            if (cosXObjectResourcesDict != null)
            {
                string[] xObjectIDs = cosXObjectResourcesDict.Keys;
                foreach (string xObjectID in xObjectIDs)
                {
                    PdfCosStream cosXObjectStream = cosXObjectResourcesDict[xObjectID] as PdfCosStream;
                    if (cosXObjectStream != null ) 
                    {
                        PdfCosDictionary cosXObjectStreamResourcesDict = cosXObjectStream[PdfNames.Resources] as PdfCosDictionary;
                        if (cosXObjectStreamResourcesDict!= null ) 
                        {
                            EmbedTrueTypeFontInResources(cosXObjectStreamResourcesDict, fontName, fontStream);
                        }
                    }
                }
            }
        }
    }
}
