using System;
using System.Collections.Generic;
using System.Text;
using Xfinium.Pdf.Content;
using Xfinium.Pdf.LogicalStructure;

namespace Xfinium.Pdf.Samples
{
    public class TaggedTextExtractor
    {
        /// <summary>
        /// Extracts the text from the given file in the order specified by the document structure tree.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ExtractText(string fileName)
        {
            string text = "";

            PdfFixedDocument document = new PdfFixedDocument(fileName);
            PdfStructureTree structureTree = document.StructureTree;
            if (structureTree != null)
            {
                List<PdfStructureElement> contentItemStructureElements = GetStructureElementsInReadingOrder(structureTree);

                List<PdfTextFragment> documentTextFragments = GetDocumentTextFragments(document);

                documentTextFragments = GetTextFragmentsInReadingOrder(contentItemStructureElements, documentTextFragments);

                text = ConvertTextFragmentsToText(documentTextFragments);
            }

            return text;
        }

        private static List<PdfStructureElement> GetStructureElementsInReadingOrder(PdfStructureTree structureTree)
        {
            List<PdfStructureElement> contentItemElements = new List<PdfStructureElement>();
            PdfStructureElementCollection rootCollection = structureTree.StructureElements as PdfStructureElementCollection;
            if (rootCollection == null)
            {
                PdfStructureElement rootElement = structureTree.StructureElements as PdfStructureElement;
                if (rootElement != null)
                {
                    rootCollection = new PdfStructureElementCollection();
                    rootCollection.Add(rootElement);
                }
            }
            for (int i = 0; i < rootCollection.Count; i++)
            {
                CopyContentItemElements(rootCollection[i], contentItemElements);
            }

            return contentItemElements;
        }

        /// <summary>
        /// Copies the structure elements that represents content items (leaf nodes) to a list.
        /// The order of the elements in the list is the reading order.
        /// </summary>
        /// <param name="structureElements"></param>
        /// <param name="contentItemElements"></param>
        private static void CopyContentItemElements(PdfStructureElement structureElement, List<PdfStructureElement> contentItemElements)
        {
            if (structureElement == null)
            {
                // For situations when the structure tree is invalid
                return;
            }

            if ((structureElement.Children is PdfMarkedContentReference) ||
                (structureElement.Children is PdfMarkedContentSequenceIdentifier) ||
                (structureElement.Children is PdfObjectReference))
            {
                contentItemElements.Add(structureElement);
            }
            else if ((structureElement.Children is PdfStructureElementContentCollection contentCollection) && (contentCollection.Count > 0))
            {
                bool structureElementAdded = false;

                for (int i = 0; i < contentCollection.Count; i++)
                {
                    if (((contentCollection[i] is PdfMarkedContentReference) ||
                        (contentCollection[i] is PdfMarkedContentSequenceIdentifier) ||
                        (contentCollection[i] is PdfObjectReference)) && !structureElementAdded)
                    {
                        contentItemElements.Add(structureElement);
                        structureElementAdded = true;
                    }
                    else if (contentCollection[i] is PdfStructureElement)
                    {
                        CopyContentItemElements(contentCollection[i] as PdfStructureElement, contentItemElements);
                    }
                }
            }
        }

        /// <summary>
        /// Extracts the text fragments from the entire document.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private static List<PdfTextFragment> GetDocumentTextFragments(PdfFixedDocument document)
        {
            PdfContentExtractionContext context = new PdfContentExtractionContext();
            List<PdfTextFragment> fragments = new List<PdfTextFragment>();

            for (int i = 0; i < document.Pages.Count; i++)
            {
                PdfContentExtractor ce = new PdfContentExtractor(document.Pages[i]);
                PdfTextFragmentCollection tfc = ce.ExtractTextFragments(context);
                fragments.AddRange(tfc);
            }

            return fragments;
        }

        /// <summary>
        /// Extracts the text fragments from the entire document.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private static List<PdfTextFragment> GetTextFragmentsInReadingOrder(List<PdfStructureElement> contentItemStructureElements, List<PdfTextFragment> documentTextFragments)
        {
            List<PdfTextFragment> fragments = new List<PdfTextFragment>();

            for (int i = 0; i < contentItemStructureElements.Count; i++)
            {
                for (int j = 0; j < documentTextFragments.Count; j++)
                {
                    if (contentItemStructureElements[i] == documentTextFragments[j].StructureElement)
                    {
                        fragments.Add(documentTextFragments[j]);
                    }
                }
            }

            return fragments;
        }

        /// <summary>
        /// Converts a list of text fragments into text.
        /// </summary>
        /// <param name="textFragments"></param>
        /// <returns></returns>
        private static string ConvertTextFragmentsToText(List<PdfTextFragment> textFragments)
        {
            double horizontalOffsetFactorForSpace = 0.13;
            double verticalOffsetFactorForNewLine = 0.3;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < textFragments.Count; i++)
            {
                if (i > 0)
                {
                    if ((Math.Abs(textFragments[i - 1].FragmentCorners[0].Y - textFragments[i].FragmentCorners[0].Y) > textFragments[i].TransformedFontSize * verticalOffsetFactorForNewLine) &&
                        (Math.Abs(textFragments[i - 1].FragmentCorners[3].Y - textFragments[i].FragmentCorners[3].Y) > textFragments[i].TransformedFontSize * verticalOffsetFactorForNewLine))
                    {
                        sb.Append("\r\n");
                    }
                    else
                    {
                        if (Math.Abs(textFragments[i].FragmentCorners[0].X - textFragments[i - 1].FragmentCorners[2].X) > textFragments[i].TransformedFontSize * horizontalOffsetFactorForSpace)
                        {
                            if ((textFragments[i].Text.Length == 0) || (textFragments[i].Text[0] != ' '))
                            {
                                sb.Append(" ");
                            }
                        }
                    }
                }

                sb.Append(textFragments[i].Text);
            }

            return sb.ToString();
        }
    }
}
