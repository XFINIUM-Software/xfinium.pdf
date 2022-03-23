using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Forms;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// FormFill sample.
    /// </summary>
    public class FormFill
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        /// <param name="stream"></param>
        public static SampleOutputInfo[] Run(Stream stream)
        {
            PdfFixedDocument document = new PdfFixedDocument(stream);
            (document.Form.Fields["firstname"] as PdfTextBoxField).Text = "John";
            (document.Form.Fields["lastname"] as PdfTextBoxField).Value = "Doe";

            (document.Form.Fields["sex"].Widgets[0] as PdfRadioButtonWidget).Checked = true;

            (document.Form.Fields["firstcar"] as PdfComboBoxField).SelectedIndex = 0;

            (document.Form.Fields["secondcar"] as PdfListBoxField).SelectedIndex = 1;

            (document.Form.Fields["agree"] as PdfCheckBoxField).Checked = true;
            document.Form.FlattenFields();

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.formfill.pdf") };
            return output;
        }
    }
}