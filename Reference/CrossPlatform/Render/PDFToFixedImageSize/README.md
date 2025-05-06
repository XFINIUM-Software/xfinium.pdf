The `PDFToFixedImageSize` sample shows how to convert a PDF page to an image of fixed size. 
This is done by settings the `OutputImageSize` property on the `PdfRendererSettings` object. When this property is set, the `PdfRendererSettings.DpiX` and `PdfRendererSettings.DpiY` properties are ignored.
If either `Width` or `Height` properties of the `OutputImageSize` are set to 0, they are computed automatically to keep the original page aspect ratio.

