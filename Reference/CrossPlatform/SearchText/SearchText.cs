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
            PdfFixedDocument document1 = SimpleSearch(input);
            input.Position = 0;
            PdfFixedDocument document2 = SearchResultsWithContext(input);

            SampleOutputInfo[] output = new SampleOutputInfo[] 
            { 
                new SampleOutputInfo(document1, "xfinium.pdf.sample.searchtext.simple.pdf"),
                new SampleOutputInfo(document2, "xfinium.pdf.sample.searchtext.searchresultswithcontext.pdf")
            };
            return output;
        }

        private static PdfFixedDocument SimpleSearch(Stream input)
        {
            PdfFixedDocument document = new PdfFixedDocument(input);
            PdfContentExtractor ce = new PdfContentExtractor(document.Pages[0]);

            // Simple search.
            PdfTextSearchResultCollection searchResults = ce.SearchText("at");
            HighlightSearchResults(document.Pages[0], searchResults, PdfRgbColor.Red, false);

            // Whole words search.
            searchResults = ce.SearchText("at", PdfTextSearchOptions.WholeWordSearch);
            HighlightSearchResults(document.Pages[0], searchResults, PdfRgbColor.Green, false);

            // Regular expression search, find all words that start with uppercase.
            searchResults = ce.SearchText("[A-Z][a-z]*", PdfTextSearchOptions.RegExSearch);
            HighlightSearchResults(document.Pages[0], searchResults, PdfRgbColor.Blue, false);

            return document;
        }

        private static PdfFixedDocument SearchResultsWithContext(Stream input)
        {
            PdfFixedDocument document = new PdfFixedDocument(input);
            PdfContentExtractor ce = new PdfContentExtractor(document.Pages[0]);

            PdfContentExtractionContext context = new PdfContentExtractionContext();
            context.TextSeachContext = new PdfTextSearchContext();
            // How many characters we want before the search result
            context.TextSeachContext.TextSearchResultPrefixLength = 20;
            // How many characters we want after the search result
            context.TextSeachContext.TextSearchResultSuffixLength = 20;

            // Whole words search.
            PdfTextSearchResultCollection searchResults = ce.SearchText("at", PdfTextSearchOptions.WholeWordSearch, context);
            for (int i = 0; i < searchResults.Count; i++)
            {
                Console.WriteLine("Before: " + searchResults[i].PrefixText);
                Console.WriteLine("Result: " + searchResults[i].Text);
                Console.WriteLine("After: " + searchResults[i].SuffixText);
                Console.WriteLine();
            }
            HighlightSearchResults(document.Pages[0], searchResults, PdfRgbColor.Green, true);

            return document;
        }

        private static void HighlightSearchResults(PdfPage page, PdfTextSearchResultCollection searchResults, PdfColor color, bool highlightPrefixSuffix)
        {
            PdfPen pen = new PdfPen(color, 0.7);
            PdfPen pen2 = new PdfPen(color, 0.2);

            for (int i = 0; i < searchResults.Count; i++)
            {
                HighlightTextFragments(page, searchResults[i].TextFragments, pen);

                if (highlightPrefixSuffix)
                {
                    if (searchResults[i].PrefixTextFragments != null)
                    {
                        HighlightTextFragments(page, searchResults[i].PrefixTextFragments, pen2);
                    }
                    if (searchResults[i].SuffixTextFragments != null)
                    {
                        HighlightTextFragments(page, searchResults[i].SuffixTextFragments, pen2);
                    }
                }
            }
        }

        private static void HighlightTextFragments(PdfPage page, PdfTextFragmentCollection textFragments, PdfPen pen)
        {
            for (int j = 0; j < textFragments.Count; j++)
            {
                PdfPath path = new PdfPath();

                path.StartSubpath(textFragments[j].FragmentCorners[0].X, textFragments[j].FragmentCorners[0].Y);
                path.AddPolygon(textFragments[j].FragmentCorners);

                page.Graphics.DrawPath(pen, path);
            }
        }
    }
}