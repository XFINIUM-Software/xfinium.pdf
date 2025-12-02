using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Xfinium.Pdf;
using Xfinium.Pdf.View;
using Xfinium.Pdf.View.Content;
using Xfinium.Pdf.View.Layouts;

namespace PDFViewer
{
    public partial class AppForm : Form
    {
        public AppForm()
        {
            InitializeComponent();

            tsbComment.Tag = tsAnnotations;
            tsAnnotations.Visible = false;
            tsbForms.Tag = tsForms;
            tsForms.Visible = false;
            tsbMarkupText.Tag = tsTextMarkup;
            tsTextMarkup.Visible = false;
            tsSearch.Visible = false;
            searchInitialized = false;
            tscbxSearchRange.SelectedIndex = 0;
            selectedFieldCount = 0;
            pdfView.UserInteractionMode = PdfUserInteractionMode.PanAndScan;
            contentLocator = new PdfVisualContentLocator(pdfView);

            InitDefaultApearances();
        }

        private bool searchInitialized;

        private PdfVisualContentLocator contentLocator;

        private ToolStripButton activeMultiTool;

        /// <summary>
        /// Number of selected form fields in the designer.
        /// </summary>
        private int selectedFieldCount;

        private void AppForm_Load(object sender, EventArgs e)
        {
            pdfDocument.Load(new PdfFixedDocument("..\\..\\..\\..\\..\\..\\..\\SupportFiles\\XFINIUM.PDF.View.pdf"));
            tsslFileName.Text = ofd.FileName;

            EnableTools(true);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // When the form is closing, detach the document from the viewers and dispose all the viewers and documents.
            pdfView.Document = null;
            pdfView.Dispose();
            pdfDocument.Dispose();
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            Point pt = this.tsMain.PointToScreen(tsbOpen.Bounds.Location);
            pt.Y += tsbOpen.Bounds.Height;
            cmsOpen.Show(pt, ToolStripDropDownDirection.BelowRight);
        }

        private void tsmpOpenIncremental_Click(object sender, EventArgs e)
        {
            // In incremental mode the viewer loads only the required parts for display.
            // Incremental load mode is achieved by loading the source file directly in the viewer.
            // The source PDF file is kept open and any changes on the PDF file must be saved in the source file as they are written in incremental update mode.
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pdfDocument.Load(ofd.FileName);
                tsslFileName.Text = ofd.FileName;

                EnableTools(true);
            }
        }

        private void tsmOpenFull_Click(object sender, EventArgs e)
        {
            // In full mode the viewer loads the whole file in memory.
            // Full load mode is achieved by loading the source file in a PdfFixedDocument and then loading the fixed document in the viewer.
            // The source PDF file can be moved/deleted and any changes on the PDF file can be saved to any file as the PDF file is written in full.
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pdfDocument.Load(new PdfFixedDocument(ofd.FileName));
                tsslFileName.Text = ofd.FileName;

                EnableTools(true);
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pdfDocument.Save(sfd.FileName);
                tsslFileName.Text = sfd.FileName;
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            pdfDocument.Close();
            pdfView.UserInteractionMode = PdfUserInteractionMode.PanAndScan;
            EnableTools(false);

            tsslFileName.Text = "No file loaded";
        }


        private void tscbxZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((tscbxZoom.SelectedItem == null ) || 
                !int.TryParse(tscbxZoom.SelectedItem.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int zoom))
            {
                zoom = 100;
            }

            pdfView.Zoom = zoom;
        }

        private void tsbFitWidth_Click(object sender, EventArgs e)
        {
            pdfView.ZoomMode = tsbFitWidth.Checked ? PdfZoomMode.FitWidth : PdfZoomMode.Custom;
        }

        private void tscbxZoom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!int.TryParse(tscbxZoom.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out int zoom))
                {
                    tscbxZoom.Text = "100";
                    zoom = 100;
                }
                pdfView.Zoom = zoom;
            }
        }

        private void tsbLayoutSingleColumn_Click(object sender, EventArgs e)
        {
            if (!tsbLayoutSingleColumn.Checked)
            {
                tsbLayoutSingleColumn.Checked = true;
                tsbLayoutSingleRow.Checked = false;

                pdfView.PageDisplayLayout = new PdfColumnBasedPageDisplayLayout();
            }
        }

        private void tsbLayoutSingleRow_Click(object sender, EventArgs e)
        {
            if (!tsbLayoutSingleRow.Checked)
            {
                tsbLayoutSingleRow.Checked = true;
                tsbLayoutSingleColumn.Checked = false;

                pdfView.PageDisplayLayout = new PdfRowBasedPageDisplayLayout();
            }
        }

        private void pdfView_ZoomChanged(object sender, EventArgs e)
        {
            tscbxZoom.Text = pdfView.Zoom.ToString(CultureInfo.InvariantCulture);
        }

        private void pdfView_ZoomModeChanged(object sender, EventArgs e)
        {
            tsbFitWidth.Checked = pdfView.ZoomMode == PdfZoomMode.FitWidth;
        }

        private void tsbPan_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.PanAndScan;
            UpdateActiveMultiTool(null);
        }

        private void tsbSelectContent_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.SelectContent;
            UpdateActiveMultiTool(null);
        }

        private void tsbComment_Click(object sender, EventArgs e)
        {
            ToggleMultiTool(tsbComment, PdfUserInteractionMode.EditAnnotations);
        }

        private void tsbForms_Click(object sender, EventArgs e)
        {
            ToggleMultiTool(tsbForms, PdfUserInteractionMode.EditFormFields);
            if (tsbForms.Checked)
            {
                selectedFieldCount = 0;
                UpdateFormDesigner(selectedFieldCount);
            }
        }

        private void tsbMarkupText_Click(object sender, EventArgs e)
        {
            ToggleMultiTool(tsbMarkupText, PdfUserInteractionMode.HighlightText);
        }

        private void tsbAnnotationsEdit_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.EditAnnotations;
        }

        private void tsbAnnotationsText_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddTextAnnotation;
        }

        private void tsbAnnotationsRubberStamp_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddRubberStampAnnotation;
        }

        private void tsbAnnotationsCircle_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddCircleAnnotation;
        }

        private void tsbAnnotationsSquare_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddSquareAnnotation;
        }

        private void tsbAnnotationsCloudSquare_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddCloudSquareAnnotation;
        }

        private void tsbAnnotationsLine_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddLineAnnotation;
        }

        private void tsbAnnotationsPolyline_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddPolylineAnnotation;
        }

        private void tsbAnnotationsPolygon_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddPolygonAnnotation;
        }

        private void tsbAnnotationsCloudPolygon_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddCloudPolygonAnnotation;
        }

        private void tsbAnnotationsInk_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddInkAnnotation;
        }

        private void tsbAnnotationsLink_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddLinkAnnotation;
        }

        private void tsbAnnotationsFileAttachment_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddFileAttachmentAnnotation;
        }

        private void tsbAnnotationsFreeText_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddFreeTextAnnotation;
        }

        private void tsbFormsEdit_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.EditFormFields;
        }

        private void tsbFormsTextBox_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddTextBoxField;
        }

        private void tsbFormsCheckBox_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddCheckBoxField;
        }

        private void tsbFormsRadiobutton_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddRadioButtonField;
        }

        private void tsbFormsComboBox_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddComboBoxField;
        }

        private void tsbFormsListBox_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddListBoxField;
        }

        private void tsbFormsPushbutton_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddPushButtonField;
        }

        private void tsbFormsSignature_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.AddSignatureField;
        }

        private void tsbFormDesignerAlignLeft_Click(object sender, EventArgs e)
        {
            pdfView.FormDesigner.AlignLeft();
        }

        private void tsbFormDesignerAlignCenter_Click(object sender, EventArgs e)
        {
            pdfView.FormDesigner.AlignCenter();
        }

        private void tsbFormDesignerAlignRight_Click(object sender, EventArgs e)
        {
            pdfView.FormDesigner.AlignRight();
        }

        private void tsbFormDesignerAlignTop_Click(object sender, EventArgs e)
        {
            pdfView.FormDesigner.AlignTop();
        }

        private void tsbFormDesignerAlignMiddle_Click(object sender, EventArgs e)
        {
            pdfView.FormDesigner.AlignMiddle();
        }

        private void tsbFormDesignerAlignBottom_Click(object sender, EventArgs e)
        {
            pdfView.FormDesigner.AlignBottom();
        }

        private void tsbFormDesignerCenterHorizontally_Click(object sender, EventArgs e)
        {
            pdfView.FormDesigner.CenterHorizontally();
        }

        private void tsbFormDesignerCenterVertically_Click(object sender, EventArgs e)
        {
            pdfView.FormDesigner.CenterVertically();
        }

        private void tsbFormDesignerCenterBoth_Click(object sender, EventArgs e)
        {
            pdfView.FormDesigner.CenterHorizontallyAndVertically();
        }

        private void tsbFormDesignerMakeSameWidth_Click(object sender, EventArgs e)
        {
            pdfView.FormDesigner.MatchWidth();
        }

        private void tsbFormDesignerMakeSameHeight_Click(object sender, EventArgs e)
        {
            pdfView.FormDesigner.MatchHeight();
        }

        private void tsbFormDesignerMakeSameSize_Click(object sender, EventArgs e)
        {
            pdfView.FormDesigner.MatchWidthAndHeight();
        }

        private void tsbFormDesignerDistributeHorizontally_Click(object sender, EventArgs e)
        {
            pdfView.FormDesigner.SpaceEquallyOnHorizontal();
        }

        private void tsbFormDesignerDistributeVertically_Click(object sender, EventArgs e)
        {
            pdfView.FormDesigner.SpaceEquallyOnVertical();
        }

        private void tsbMarkupHighlightText_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.HighlightText;
        }

        private void tsbMarkupUnderlineText_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.FlatUnderlineText;
        }

        private void tsbMarkupStrikeoutText_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.StrikeoutText;
        }

        private void tsbMarkupSquigglyText_Click(object sender, EventArgs e)
        {
            pdfView.UserInteractionMode = PdfUserInteractionMode.SquigglyUnderlineText;
        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            tsSearch.Visible = tsbSearch.Checked;
        }

        private void tsbMatchCase_Click(object sender, EventArgs e)
        {
            if (tsbMatchCase.Checked)
            {
                tsbMatchRegEx.Checked = false;
            }
        }

        private void tsbMatchAccent_Click(object sender, EventArgs e)
        {
            if (tsbMatchAccent.Checked)
            {
                tsbMatchRegEx.Checked = false;
            }
        }

        private void tsbMatchWholeWord_Click(object sender, EventArgs e)
        {
            if (tsbMatchWholeWord.Checked)
            {
                tsbMatchRegEx.Checked = false;
            }
        }

        private void tsbMatchRegEx_Click(object sender, EventArgs e)
        {
            if (tsbMatchRegEx.Checked)
            {
                tsbMatchCase.Checked = false;
                tsbMatchAccent.Checked = false;
                tsbMatchWholeWord.Checked = false;
            }
        }

        private void tstbxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            searchInitialized = false;
        }

        private void tscbxSearchRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchInitialized = false;
        }

        private void tsbFindPrevious_Click(object sender, EventArgs e)
        {
            if (!searchInitialized)
            {
                InitSearch();
            }
            else
            {
                contentLocator.HighlightPreviousTextSearchResult();
            }
        }

        private void tsbFindNext_Click(object sender, EventArgs e)
        {
            if (!searchInitialized)
            {
                InitSearch();
            }
            else
            {
                contentLocator.HighlightNextTextSearchResult();
            }
        }

        private void pdfView_UserInteractionModeChanged(object sender, EventArgs e)
        {
            tsbPan.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.PanAndScan;
            tsbSelectContent.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.SelectContent;

            tsbAnnotationsEdit.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.EditAnnotations;
            tsbAnnotationsText.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddTextAnnotation;
            tsbAnnotationsRubberStamp.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddRubberStampAnnotation;
            tsbAnnotationsCircle.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddCircleAnnotation;
            tsbAnnotationsSquare.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddSquareAnnotation;
            tsbAnnotationsCloudSquare.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddCloudSquareAnnotation;
            tsbAnnotationsLine.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddLineAnnotation;
            tsbAnnotationsPolyline.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddPolylineAnnotation;
            tsbAnnotationsPolygon.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddPolygonAnnotation;
            tsbAnnotationsCloudPolygon.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddCloudPolygonAnnotation;
            tsbAnnotationsInk.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddInkAnnotation;
            tsbAnnotationsLink.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddLinkAnnotation;
            tsbAnnotationsFileAttachment.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddFileAttachmentAnnotation;
            tsbAnnotationsFreeText.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddFreeTextAnnotation;

            tsbFormsEdit.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.EditFormFields;
            tsbFormsTextBox.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddTextBoxField;
            tsbFormsCheckBox.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddCheckBoxField;
            tsbFormsRadiobutton.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddRadioButtonField;
            tsbFormsComboBox.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddComboBoxField;
            tsbFormsListBox.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddListBoxField;
            tsbFormsPushbutton.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddPushButtonField;
            tsbFormsSignature.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.AddSignatureField;

            tsbMarkupHighlightText.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.HighlightText;
            tsbMarkupUnderlineText.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.FlatUnderlineText;
            tsbMarkupStrikeoutText.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.StrikeoutText;
            tsbMarkupSquigglyText.Checked = pdfView.UserInteractionMode == PdfUserInteractionMode.SquigglyUnderlineText;

            selectedFieldCount = 0;
            UpdateFormDesigner(selectedFieldCount);
        }

        private void pdfView_AnnotationSelected(object sender, PdfVisualAnnotationEventArgs e)
        {
            if (pdfView.UserInteractionMode == PdfUserInteractionMode.EditFormFields)
            {
                selectedFieldCount++;
                UpdateFormDesigner(selectedFieldCount);
            }
        }

        private void pdfView_AnnotationDeselected(object sender, PdfVisualAnnotationEventArgs e)
        {
            if (pdfView.UserInteractionMode == PdfUserInteractionMode.EditFormFields)
            {
                selectedFieldCount--;
                UpdateFormDesigner(selectedFieldCount);
            }
        }

        private void pdfView_BeforeAnnotationDelete(object sender, PdfVisualAnnotationDeleteEventArgs e)
        {
            e.AllowDelete = MessageBox.Show("Are you sure you want to delete the selected annotation?", "XFINIUM.PDF Viewer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void pdfView_BeforePageDelete(object sender, PdfVisualPageDeleteEventArgs e)
        {
            e.AllowDelete = MessageBox.Show("Are you sure you want to delete the current page?", "XFINIUM.PDF Viewer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void EnableTools(bool enable)
        {
            tsbSave.Enabled = enable;
            tsbClose.Enabled = enable;
            tsbComment.Enabled = enable;
            tsbForms.Enabled = enable;
            tsbMarkupText.Enabled = enable;
            tsbSearch.Enabled = enable;
        }

        private void UpdateActiveMultiTool(ToolStripButton multiTool)
        {
            if (activeMultiTool != null)
            {
                activeMultiTool.Checked = false;
                ToolStrip toolStrip = activeMultiTool.Tag as ToolStrip;
                toolStrip.Visible = false;
            }

            activeMultiTool = multiTool;
            if (activeMultiTool != null)
            {
                ToolStrip toolStrip = activeMultiTool.Tag as ToolStrip;
                toolStrip.Visible = true;
            }
        }

        private void ToggleMultiTool (ToolStripButton multiTool, PdfUserInteractionMode userInteractionMode)
        {
            if (multiTool.Checked)
            {
                UpdateActiveMultiTool(multiTool);
                pdfView.UserInteractionMode = userInteractionMode;
            }
            else
            {
                UpdateActiveMultiTool(null);
                pdfView.UserInteractionMode = PdfUserInteractionMode.PanAndScan;
            }
        }

        private void UpdateFormDesigner(int selectedFieldCount)
        {
            tsbFormDesignerAlignLeft.Enabled = selectedFieldCount > 1;
            tsbFormDesignerAlignCenter.Enabled = selectedFieldCount > 1;
            tsbFormDesignerAlignRight.Enabled = selectedFieldCount > 1;
            tsbFormDesignerAlignTop.Enabled = selectedFieldCount > 1;
            tsbFormDesignerAlignMiddle.Enabled = selectedFieldCount > 1;
            tsbFormDesignerAlignBottom.Enabled = selectedFieldCount > 1;
            tsbFormDesignerCenterVertically.Enabled = selectedFieldCount > 0;
            tsbFormDesignerCenterHorizontally.Enabled = selectedFieldCount > 0;
            tsbFormDesignerCenterBoth.Enabled = selectedFieldCount > 0;
            tsbFormDesignerMakeSameWidth.Enabled = selectedFieldCount > 1;
            tsbFormDesignerMakeSameHeight.Enabled = selectedFieldCount > 1;
            tsbFormDesignerMakeSameSize.Enabled = selectedFieldCount > 1;
            tsbFormDesignerDistributeHorizontally.Enabled = selectedFieldCount > 2;
            tsbFormDesignerDistributeVertically.Enabled = selectedFieldCount > 2;
        }

        private void InitSearch()
        {
            PdfVisualTextSearchOptions searchOptions = tsbMatchCase.Checked ? PdfVisualTextSearchOptions.CaseSensitiveSearch : PdfVisualTextSearchOptions.CaseInsensitiveSearch;
            searchOptions |= tsbMatchAccent.Checked ? PdfVisualTextSearchOptions.AccentSensitiveSearch : PdfVisualTextSearchOptions.AccentInsensitiveSearch;
            if (tsbMatchWholeWord.Checked)
            {
                searchOptions |= PdfVisualTextSearchOptions.WholeWordSearch;
            }
            if (tsbMatchRegEx.Checked)
            {
                searchOptions = PdfVisualTextSearchOptions.RegExSearch;
            }

            PdfVisualSearchRange searchRange = PdfVisualSearchRange.CurrentPage;
            switch (tscbxSearchRange.SelectedIndex)
            {
                case 1:
                    searchRange = PdfVisualSearchRange.VisiblePages;
                    break;
                case 2:
                    searchRange = PdfVisualSearchRange.AllPages;
                    break;
            }

            contentLocator.SearchText(tstbxSearch.Text, searchRange, searchOptions, true);

            searchInitialized = true;
        }

        private void InitDefaultApearances()
        {
            pdfView.TextSelectionAppearance = new PathVisualAppearance(null, new SolidBrush(Color.FromArgb(128, Color.BlueViolet)));
            pdfView.AnnotationSelectionRectangleAppearance = new PathVisualAppearance(new Pen(Color.Blue, 1), new SolidBrush(Color.FromArgb(64, Color.LightBlue)));

            pdfView.DefaultTextAnnotationAppearance = new PathVisualAppearance(new Pen(Color.Black, 1), null);
            pdfView.DefaultFileAttachmentAnnotationAppearance = new PathVisualAppearance(new Pen(Color.Black, 1), null);
            pdfView.DefaultSquareAnnotationAppearance = new PathVisualAppearance(new Pen(Color.Red, 1), null);
            pdfView.DefaultCircleAnnotationAppearance = new PathVisualAppearance(new Pen(Color.Red, 1), null);
            pdfView.DefaultCloudSquareAnnotationAppearance = new PathVisualAppearance(new Pen(Color.Red, 1), null);
            pdfView.DefaultLinkAnnotationAppearance = new PathVisualAppearance(new Pen(Color.Black, 1), null);
            pdfView.DefaultRubberStampAnnotationAppearance = new PathVisualAppearance(new Pen(Color.Red, 1), null);
            pdfView.DefaultLineAnnotationAppearance = new PathVisualAppearance(new Pen(Color.Red, 2), null);
            pdfView.DefaultInkAnnotationAppearance = new PathVisualAppearance(new Pen(Color.Red, 2), null);
            pdfView.DefaultPolylineAnnotationAppearance = new PathVisualAppearance(new Pen(Color.Red, 2), null);
            pdfView.DefaultHighlightAnnotationAppearance = new PathVisualAppearance(null, new SolidBrush(Color.FromArgb(64, Color.Yellow)));
            pdfView.DefaultUnderlineAnnotationAppearance = new PathVisualAppearance(new Pen(Color.Red, 1), null);
            pdfView.DefaultSquigglyAnnotationAppearance = new PathVisualAppearance(new Pen(Color.Red, 1), null);
            pdfView.DefaultStrikeoutAnnotationAppearance = new PathVisualAppearance(new Pen(Color.Red, 1), null);
            pdfView.DefaultFreeTextAnnotationAppearance = new PathVisualAppearance(new Pen(Color.Black, 1), null);
            pdfView.DefaultFormFieldAppearance = new PathVisualAppearance(new Pen(Color.Black, 1), null);
        }
    }
}
