using Android.App;
using Android.Widget;
using Android.OS;
using System.Reflection;
using System.IO;
using Xfinium.Pdf.View;

namespace PDFViewer
{
    [Activity(Label = "XFINIUM.PDF Viewer", MainLauncher = true, ConfigurationChanges = global::Android.Content.PM.ConfigChanges.Orientation | global::Android.Content.PM.ConfigChanges.ScreenSize)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.main);

            Button btnLoadDocument = FindViewById<Button>(Resource.Id.btnLoadDocument);
            btnLoadDocument.Click += BtnLoadDocument_Click;

            pdfView = FindViewById<PdfCoreView>(Resource.Id.pdfView);
            pdfView.GraphicRendererFactory = new Xfinium.Graphics.Skia.SkiaRendererFactory();
            pdfView.Document = new PdfVisualDocument();
            pdfView.ZoomMode = PdfZoomMode.FitWidth;
            pdfView.FitWidthOnDoubleTap = true;
        }

        /// <summary>
        /// The PDF viewer
        /// </summary>
        private PdfCoreView pdfView;

        private void BtnLoadDocument_Click(object sender, System.EventArgs e)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream pdfStream = asm.GetManifestResourceStream("PDFViewer.xfinium.pdf");
            pdfView.Document.Load(pdfStream);
            pdfStream.Close();
        }
    }
}

