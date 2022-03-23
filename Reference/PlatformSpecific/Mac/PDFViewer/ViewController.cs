using System;
using System.IO;
using System.Threading.Tasks;
using AppKit;
using CoreGraphics;
using Foundation;
using Xfinium.Pdf;
using Xfinium.Pdf.Rendering;
using Xfinium.Pdf.View;

namespace PDFViewer
{
    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            pageViewer.GraphicRendererFactory = new Xfinium.Graphics.CoreGraphics.CoreGraphicsRendererFactory();
			pageViewer.Document = new Xfinium.Pdf.View.PdfVisualDocument();
            pageViewer.ZoomModeChanged += PageViewer_ZoomModeChanged;

            LoadPDFFile(NSBundle.MainBundle.ResourcePath + "/xfinium.pdf");
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        partial void BrowseClick(NSObject sender)
        {
			NSOpenPanel dlg = new NSOpenPanel();
			dlg.Title = "Open PDF document";
			dlg.AllowedFileTypes = new string[] { "pdf" };
			dlg.CanChooseDirectories = false;

			nint result = dlg.RunModal();
			if (result == 1)
			{
                LoadPDFFile(dlg.Filename);
			}
		}

        private void LoadPDFFile(string path)
        {
			FileStream pdfStream = File.OpenRead(path);
			pageViewer.Document.Load(pdfStream);
			pdfStream.Close();
		}

		partial void OpenFileClick(NSObject sender)
		{
            NSOpenPanel dlg = new NSOpenPanel();
            dlg.Title = "Open PDF document";
            dlg.AllowedFileTypes = new string[] { "pdf" };
            dlg.CanChooseDirectories = false;

            nint result = dlg.RunModal();
            if (result == 1)
            {
                LoadPDFFile(dlg.Filename);
            }
		}

		partial void SaveFileClick(NSObject sender)
		{
            if (pageViewer.Document.Document == null)
            {
                return;    
            }

            NSSavePanel dlg = new NSSavePanel();
            dlg.Title = "Save PDF document";
            dlg.AllowedFileTypes = new string[] { "pdf" };

            nint result = dlg.RunModal();
            if (result == 1)
            {
                pageViewer.Document.Document.Save(dlg.Filename);
            }
		}

		partial void CbxZoomClick(NSObject sender)
		{
            pageViewer.Zoom = int.Parse((sender as NSPopUpButton).SelectedItem.Title);
		}

		partial void FitWidthClick(NSObject sender)
		{
            pageViewer.ZoomMode = BtnFitWidth.State == NSCellStateValue.On ? PdfZoomMode.FitWidth : PdfZoomMode.Custom;
		}

		partial void FitWidthOnDoubleClkClick(NSObject sender)
		{
            pageViewer.FitWidthOnDoubleClick = BtnFitWidthOnDoubleClk.State == NSCellStateValue.On;
		}

        void PageViewer_ZoomModeChanged(object sender, EventArgs e)
        {
            BtnFitWidth.State = pageViewer.ZoomMode == PdfZoomMode.FitWidth ? NSCellStateValue.On : NSCellStateValue.Off;
        }
	}
}
