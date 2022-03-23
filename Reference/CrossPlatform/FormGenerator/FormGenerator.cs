using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Forms;
using Xfinium.Pdf.Actions;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// FormGenerator sample.
    /// </summary>
    public class FormGenerator
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run()
        {
            PdfFixedDocument document = new PdfFixedDocument();
            PdfStandardFont helvetica = new PdfStandardFont(PdfStandardFontFace.Helvetica, 12);
            PdfBrush brush = new PdfBrush();

            PdfPage page = document.Pages.Add();

            // First name
            page.Graphics.DrawString("First name:", helvetica, brush, 50, 50);
            PdfTextBoxField firstNameTextBox = new PdfTextBoxField("firstname");
            page.Fields.Add(firstNameTextBox);
            firstNameTextBox.Widgets[0].Font = helvetica;
            firstNameTextBox.Widgets[0].VisualRectangle = new PdfVisualRectangle(150, 45, 200, 20);
            firstNameTextBox.Widgets[0].BorderColor = PdfRgbColor.Black;
            firstNameTextBox.Widgets[0].BorderWidth = 1;

            // Last name
            page.Graphics.DrawString("Last name:", helvetica, brush, 50, 80);
            PdfTextBoxField lastNameTextBox = new PdfTextBoxField("lastname");
            page.Fields.Add(lastNameTextBox);
            lastNameTextBox.Widgets[0].Font = helvetica;
            lastNameTextBox.Widgets[0].VisualRectangle = new PdfVisualRectangle(150, 75, 200, 20);
            lastNameTextBox.Widgets[0].BorderColor = PdfRgbColor.Black;
            lastNameTextBox.Widgets[0].BorderWidth = 1;

            // Sex
            page.Graphics.DrawString("Sex:", helvetica, brush, 50, 110);
            PdfRadioButtonField sexRadioButton = new PdfRadioButtonField("sex");
            PdfRadioButtonWidget maleRadioItem = new PdfRadioButtonWidget();
            sexRadioButton.Widgets.Add(maleRadioItem);
            PdfRadioButtonWidget femaleRadioItem = new PdfRadioButtonWidget();
            sexRadioButton.Widgets.Add(femaleRadioItem);
            page.Fields.Add(sexRadioButton);

            page.Graphics.DrawString("Male", helvetica, brush, 180, 110);
            maleRadioItem.ExportValue = "M";
            maleRadioItem.CheckStyle = PdfCheckStyle.Circle;
            maleRadioItem.VisualRectangle = new PdfVisualRectangle(150, 105, 20, 20);
            maleRadioItem.BorderColor = PdfRgbColor.Black;
            maleRadioItem.BorderWidth = 1;

            page.Graphics.DrawString("Female", helvetica, brush, 280, 110);
            femaleRadioItem.ExportValue = "F";
            femaleRadioItem.CheckStyle = PdfCheckStyle.Circle;
            femaleRadioItem.VisualRectangle = new PdfVisualRectangle(250, 105, 20, 20);
            femaleRadioItem.BorderColor = PdfRgbColor.Black;
            femaleRadioItem.BorderWidth = 1;

            // First car
            page.Graphics.DrawString("First car:", helvetica, brush, 50, 140);
            PdfComboBoxField firstCarList = new PdfComboBoxField("firstcar");
            firstCarList.Items.Add(new PdfListItem("Mercedes", "Mercedes"));
            firstCarList.Items.Add(new PdfListItem("BMW", "BMW"));
            firstCarList.Items.Add(new PdfListItem("Audi", "Audi"));
            firstCarList.Items.Add(new PdfListItem("Volkswagen", "Volkswagen"));
            firstCarList.Items.Add(new PdfListItem("Porsche", "Porsche"));
            firstCarList.Items.Add(new PdfListItem("Honda", "Honda"));
            firstCarList.Items.Add(new PdfListItem("Toyota", "Toyota"));
            firstCarList.Items.Add(new PdfListItem("Lexus", "Lexus"));
            firstCarList.Items.Add(new PdfListItem("Infiniti", "Infiniti"));
            firstCarList.Items.Add(new PdfListItem("Acura", "Acura"));
            page.Fields.Add(firstCarList);
            firstCarList.Widgets[0].Font = helvetica;
            firstCarList.Widgets[0].VisualRectangle = new PdfVisualRectangle(150, 135, 200, 20);
            firstCarList.Widgets[0].BorderColor = PdfRgbColor.Black;
            firstCarList.Widgets[0].BorderWidth = 1;

            // Second car
            page.Graphics.DrawString("Second car:", helvetica, brush, 50, 170);
            PdfListBoxField secondCarList = new PdfListBoxField("secondcar");
            secondCarList.Items.Add(new PdfListItem("Mercedes", "Mercedes"));
            secondCarList.Items.Add(new PdfListItem("BMW", "BMW"));
            secondCarList.Items.Add(new PdfListItem("Audi", "Audi"));
            secondCarList.Items.Add(new PdfListItem("Volkswagen", "Volkswagen"));
            secondCarList.Items.Add(new PdfListItem("Porsche", "Porsche"));
            secondCarList.Items.Add(new PdfListItem("Honda", "Honda"));
            secondCarList.Items.Add(new PdfListItem("Toyota", "Toyota"));
            secondCarList.Items.Add(new PdfListItem("Lexus", "Lexus"));
            secondCarList.Items.Add(new PdfListItem("Infiniti", "Infiniti"));
            secondCarList.Items.Add(new PdfListItem("Acura", "Acura"));
            page.Fields.Add(secondCarList);
            secondCarList.Widgets[0].Font = helvetica;
            secondCarList.Widgets[0].VisualRectangle = new PdfVisualRectangle(150, 165, 200, 60);
            secondCarList.Widgets[0].BorderColor = PdfRgbColor.Black;
            secondCarList.Widgets[0].BorderWidth = 1;

            // I agree
            page.Graphics.DrawString("I agree:", helvetica, brush, 50, 240);
            PdfCheckBoxField agreeCheckBox = new PdfCheckBoxField("agree");
            page.Fields.Add(agreeCheckBox);
            agreeCheckBox.Widgets[0].Font = helvetica;
            (agreeCheckBox.Widgets[0] as PdfCheckWidget).ExportValue = "YES";
            (agreeCheckBox.Widgets[0] as PdfCheckWidget).CheckStyle = PdfCheckStyle.Check;
            agreeCheckBox.Widgets[0].VisualRectangle = new PdfVisualRectangle(150, 235, 20, 20);
            agreeCheckBox.Widgets[0].BorderColor = PdfRgbColor.Black;
            agreeCheckBox.Widgets[0].BorderWidth = 1;

            // Sign here
            page.Graphics.DrawString("Sign here:", helvetica, brush, 50, 270);
            PdfSignatureField signHereField = new PdfSignatureField("signhere");
            page.Fields.Add(signHereField);
            signHereField.Widgets[0].Font = helvetica;
            signHereField.Widgets[0].VisualRectangle = new PdfVisualRectangle(150, 265, 200, 60);

            // Submit form
            PdfPushButtonField submitBtn = new PdfPushButtonField("submit");
            page.Fields.Add(submitBtn);
            submitBtn.Widgets[0].VisualRectangle = new PdfVisualRectangle(450, 45, 150, 30);
            (submitBtn.Widgets[0] as PdfPushButtonWidget).Caption = "Submit form";
            submitBtn.Widgets[0].BackgroundColor = PdfRgbColor.LightGray;
            PdfSubmitFormAction submitFormAction = new PdfSubmitFormAction();
            submitFormAction.DataFormat = PdfSubmitDataFormat.FDF;
            submitFormAction.Fields.Add("firstname");
            submitFormAction.Fields.Add("lastname");
            submitFormAction.Fields.Add("sex");
            submitFormAction.Fields.Add("firstcar");
            submitFormAction.Fields.Add("secondcar");
            submitFormAction.Fields.Add("agree");
            submitFormAction.Fields.Add("signhere");
            submitFormAction.SubmitFields = true;
            submitFormAction.Url = "http://www.xfiniumsoft.com/";
            submitBtn.Widgets[0].MouseUp = submitFormAction;

            // Reset form
            PdfPushButtonField resetBtn = new PdfPushButtonField("reset");
            page.Fields.Add(resetBtn);
            resetBtn.Widgets[0].VisualRectangle = new PdfVisualRectangle(450, 85, 150, 30);
            (resetBtn.Widgets[0] as PdfPushButtonWidget).Caption = "Reset form";
            resetBtn.Widgets[0].BackgroundColor = PdfRgbColor.LightGray;
            PdfResetFormAction resetFormAction = new PdfResetFormAction();
            resetBtn.Widgets[0].MouseUp = resetFormAction;

            // Print form
            PdfPushButtonField printBtn = new PdfPushButtonField("print");
            page.Fields.Add(printBtn);
            printBtn.Widgets[0].VisualRectangle = new PdfVisualRectangle(450, 125, 150, 30);
            (printBtn.Widgets[0] as PdfPushButtonWidget).Caption = "Print form";
            printBtn.Widgets[0].BackgroundColor = PdfRgbColor.LightGray;
            PdfJavaScriptAction printAction = new PdfJavaScriptAction();
            printAction.Script = "this.print(true);\n";
            printBtn.Widgets[0].MouseUp = printAction;

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.formgenerator.pdf") };
            return output;
        }
    }
}