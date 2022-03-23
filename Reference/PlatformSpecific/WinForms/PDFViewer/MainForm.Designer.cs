namespace PDFViewer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.documentView = new Xfinium.Pdf.View.PdfDocumentView();
            this.appToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscbxZoomLevels = new System.Windows.Forms.ToolStripComboBox();
            this.tsbtnOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnFitWidth = new System.Windows.Forms.ToolStripButton();
            this.tschkFitWidthOnPageDoubleClick = new System.Windows.Forms.ToolStripButton();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.appToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ofd
            // 
            this.ofd.DefaultExt = "pdf";
            this.ofd.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            // 
            // documentView
            // 
            this.documentView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.documentView.Document = null;
            this.documentView.GraphicRendererFactory = null;
            this.documentView.Location = new System.Drawing.Point(0, 28);
            this.documentView.Name = "documentView";
            this.documentView.PageNumber = 0;
            this.documentView.Size = new System.Drawing.Size(884, 487);
            this.documentView.TabIndex = 6;
            this.documentView.Text = "pdfDocumentView1";
            this.documentView.Zoom = 100D;
            // 
            // appToolStrip
            // 
            this.appToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnOpen,
            this.tsbtnSave,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tscbxZoomLevels,
            this.tsbtnFitWidth,
            this.tschkFitWidthOnPageDoubleClick});
            this.appToolStrip.Location = new System.Drawing.Point(0, 0);
            this.appToolStrip.Name = "appToolStrip";
            this.appToolStrip.Size = new System.Drawing.Size(884, 25);
            this.appToolStrip.TabIndex = 7;
            this.appToolStrip.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(42, 22);
            this.toolStripLabel1.Text = "Zoom:";
            // 
            // tscbxZoomLevels
            // 
            this.tscbxZoomLevels.Items.AddRange(new object[] {
            "25",
            "50",
            "75",
            "100",
            "125",
            "150",
            "175",
            "200",
            "250",
            "300"});
            this.tscbxZoomLevels.Name = "tscbxZoomLevels";
            this.tscbxZoomLevels.Size = new System.Drawing.Size(121, 25);
            this.tscbxZoomLevels.SelectedIndexChanged += new System.EventHandler(this.tscbxZoomLevels_SelectedIndexChanged);
            // 
            // tsbtnOpen
            // 
            this.tsbtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnOpen.Image = global::PDFViewer.Properties.Resources.OpenFolder_16x_24;
            this.tsbtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnOpen.Name = "tsbtnOpen";
            this.tsbtnOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbtnOpen.Text = "Open PDF file";
            this.tsbtnOpen.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tsbtnSave
            // 
            this.tsbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSave.Image = global::PDFViewer.Properties.Resources.Save_16x_24;
            this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSave.Name = "tsbtnSave";
            this.tsbtnSave.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSave.Text = "Save PDF file";
            this.tsbtnSave.Click += new System.EventHandler(this.tsbtnSave_Click);
            // 
            // tsbtnFitWidth
            // 
            this.tsbtnFitWidth.CheckOnClick = true;
            this.tsbtnFitWidth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnFitWidth.Image = global::PDFViewer.Properties.Resources.FitWidth_16x;
            this.tsbtnFitWidth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnFitWidth.Name = "tsbtnFitWidth";
            this.tsbtnFitWidth.Size = new System.Drawing.Size(23, 22);
            this.tsbtnFitWidth.Text = "Fit width";
            this.tsbtnFitWidth.ToolTipText = "Sets the ZoomMode on the viewer to FitWidth";
            this.tsbtnFitWidth.Click += new System.EventHandler(this.tsbtnFitWidth_Click);
            // 
            // tschkFitWidthOnPageDoubleClick
            // 
            this.tschkFitWidthOnPageDoubleClick.CheckOnClick = true;
            this.tschkFitWidthOnPageDoubleClick.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tschkFitWidthOnPageDoubleClick.Image = global::PDFViewer.Properties.Resources.FitWidthOnDoubleClick_16x;
            this.tschkFitWidthOnPageDoubleClick.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tschkFitWidthOnPageDoubleClick.Name = "tschkFitWidthOnPageDoubleClick";
            this.tschkFitWidthOnPageDoubleClick.Size = new System.Drawing.Size(23, 22);
            this.tschkFitWidthOnPageDoubleClick.Text = "Fit Width on Page Double Click";
            this.tschkFitWidthOnPageDoubleClick.ToolTipText = "Fit Width on Page Double Click\r\nThe double clicked page is scrolled to top and Zo" +
    "omMode is set to FitWidth";
            this.tschkFitWidthOnPageDoubleClick.Click += new System.EventHandler(this.tschkFitWidthOnPageDoubleClick_Click);
            // 
            // sfd
            // 
            this.sfd.DefaultExt = "pdf";
            this.sfd.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            this.sfd.Title = "Save PDF file";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 515);
            this.Controls.Add(this.appToolStrip);
            this.Controls.Add(this.documentView);
            this.Name = "MainForm";
            this.Text = "XFINIUM.PDF Samples - PDF Viewer";
            this.appToolStrip.ResumeLayout(false);
            this.appToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog ofd;
        private Xfinium.Pdf.View.PdfDocumentView documentView;
        private System.Windows.Forms.ToolStrip appToolStrip;
        private System.Windows.Forms.ToolStripButton tsbtnOpen;
        private System.Windows.Forms.ToolStripButton tsbtnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tscbxZoomLevels;
        private System.Windows.Forms.ToolStripButton tsbtnFitWidth;
        private System.Windows.Forms.ToolStripButton tschkFitWidthOnPageDoubleClick;
        private System.Windows.Forms.SaveFileDialog sfd;
    }
}

