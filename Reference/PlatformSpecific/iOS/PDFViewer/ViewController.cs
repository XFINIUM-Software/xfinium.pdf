using System;
using System.IO;
using Foundation;
using UIKit;

namespace PDFViewer
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            DocumentView.Document = new Xfinium.Pdf.View.PdfVisualDocument();
            DocumentView.GraphicRendererFactory = new Xfinium.Graphics.CoreGraphics.CoreGraphicsRendererFactory();
            DocumentView.ZoomMode = Xfinium.Pdf.View.PdfZoomMode.FitWidth;
            DocumentView.FitWidthOnDoubleTap = true;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void BtnLoadDocumentTouchUpInside(UIButton sender)
        {
			FileStream fs = File.OpenRead(NSBundle.MainBundle.BundlePath + "/xfinium.pdf");
			DocumentView.Document.Load(fs);
			fs.Close();
		}
    }
}
