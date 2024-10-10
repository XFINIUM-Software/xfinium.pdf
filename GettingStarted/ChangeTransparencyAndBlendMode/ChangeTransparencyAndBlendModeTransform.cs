using System;
using System.Collections.Generic;
using Xfinium.Pdf.Core;
using Xfinium.Pdf.Core.Cos;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Graphics.Operators;
using Xfinium.Pdf.Transforms;

namespace ChangeTransparencyAndBlendMode
{
    public class ChangeTransparencyAndBlendModeTransform : PdfPageTransform
    {
        public ChangeTransparencyAndBlendModeTransform() 
        {
            cachedOperators = new List<PdfContentStreamOperator>();
            cosTransparencyGs = new PdfCosName("/TransparencyGS");
            transparencyGs = new PdfExtendedGraphicState();
            transparencyGs.FillAlpha = 0.5;
            cosBlendModeGs = new PdfCosName("/BlendModeGS");
            blendModeGs = new PdfExtendedGraphicState();
            blendModeGs.BlendMode = PdfBlendMode.Multiply;
        }

        private List<PdfContentStreamOperator> cachedOperators;

        private PdfCosName cosTransparencyGs;

        private PdfExtendedGraphicState transparencyGs;

        private PdfCosName cosBlendModeGs;

        private PdfExtendedGraphicState blendModeGs;

        protected override void TransformOperator(PdfContentStreamOperator input, List<PdfContentStreamOperator> output)
        {
            switch (input.Type)
            {
                case PdfContentStreamOperatorType.MoveTo:
                case PdfContentStreamOperatorType.LineTo:
                case PdfContentStreamOperatorType.CCurveTo:
                case PdfContentStreamOperatorType.YCurveTo:
                case PdfContentStreamOperatorType.VCurveTo:
                case PdfContentStreamOperatorType.Rectangle:
                case PdfContentStreamOperatorType.CloseSubpath:
                    cachedOperators.Add(input);
                    break;
                case PdfContentStreamOperatorType.CloseFillNonZeroStroke:
                case PdfContentStreamOperatorType.CloseFillEvenOddStroke:
                case PdfContentStreamOperatorType.FillNonZeroStroke:
                case PdfContentStreamOperatorType.FillEvenOddStroke:
                case PdfContentStreamOperatorType.FillNonZero:
                case PdfContentStreamOperatorType.FillNonZero2:
                case PdfContentStreamOperatorType.FillOddEven:
                case PdfContentStreamOperatorType.EndPath:
                case PdfContentStreamOperatorType.Stroke:
                case PdfContentStreamOperatorType.CloseStroke:
                    PdfPathPaintingOperator ppo = input as PdfPathPaintingOperator;
                    PdfRgbColor fillColor = ppo.PathVisualObject.Brush.Color.ToRgbColor();
                    // Red filled paths are made transparent
                    if ((fillColor.R == 255) && (fillColor.G == 0) && (fillColor.B == 0))
                    {
                        output.Add(new PdfContentStreamOperator(PdfContentStreamOperatorType.SaveGraphicsState));
                        output.Add(new PdfSetGraphicsStateOperator(cosTransparencyGs));
                        for (int i = 0; i < cachedOperators.Count; i++)
                        {
                            output.Add(cachedOperators[i]);
                        }
                        output.Add(input);
                        output.Add(new PdfContentStreamOperator(PdfContentStreamOperatorType.RestoreGraphicsState));

                        cachedOperators.Clear();
                        AddResource(PdfNames.ExtGState, cosTransparencyGs, transparencyGs.CosDictionary);
                    }
                    // Blend mode is changed for blue filled paths
                    else if ((fillColor.R == 0) && (fillColor.G == 0) && (fillColor.B == 255))
                    {
                        output.Add(new PdfContentStreamOperator(PdfContentStreamOperatorType.SaveGraphicsState));
                        output.Add(new PdfSetGraphicsStateOperator(cosBlendModeGs));
                        for (int i = 0; i < cachedOperators.Count; i++)
                        {
                            output.Add(cachedOperators[i]);
                        }
                        output.Add(input);
                        output.Add(new PdfContentStreamOperator(PdfContentStreamOperatorType.RestoreGraphicsState));

                        cachedOperators.Clear();
                        AddResource(PdfNames.ExtGState, cosBlendModeGs, blendModeGs.CosDictionary);
                    }
                    else
                    {
                        for (int i = 0; i < cachedOperators.Count; i++)
                        {
                            output.Add(cachedOperators[i]);
                        }
                        output.Add(input);

                        cachedOperators.Clear();
                    }
                    break;
                default:
                    output.Add(input);
                    break;
            }
        }

        private void AddResource(PdfCosName cosResourceType, PdfCosName cosResourceID, PdfCosObject cosResource)
        {
            PdfCosDictionary cosResources = context.ContentStreamContainer[PdfNames.Resources] as PdfCosDictionary;
            if (cosResources == null)
            {
                cosResources = new PdfCosDictionary();
                context.ContentStreamContainer[PdfNames.Resources] = cosResources;
            }
            PdfCosDictionary cosTypeResource = cosResources[cosResourceType] as PdfCosDictionary;
            if (cosTypeResource == null)
            {
                cosTypeResource = new PdfCosDictionary();
                cosResources[cosResourceType] = cosTypeResource;
            }
            cosTypeResource[cosResourceID] = cosResource;
        }
    }
}
