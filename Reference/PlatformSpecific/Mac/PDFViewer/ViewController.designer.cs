// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PDFViewer
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSButton BtnFitWidth { get; set; }

		[Outlet]
		AppKit.NSButton BtnFitWidthOnDoubleClk { get; set; }

		[Outlet]
		AppKit.NSComboBox CbxZoom { get; set; }

		[Outlet]
		AppKit.NSTextField DescriptionLabel { get; set; }

		[Outlet]
		Xfinium.Pdf.View.PdfCoreView pageViewer { get; set; }

		[Outlet]
		AppKit.NSTextField PDFPathTextField { get; set; }

		[Action ("BrowseClick:")]
		partial void BrowseClick (Foundation.NSObject sender);

		[Action ("CbxZoomClick:")]
		partial void CbxZoomClick (Foundation.NSObject sender);

		[Action ("FitWidthClick:")]
		partial void FitWidthClick (Foundation.NSObject sender);

		[Action ("FitWidthOnDoubleClkClick:")]
		partial void FitWidthOnDoubleClkClick (Foundation.NSObject sender);

		[Action ("OpenFileClick:")]
		partial void OpenFileClick (Foundation.NSObject sender);

		[Action ("SaveFileClick:")]
		partial void SaveFileClick (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (BtnFitWidth != null) {
				BtnFitWidth.Dispose ();
				BtnFitWidth = null;
			}

			if (BtnFitWidthOnDoubleClk != null) {
				BtnFitWidthOnDoubleClk.Dispose ();
				BtnFitWidthOnDoubleClk = null;
			}

			if (CbxZoom != null) {
				CbxZoom.Dispose ();
				CbxZoom = null;
			}

			if (DescriptionLabel != null) {
				DescriptionLabel.Dispose ();
				DescriptionLabel = null;
			}

			if (pageViewer != null) {
				pageViewer.Dispose ();
				pageViewer = null;
			}

			if (PDFPathTextField != null) {
				PDFPathTextField.Dispose ();
				PDFPathTextField = null;
			}
		}
	}
}
