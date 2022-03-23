using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Xfinium.Pdf.View;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PDFViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().Title = "XFINIUM.PDF Samples";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            viewer.Document = new Xfinium.Pdf.View.PdfVisualDocument();

            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream pdfStream =
                assembly.GetManifestResourceStream("PDFViewer.xfinium.pdf");
            viewer.Document.Load(pdfStream);
            pdfStream.Dispose();
        }

        private async void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".pdf");

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                using (var inputStream = await file.OpenReadAsync())
                {
                    using (var pdfStream = inputStream.AsStreamForRead())
                    {
                        viewer.Document.Load(pdfStream);
                    }
                }
            }
        }

        private async void btnSaveClick_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker picker = new FileSavePicker();

            StorageFile file = await picker.PickSaveFileAsync();
            if (file != null)
            {
                using (var outputStream = await file.OpenStreamForWriteAsync())
                {
                    viewer.Document.Document.Save(outputStream);
                    await outputStream.FlushAsync();
                }
            }
        }

        private void btnFitWidth_Click(object sender, RoutedEventArgs e)
        {
            viewer.ZoomMode = btnFitWidth.IsChecked.Value ? PdfZoomMode.FitWidth : PdfZoomMode.Custom;
        }

        private void btnFitWidthOnDoubleClick_Click(object sender, RoutedEventArgs e)
        {
            viewer.FitWidthOnDoubleClick = btnFitWidthOnDoubleClick.IsChecked.Value;
        }
    }
}
