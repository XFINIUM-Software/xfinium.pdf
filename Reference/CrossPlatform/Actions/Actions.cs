using System;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Annotations;
using Xfinium.Pdf.Actions;
using Xfinium.Pdf.Destinations;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// Actions sample.
    /// </summary>
    public class Actions
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run()
        {
            // Create a PDF document with 10 pages.
            PdfFixedDocument document = new PdfFixedDocument();
            PdfStandardFont helvetica = new PdfStandardFont(PdfStandardFontFace.Helvetica, 216);
            PdfBrush blackBrush = new PdfBrush();
            for (int i = 0; i < 10; i++)
            {
                PdfPage page = document.Pages.Add();
                page.Graphics.DrawString((i + 1).ToString(), helvetica, blackBrush, 5, 5);
            }

            CreateNamedActions(document, helvetica);

            CreateGoToActions(document, helvetica);

            CreateRemoteGoToActions(document, helvetica);

            CreateLaunchActions(document, helvetica);

            CreateUriActions(document, helvetica);

            CreateJavaScriptActions(document, helvetica);

            CreateDocumentActions(document);

            // Compress the page graphic content.
            for (int i = 0; i < document.Pages.Count; i++)
            {
                document.Pages[i].Graphics.CompressAndClose();
            }

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.actions.pdf") };
            return output;
        }

        private static void CreateNamedActions(PdfFixedDocument document, PdfFont font)
        {
            PdfPen blackPen = new PdfPen(new PdfRgbColor(0, 0, 0), 1);
            PdfBrush blackBrush = new PdfBrush();

            font.Size = 12;
            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Brush = blackBrush;
            sao.Font = font;
            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.HorizontalAlign = PdfStringHorizontalAlign.Center;
            slo.VerticalAlign = PdfStringVerticalAlign.Middle;

            for (int i = 0; i < document.Pages.Count; i++)
            {
                document.Pages[i].Graphics.DrawString("Named actions:", font, blackBrush, 400, 20);

                /////////////
                // First page
                /////////////
                document.Pages[i].Graphics.DrawRectangle(blackPen, 400, 40, 200, 20);
                slo.X = 500;
                slo.Y = 50;
                document.Pages[i].Graphics.DrawString("Go To First Page", sao, slo);

                // Create a link annotation on top of the widget.
                PdfLinkAnnotation link = new PdfLinkAnnotation();
                document.Pages[i].Annotations.Add(link);
                link.VisualRectangle = new PdfVisualRectangle(400, 40, 200, 20);

                // Create a named action and attach it to the link.
                PdfNamedAction namedAction = new PdfNamedAction();
                namedAction.NamedAction = PdfActionName.FirstPage;
                link.Action = namedAction;

                /////////////
                // Prev page
                /////////////
                document.Pages[i].Graphics.DrawRectangle(blackPen, 400, 80, 200, 20);
                slo.Y = 90;
                document.Pages[i].Graphics.DrawString("Go To Previous Page", sao, slo);

                // Create a link annotation on top of the widget.
                link = new PdfLinkAnnotation();
                document.Pages[i].Annotations.Add(link);
                link.VisualRectangle = new PdfVisualRectangle(400, 80, 200, 20);

                // Create a named action and attach it to the link.
                namedAction = new PdfNamedAction();
                namedAction.NamedAction = PdfActionName.PrevPage;
                link.Action = namedAction;

                /////////////
                // Next page
                /////////////
                document.Pages[i].Graphics.DrawRectangle(blackPen, 400, 120, 200, 20);
                slo.Y = 130;
                document.Pages[i].Graphics.DrawString("Go To Next Page", sao, slo);

                // Create a link annotation on top of the widget.
                link = new PdfLinkAnnotation();
                document.Pages[i].Annotations.Add(link);
                link.VisualRectangle = new PdfVisualRectangle(400, 120, 200, 20);

                // Create a named action and attach it to the link.
                namedAction = new PdfNamedAction();
                namedAction.NamedAction = PdfActionName.NextPage;
                link.Action = namedAction;

                /////////////
                // Last page
                /////////////
                document.Pages[i].Graphics.DrawRectangle(blackPen, 400, 160, 200, 20);
                slo.Y = 170;
                document.Pages[i].Graphics.DrawString("Go To Last Page", sao, slo);

                // Create a link annotation on top of the widget.
                link = new PdfLinkAnnotation();
                document.Pages[i].Annotations.Add(link);
                link.VisualRectangle = new PdfVisualRectangle(400, 160, 200, 20);

                // Create a named action and attach it to the link.
                namedAction = new PdfNamedAction();
                namedAction.NamedAction = PdfActionName.LastPage;
                link.Action = namedAction;

                /////////////
                // Print document
                /////////////
                document.Pages[i].Graphics.DrawRectangle(blackPen, 400, 200, 200, 20);
                slo.Y = 210;
                document.Pages[i].Graphics.DrawString("Print Document", sao, slo);

                // Create a link annotation on top of the widget.
                link = new PdfLinkAnnotation();
                document.Pages[i].Annotations.Add(link);
                link.VisualRectangle = new PdfVisualRectangle(400, 200, 200, 20);

                // Create a named action and attach it to the link.
                namedAction = new PdfNamedAction();
                namedAction.NamedAction = PdfActionName.Print;
                link.Action = namedAction;
            }
        }

        private static void CreateGoToActions(PdfFixedDocument document, PdfFont font)
        {
            PdfPen blackPen = new PdfPen(new PdfRgbColor(0, 0, 0), 1);
            PdfBrush blackBrush = new PdfBrush();

            font.Size = 12;
            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Brush = blackBrush;
            sao.Font = font;
            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.HorizontalAlign = PdfStringHorizontalAlign.Center;
            slo.VerticalAlign = PdfStringVerticalAlign.Middle;

            Random rnd = new Random();
            for (int i = 0; i < document.Pages.Count; i++)
            {
                int destinationPage = rnd.Next(document.Pages.Count);

                document.Pages[i].Graphics.DrawString("Go To actions:", font, blackBrush, 400, 240);

                document.Pages[i].Graphics.DrawRectangle(blackPen, 400, 260, 200, 20);
                slo.X = 500;
                slo.Y = 270;
                document.Pages[i].Graphics.DrawString("Go To page: " + (destinationPage + 1).ToString(), sao, slo);

                // Create a link annotation on top of the widget.
                PdfLinkAnnotation link = new PdfLinkAnnotation();
                document.Pages[i].Annotations.Add(link);
                link.VisualRectangle = new PdfVisualRectangle(400, 260, 200, 20);

                // Create a GoTo action and attach it to the link.
                PdfPageDirectDestination pageDestination = new PdfPageDirectDestination();
                pageDestination.Page = document.Pages[destinationPage];
                pageDestination.Left = 0;
                pageDestination.Top = 0;
                pageDestination.Zoom = 0; // Keep current zoom
                PdfGoToAction gotoPageAction = new PdfGoToAction();
                gotoPageAction.Destination = pageDestination;
                link.Action = gotoPageAction;
            }
        }

        private static void CreateRemoteGoToActions(PdfFixedDocument document, PdfFont font)
        {
            PdfPen blackPen = new PdfPen(new PdfRgbColor(0, 0, 0), 1);
            PdfBrush blackBrush = new PdfBrush();

            font.Size = 12;
            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Brush = blackBrush;
            sao.Font = font;
            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.HorizontalAlign = PdfStringHorizontalAlign.Center;
            slo.VerticalAlign = PdfStringVerticalAlign.Middle;

            Random rnd = new Random();
            for (int i = 0; i < document.Pages.Count; i++)
            {
                int destinationPage = rnd.Next(document.Pages.Count);

                document.Pages[i].Graphics.DrawString("Go To Remote actions:", font, blackBrush, 400, 300);

                document.Pages[i].Graphics.DrawRectangle(blackPen, 400, 320, 200, 20);
                slo.X = 500;
                slo.Y = 330;
                document.Pages[i].Graphics.DrawString("Go To page " + (destinationPage + 1).ToString() + " in sample.pdf", sao, slo);

                // Create a link annotation on top of the widget.
                PdfLinkAnnotation link = new PdfLinkAnnotation();
                document.Pages[i].Annotations.Add(link);
                link.VisualRectangle = new PdfVisualRectangle(400, 320, 200, 20);

                // Create a GoToR action and attach it to the link.
                PdfPageNumberDestination pageDestination = new PdfPageNumberDestination();
                pageDestination.PageNumber = destinationPage;
                pageDestination.Left = 0;
                pageDestination.Top = 792;
                pageDestination.Zoom = 0; // Keep current zoom
                PdfRemoteGoToAction remoteGoToAction = new PdfRemoteGoToAction();
                remoteGoToAction.FileName = "sample.pdf";
                remoteGoToAction.Destination = pageDestination;
                link.Action = remoteGoToAction;
            }
        }

        private static void CreateLaunchActions(PdfFixedDocument document, PdfFont font)
        {
            PdfPen blackPen = new PdfPen(new PdfRgbColor(0, 0, 0), 1);
            PdfBrush blackBrush = new PdfBrush();

            font.Size = 12;
            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Brush = blackBrush;
            sao.Font = font;
            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.HorizontalAlign = PdfStringHorizontalAlign.Center;
            slo.VerticalAlign = PdfStringVerticalAlign.Middle;

            for (int i = 0; i < document.Pages.Count; i++)
            {
                document.Pages[i].Graphics.DrawString("Launch actions:", font, blackBrush, 400, 360);

                document.Pages[i].Graphics.DrawRectangle(blackPen, 400, 380, 200, 20);
                slo.X = 500;
                slo.Y = 390;
                document.Pages[i].Graphics.DrawString("Launch samples explorer", sao, slo);

                // Create a link annotation on top of the widget.
                PdfLinkAnnotation link = new PdfLinkAnnotation();
                document.Pages[i].Annotations.Add(link);
                link.VisualRectangle = new PdfVisualRectangle(400, 380, 200, 20);

                // Create a launch action and attach it to the link.
                PdfLaunchAction launchAction = new PdfLaunchAction();
                launchAction.FileName = "Xfinium.Pdf.SamplesExplorer.Win.exe";
                link.Action = launchAction;
            }
        }

        private static void CreateUriActions(PdfFixedDocument document, PdfFont font)
        {
            PdfPen blackPen = new PdfPen(new PdfRgbColor(0, 0, 0), 1);
            PdfBrush blackBrush = new PdfBrush();

            font.Size = 12;
            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Brush = blackBrush;
            sao.Font = font;
            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.HorizontalAlign = PdfStringHorizontalAlign.Center;
            slo.VerticalAlign = PdfStringVerticalAlign.Middle;

            for (int i = 0; i < document.Pages.Count; i++)
            {
                document.Pages[i].Graphics.DrawString("Uri actions:", font, blackBrush, 400, 420);

                document.Pages[i].Graphics.DrawRectangle(blackPen, 400, 440, 200, 20);
                slo.X = 500;
                slo.Y = 450;
                document.Pages[i].Graphics.DrawString("Go to xfiniumpdf.com", sao, slo);

                // Create a link annotation on top of the widget.
                PdfLinkAnnotation link = new PdfLinkAnnotation();
                document.Pages[i].Annotations.Add(link);
                link.VisualRectangle = new PdfVisualRectangle(400, 440, 200, 20);

                // Create an uri action and attach it to the link.
                PdfUriAction uriAction = new PdfUriAction();
                uriAction.URI = "http://www.xfiniumpdf.com";
                link.Action = uriAction;
            }
        }

        private static void CreateJavaScriptActions(PdfFixedDocument document, PdfFont font)
        {
            PdfPen blackPen = new PdfPen(new PdfRgbColor(0, 0, 0), 1);
            PdfBrush blackBrush = new PdfBrush();

            font.Size = 12;
            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Brush = blackBrush;
            sao.Font = font;
            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.HorizontalAlign = PdfStringHorizontalAlign.Center;
            slo.VerticalAlign = PdfStringVerticalAlign.Middle;

            for (int i = 0; i < document.Pages.Count; i++)
            {
                document.Pages[i].Graphics.DrawString("JavaScript actions:", font, blackBrush, 400, 480);

                document.Pages[i].Graphics.DrawRectangle(blackPen, 400, 500, 200, 20);
                slo.X = 500;
                slo.Y = 510;
                document.Pages[i].Graphics.DrawString("Click me", sao, slo);

                // Create a link annotation on top of the widget.
                PdfLinkAnnotation link = new PdfLinkAnnotation();
                document.Pages[i].Annotations.Add(link);
                link.VisualRectangle = new PdfVisualRectangle(400, 500, 200, 20);

                // Create a Javascript action and attach it to the link.
                PdfJavaScriptAction jsAction = new PdfJavaScriptAction();
                jsAction.Script = "app.alert({cMsg: \"JavaScript action: you are now on page " + (i + 1) + "\", cTitle: \"Xfinium.Pdf Actions Sample\"});";
                link.Action = jsAction;
            }
        }

        private static void CreateDocumentActions(PdfFixedDocument document)
        {
            // Create an action that will open the document at the last page with 200% zoom.
            PdfPageDirectDestination pageDestination = new PdfPageDirectDestination();
            pageDestination.Page = document.Pages[document.Pages.Count - 1];
            pageDestination.Zoom = 200;
            pageDestination.Top = 0;
            pageDestination.Left = 0;
            PdfGoToAction openAction = new PdfGoToAction();
            openAction.Destination = pageDestination;
            document.OpenAction = openAction;

            // Create an action that is executed when the document is closed.
            PdfJavaScriptAction jsCloseAction = new PdfJavaScriptAction();
            jsCloseAction.Script = "app.alert({cMsg: \"The document will close now.\", cTitle: \"Xfinium.Pdf Actions Sample\"});";
            document.BeforeCloseAction = jsCloseAction;

            // Create an action that is executed before the document is printed.
            PdfJavaScriptAction jsBeforePrintAction = new PdfJavaScriptAction();
            jsBeforePrintAction.Script = "app.alert({cMsg: \"The document will be printed.\", cTitle: \"Xfinium.Pdf Actions Sample\"});";
            document.BeforePrintAction = jsBeforePrintAction;

            // Create an action that is executed after the document is printed.
            PdfJavaScriptAction jsAfterPrintAction = new PdfJavaScriptAction();
            jsAfterPrintAction.Script = "app.alert({cMsg: \"The document has been printed.\", cTitle: \"Xfinium.Pdf Actions Sample\"});";
            document.AfterPrintAction = jsAfterPrintAction;
        }
    }
}