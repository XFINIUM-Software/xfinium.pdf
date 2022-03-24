// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace PDFViewer
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnLoadDocument { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Xfinium.Pdf.View.PdfCoreView DocumentView { get; set; }

        [Action ("BtnLoadDocumentTouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnLoadDocumentTouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (BtnLoadDocument != null) {
                BtnLoadDocument.Dispose ();
                BtnLoadDocument = null;
            }

            if (DocumentView != null) {
                DocumentView.Dispose ();
                DocumentView = null;
            }
        }
    }
}