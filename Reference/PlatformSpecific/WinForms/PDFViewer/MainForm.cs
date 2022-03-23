using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xfinium.Pdf.View;

namespace PDFViewer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            documentView.GraphicRendererFactory = new Xfinium.Graphics.Gdi.GdiRendererFactory();
            documentView.Document = new Xfinium.Pdf.View.PdfVisualDocument();
            documentView.PageView.ZoomModeChanged += pageView_ZoomModeChanged;
            LoadPDFFile("..\\..\\..\\..\\..\\..\\SupportFiles\\xfinium.pdf");
        }

        private void pageView_ZoomModeChanged(object sender, EventArgs e)
        {
            tsbtnFitWidth.Checked = documentView.PageView.ZoomMode == PdfZoomMode.FitWidth;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadPDFFile(ofd.FileName);
            }
        }

        private void LoadPDFFile(string filePath)
        {
            FileStream pdfStream = File.OpenRead(filePath);
            documentView.Document.Load(pdfStream);
            pdfStream.Close();
        }

        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                documentView.Document.Document.Save(sfd.FileName);
            }
        }

        private void tscbxZoomLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            int zoomLevel = 100;
            if ((tscbxZoomLevels.SelectedItem != null) && (int.TryParse(tscbxZoomLevels.SelectedItem.ToString(), out zoomLevel)))
            {
                documentView.PageView.Zoom = zoomLevel;
            }
        }

        private void tsbtnFitWidth_Click(object sender, EventArgs e)
        {
            documentView.PageView.ZoomMode = tsbtnFitWidth.Checked ? PdfZoomMode.FitWidth : PdfZoomMode.Custom;
        }

        private void tschkFitWidthOnPageDoubleClick_Click(object sender, EventArgs e)
        {
            documentView.PageView.FitWidthOnDoubleClick = tschkFitWidthOnPageDoubleClick.Checked;
        }
    }
}
