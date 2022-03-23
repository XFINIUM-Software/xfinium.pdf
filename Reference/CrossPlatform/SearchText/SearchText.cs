using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Content;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// Search text sample.
    /// </summary>
    public class SearchText
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream input)
        {
            PdfFixedDocument document = new PdfFixedDocument(input);
            PdfContentExtractor ce = new PdfContentExtractor(document.Pages[0]);

            // Simple search.
            PdfTextSearchResultCollection searchResults = ce.SearchText("at");
            HighlightSearchResults(document.Pages[0], searchResults, PdfRgbColor.Red);

            // Whole words search.
            searchResults = ce.SearchText("at", PdfTextSearchOptions.WholeWordSearch);
            HighlightSearchResults(document.Pages[0], searchResults, PdfRgbColor.Green);

            // Regular expression search, find all words that start with uppercase.
            searchResults = ce.SearchText("[A-Z][a-z]*", PdfTextSearchOptions.RegExSearch);
            HighlightSearchResults(document.Pages[0], searchResults, PdfRgbColor.Blue);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.searchtext.pdf") };
            return output;
        }

        private static void HighlightSearchResults(PdfPage page, PdfTextSearchResultCollection searchResults, PdfColor color)
        {
            PdfPen pen = new PdfPen(color, 0.5);

            for (int i = 0; i < searchResults.Count; i++)
            {
                PdfTextFragmentCollection tfc = searchResults[i].TextFragments;
                for (int j = 0; j < tfc.Count; j++)
                {
                    PdfPath path = new PdfPath();

                    path.StartSubpath(tfc[j].FragmentCorners[0].X, tfc[j].FragmentCorners[0].Y);
                    path.AddPolygon(tfc[j].FragmentCorners);

                    page.Graphics.DrawPath(pen, path);
                }
            }
        }
    }
}