using Xfinium.Pdf;
using Xfinium.Pdf.Core;
using Xfinium.Pdf.Core.Cos;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Graphics.Operators;
using Xfinium.Pdf.Transforms;
using System;
using System.Collections.Generic;
using System.IO;

namespace RemoveUnusedResources
{
    public class RemoveUnusedResourcesTransform : PdfPageTransform
    {
        public RemoveUnusedResourcesTransform()
        {
            colorspaces = new HashSet<string>();
            xObjects = new HashSet<string>();
            fonts = new HashSet<string>();
            patterns = new HashSet<string>();
        }

        private HashSet<string> colorspaces;

        private HashSet<string> xObjects;

        private HashSet<string> fonts;

        private HashSet<string> patterns;

        protected override void TransformOperator(PdfContentStreamOperator input, List<PdfContentStreamOperator> output)
        {
            // Scan the operators in the page content and keep track of used resources (fonts, images, colorspaces, patterns)
            switch (input.Type)
            {
                case PdfContentStreamOperatorType.SetStrokeColorSpace:
                    PdfSetStrokeColorSpaceOperator sscs = input as PdfSetStrokeColorSpaceOperator;
                    colorspaces.Add(sscs.ColorSpaceID.Value);
                    break;
                case PdfContentStreamOperatorType.SetStrokeColorN:
                    PdfSetStrokeColorNOperator sscn = input as PdfSetStrokeColorNOperator;
                    if (sscn.PatternID != null)
                    {
                        patterns.Add(sscn.PatternID.Value);
                    }
                    break;
                case PdfContentStreamOperatorType.SetFillColorSpace:
                    PdfSetFillColorSpaceOperator sfcs = input as PdfSetFillColorSpaceOperator;
                    colorspaces.Add(sfcs.ColorSpaceID.Value);
                    break;
                case PdfContentStreamOperatorType.SetFillColorN:
                    PdfSetFillColorNOperator sfcn = input as PdfSetFillColorNOperator;
                    if (sfcn.PatternID != null)
                    {
                        patterns.Add(sfcn.PatternID.Value);
                    }
                    break;
                case PdfContentStreamOperatorType.DisplayXObject:
                    PdfDisplayImageXObjectOperator ixoo = input as PdfDisplayImageXObjectOperator;
                    if (ixoo != null)
                    {
                        xObjects.Add(ixoo.ImageID.Value);
                    }
                    else
                    {
                        PdfDisplayFormXObjectOperator fxoo = input as PdfDisplayFormXObjectOperator;
                        if (fxoo != null)
                        {
                            xObjects.Add(fxoo.FormXObjectID.Value);
                        }
                    }
                    break;
                case PdfContentStreamOperatorType.SetTextFontAndSize:
                    PdfSetTextFontAndSizeOperator stfs = input as PdfSetTextFontAndSizeOperator;
                    fonts.Add(stfs.FontID.Value);
                    break;
            }

            output.Add(input);
        }

        protected override void CleanUp()
        {
            base.CleanUp();

            PdfCosDictionary resourcesDict = this.Context.ContentStreamContainer[PdfNames.Resources] as PdfCosDictionary;
            if (resourcesDict != null)
            {
                resourcesDict = CopyResources(resourcesDict);
                CleanUpResources(resourcesDict[PdfNames.ColorSpace] as PdfCosDictionary, colorspaces);
                CleanUpResources(resourcesDict[PdfNames.XObject] as PdfCosDictionary, xObjects);
                CleanUpResources(resourcesDict[PdfNames.Font] as PdfCosDictionary, fonts);
                CleanUpResources(resourcesDict[PdfNames.Pattern] as PdfCosDictionary, patterns);

                this.Context.ContentStreamContainer[PdfNames.Resources] = resourcesDict;
            }
        }

        private void CleanUpResources(PdfCosDictionary resourcesDict, HashSet<string> usedResources)
        {
            if (resourcesDict == null)
            {
                return;
            }

            string[] keys = resourcesDict.Keys;
            foreach (string key in keys)
            {
                // The resources dictionary contains a key that is not used, remove it.
                if (!usedResources.Contains(key))
                {
                    resourcesDict[key] = null;
                }
            }
        }

        private PdfCosDictionary CopyResources(PdfCosDictionary resourcesDict)
        {
            PdfCosDictionary copy = new PdfCosDictionary();
            copy[PdfNames.XObject] = Copy(resourcesDict[PdfNames.XObject] as PdfCosDictionary);
            copy[PdfNames.Font] = Copy(resourcesDict[PdfNames.Font] as PdfCosDictionary);
            copy[PdfNames.ColorSpace] = Copy(resourcesDict[PdfNames.ColorSpace] as PdfCosDictionary);
            copy[PdfNames.Pattern] = Copy(resourcesDict[PdfNames.Pattern] as PdfCosDictionary);
            copy[PdfNames.Shading] = resourcesDict[PdfNames.Shading] as PdfCosDictionary;
            copy[PdfNames.Properties] = resourcesDict[PdfNames.Properties] as PdfCosDictionary;
            copy[PdfNames.ProcSet] = resourcesDict[PdfNames.ProcSet] as PdfCosArray;

            return copy;
        }

        private PdfCosDictionary Copy(PdfCosDictionary dict)
        {
            if (dict == null)
            {
                return null;
            }

            PdfCosDictionary copy = new PdfCosDictionary();
            string[] keys = dict.Keys;
            foreach(string key in keys)
            {
                copy[key] = dict[key];
            }

            return copy;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SplitPagesWithUnusedResourcesRemoval();

            SplitPagesNoUnusedResourcesRemoval();
        }

        private static void SplitPagesWithUnusedResourcesRemoval()
        {
            using (FileStream fs = File.OpenRead("sample.pdf"))
            {
                PdfFile sourceFile = new PdfFile(fs);

                for (int i = 0; i < sourceFile.PageCount; i++)
                {
                    PdfFixedDocument document = new PdfFixedDocument();
                    PdfPage page = sourceFile.ExtractPage(i);

                    RemoveUnusedResourcesTransform transform = new RemoveUnusedResourcesTransform();
                    PdfPageTransformer pageTransformer = new PdfPageTransformer(page);
                    pageTransformer.ApplyTransform(transform);

                    document.Pages.Add(page);

                    document.Save($"UnusedResourcesRemoved.Page.{i}.pdf");
                }
            }
        }

        private static void SplitPagesNoUnusedResourcesRemoval()
        {
            using (FileStream fs = File.OpenRead("Pdf4NET.Features.pdf"))
            {
                PdfFile sourceFile = new PdfFile(fs);

                for (int i = 0; i < sourceFile.PageCount; i++)
                {
                    PdfFixedDocument document = new PdfFixedDocument();
                    PdfPage page = sourceFile.ExtractPage(i);
                    document.Pages.Add(page);

                    document.Save($"UnusedResourcesKept.Page.{i}.pdf");
                }
            }
        }
    }
}
