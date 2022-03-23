using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// Barcodes sample.
    /// </summary>
    public class Barcodes
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run()
        {
            PdfFixedDocument document = new PdfFixedDocument();
            PdfStandardFont titleFont = new PdfStandardFont(PdfStandardFontFace.HelveticaBold, 16);
            PdfStandardFont barcodeFont = new PdfStandardFont(PdfStandardFontFace.Helvetica, 12);

            PdfPage page = document.Pages.Add();
            DrawGenericBarcodes(page, titleFont, barcodeFont);

            page = document.Pages.Add();
            DrawPharmaBarcodes(page, titleFont, barcodeFont);

            page = document.Pages.Add();
            DrawEanUpcBarcodes(page, titleFont, barcodeFont);

            page = document.Pages.Add();
            DrawPostAndTransportantionBarcodes(page, titleFont, barcodeFont);

            page = document.Pages.Add();
            Draw2DBarcodes(page, titleFont, barcodeFont);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.barcodes.pdf") };
            return output;
        }

        private static void DrawGenericBarcodes(PdfPage page, PdfFont titleFont, PdfFont barcodeFont)
        {
            
            PdfBrush brush = new PdfBrush();
            PdfPen lightGrayPen = new PdfPen(PdfRgbColor.LightGray, 0.5);

            page.Graphics.DrawString("Generic barcodes", titleFont, brush, 40, 20);
            for (int i = 0; i < 7; i++)
            {
                page.Graphics.DrawLine(lightGrayPen, 40, 50 + 100 * i, 570, 50 + 100 * i);
            }
            page.Graphics.DrawLine(lightGrayPen, 306, 50, 306, 750);

            string[] barcodes = new string[] { "Codabar", "Code 11", "Code 25", "Code 25 Interleaved", "Code 39", "Code 39 Extended",
                "Code 93", "Code 93 Extended", "Code 128 A", "Code 128 B", "Code 128 C", "COOP 25", "Matrix 25", "MSI/Plessey" };
            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Brush = brush;
            sao.Font = barcodeFont;
            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.HorizontalAlign = PdfStringHorizontalAlign.Center;
            slo.VerticalAlign = PdfStringVerticalAlign.Top;

            slo.X = 173;
            int sign = 1;
            for (int i = 0; i < barcodes.Length; i++)
            {
                slo.Y = 55 + 100 * (i / 2);

                page.Graphics.DrawString(barcodes[i], sao, slo);

                slo.X = slo.X + sign * 266;
                sign = -sign;
            }

            // Codabar
            PdfCodabarBarcode codabarBarcode = new PdfCodabarBarcode();
            codabarBarcode.Data = "523408943724";
            codabarBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(codabarBarcode, 173 - codabarBarcode.Width / 2, 70);

            // Code 11
            PdfCode11Barcode code11Barcode = new PdfCode11Barcode();
            code11Barcode.Data = "42376524534";
            code11Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(code11Barcode, 173 + 266 - code11Barcode.Width / 2, 70);

            // Code 25
            PdfCode25Barcode code25Barcode = new PdfCode25Barcode();
            code25Barcode.Data = "857621354312";
            code25Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(code25Barcode, 173 - code25Barcode.Width / 2, 170);

            // Code 25 Interleaved
            PdfCode25InterleavedBarcode code25InterleavedBarcode = new PdfCode25InterleavedBarcode();
            code25InterleavedBarcode.Data = "42376524534";
            code25InterleavedBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(code25InterleavedBarcode, 173 + 266 - code25InterleavedBarcode.Width / 2, 170);

            // Code 39
            PdfCode39Barcode code39Barcode = new PdfCode39Barcode();
            code39Barcode.Data = "6430784327";
            code39Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(code39Barcode, 173 - code39Barcode.Width / 2, 270);

            // Code 39 Extended
            PdfCode39ExtendedBarcode code39ExtendedBarcode = new PdfCode39ExtendedBarcode();
            code39ExtendedBarcode.Data = "8990436322";
            code39ExtendedBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(code39ExtendedBarcode, 173 + 266 - code39ExtendedBarcode.Width / 2, 270);

            // Code 93
            PdfCode93Barcode code93Barcode = new PdfCode93Barcode();
            code93Barcode.Data = "6345212344";
            code93Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(code93Barcode, 173 - code93Barcode.Width / 2, 370);

            // Code 39 Extended
            PdfCode93ExtendedBarcode code93ExtendedBarcode = new PdfCode93ExtendedBarcode();
            code93ExtendedBarcode.Data = "125436732";
            code93ExtendedBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(code93ExtendedBarcode, 173 + 266 - code93ExtendedBarcode.Width / 2, 370);

            // Code 128 A
            PdfCode128ABarcode code128ABarcode = new PdfCode128ABarcode();
            code128ABarcode.Data = "XFINIUM.PDF";
            code128ABarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(code128ABarcode, 173 - code128ABarcode.Width / 2, 470);

            // Code 128 B
            PdfCode128BBarcode code128BBarcode = new PdfCode128BBarcode();
            code128BBarcode.Data = "xfinium.pdf";
            code128BBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(code128BBarcode, 173 + 266 - code128BBarcode.Width / 2, 470);

            // Code 128 C
            PdfCode128CBarcode code128CBarcode = new PdfCode128CBarcode();
            code128CBarcode.Data = "423409865432";
            code128CBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(code128CBarcode, 173 - code128CBarcode.Width / 2, 570);

            // COOP 25
            PdfCoop25Barcode coop25Barcode = new PdfCoop25Barcode();
            coop25Barcode.Data = "43256565543";
            coop25Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(coop25Barcode, 173 + 266 - coop25Barcode.Width / 2, 570);

            // Matrix 25
            PdfMatrix25Barcode matrix25Barcode = new PdfMatrix25Barcode();
            matrix25Barcode.Data = "500540024300";
            matrix25Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(matrix25Barcode, 173 - matrix25Barcode.Width / 2, 670);

            // MSI/Plessey
            PdfMsiPlesseyBarcode msiPlesseyBarcode = new PdfMsiPlesseyBarcode();
            msiPlesseyBarcode.Data = "1124332556";
            msiPlesseyBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(msiPlesseyBarcode, 173 + 266 - msiPlesseyBarcode.Width / 2, 670);

            page.Graphics.CompressAndClose();
        }

        private static void DrawPharmaBarcodes(PdfPage page, PdfFont titleFont, PdfFont barcodeFont)
        {
            PdfBrush brush = new PdfBrush();
            PdfPen lightGrayPen = new PdfPen(PdfRgbColor.LightGray, 0.5);

            page.Graphics.DrawString("Pharma barcodes (barcodes used in the pharmaceutical industry)", titleFont, brush, 40, 20);
            for (int i = 0; i < 2; i++)
            {
                page.Graphics.DrawLine(lightGrayPen, 40, 50 + 100 * i, 570, 50 + 100 * i);
            }
            page.Graphics.DrawLine(lightGrayPen, 306, 50, 306, 250);

            string[] barcodes = new string[] { "Code 32", "Pharmacode", "PZN (Pharma-Zentral-Nummer)" };
            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Brush = brush;
            sao.Font = barcodeFont;
            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.HorizontalAlign = PdfStringHorizontalAlign.Center;
            slo.VerticalAlign = PdfStringVerticalAlign.Top;

            slo.X = 173;
            int sign = 1;
            for (int i = 0; i < barcodes.Length; i++)
            {
                slo.Y = 55 + 100 * (i / 2);

                page.Graphics.DrawString(barcodes[i], sao, slo);

                slo.X = slo.X + sign * 266;
                sign = -sign;
            }

            // Code 32
            PdfCode32Barcode code32Barcode = new PdfCode32Barcode();
            code32Barcode.Data = "54925174";
            code32Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(code32Barcode, 173 - code32Barcode.Width / 2, 70);

            // Pharmacode
            PdfPharmacodeBarcode pharmacodeBarcode = new PdfPharmacodeBarcode();
            pharmacodeBarcode.Data = "128128";
            pharmacodeBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(pharmacodeBarcode, 173 + 266 - pharmacodeBarcode.Width / 2, 70);

            // PZN 
            PdfPznBarcode pznBarcode = new PdfPznBarcode();
            pznBarcode.Data = "903271";
            pznBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(pznBarcode, 173 - pznBarcode.Width / 2, 170);

            page.Graphics.CompressAndClose();
        }

        private static void DrawEanUpcBarcodes(PdfPage page, PdfFont titleFont, PdfFont barcodeFont)
        {

            PdfBrush brush = new PdfBrush();
            PdfPen lightGrayPen = new PdfPen(PdfRgbColor.LightGray, 0.5);

            page.Graphics.DrawString("EAN/UPC barcodes", titleFont, brush, 40, 20);
            for (int i = 0; i < 7; i++)
            {
                page.Graphics.DrawLine(lightGrayPen, 40, 50 + 100 * i, 570, 50 + 100 * i);
            }
            page.Graphics.DrawLine(lightGrayPen, 306, 50, 306, 750);

            string[] barcodes = new string[] { "EAN 128", "EAN-13", "EAN-8", "ISBN", "ISMN", "ISSN", "JAN-13", "UPC-A", "UPC-E" };
            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Brush = brush;
            sao.Font = barcodeFont;
            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.HorizontalAlign = PdfStringHorizontalAlign.Center;
            slo.VerticalAlign = PdfStringVerticalAlign.Top;

            slo.X = 173;
            int sign = 1;
            for (int i = 0; i < barcodes.Length; i++)
            {
                slo.Y = 55 + 100 * (i / 2);

                page.Graphics.DrawString(barcodes[i], sao, slo);

                slo.X = slo.X + sign * 266;
                sign = -sign;
            }

            // EAN 128
            PdfEan128Barcode ean128Barcode = new PdfEan128Barcode();
            ean128Barcode.Data = "WWW.XFINIUMSOFT.COM";
            ean128Barcode.QuietZones.Left = 0;
            ean128Barcode.QuietZones.Right = 0;
            ean128Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            ean128Barcode.ApplicationIdentifier = "URL";
            page.Graphics.DrawBarcode(ean128Barcode, 173 - ean128Barcode.Width / 2, 70);

            // EAN-13
            PdfEan13Barcode ean13Barcode = new PdfEan13Barcode();
            ean13Barcode.Data = "437612735617";
            ean13Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(ean13Barcode, 173 + 266 - ean13Barcode.Width / 2, 70);

            // EAN-8
            PdfEan8Barcode ean8Barcode = new PdfEan8Barcode();
            ean8Barcode.Data = "5423731";
            ean8Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(ean8Barcode, 173 - ean8Barcode.Width / 2, 170);

            // ISBN
            PdfIsbnBarcode isbnBarcode = new PdfIsbnBarcode();
            isbnBarcode.Data = "436314378";
            isbnBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(isbnBarcode, 173 + 266 - isbnBarcode.Width / 2, 170);

            // ISMN
            PdfIsmnBarcode ismnBarcode = new PdfIsmnBarcode();
            ismnBarcode.Data = "437612489";
            ismnBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(ismnBarcode, 173 - ismnBarcode.Width / 2, 270);

            // ISSN
            PdfIssnBarcode issnBarcode = new PdfIssnBarcode();
            issnBarcode.Data = "546712341";
            issnBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(issnBarcode, 173 + 266 - issnBarcode.Width / 2, 270);

            // JAN-13
            PdfJan13Barcode jan13Barcode = new PdfJan13Barcode();
            jan13Barcode.Data = "1256127634";
            jan13Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(jan13Barcode, 173 - jan13Barcode.Width / 2, 370);

            // UPC-A
            PdfUpcaBarcode upcaBarcode = new PdfUpcaBarcode();
            upcaBarcode.Data = "12543267841";
            upcaBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(upcaBarcode, 173 + 266 - upcaBarcode.Width / 2, 370);

            // UPC-E
            PdfUpceBarcode upceBarcode = new PdfUpceBarcode();
            upceBarcode.Data = "1234532";
            upceBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(upceBarcode, 173 - upceBarcode.Width / 2, 470);

            page.Graphics.CompressAndClose();
        }

        private static void DrawPostAndTransportantionBarcodes(PdfPage page, PdfFont titleFont, PdfFont barcodeFont)
        {

            PdfBrush brush = new PdfBrush();
            PdfPen lightGrayPen = new PdfPen(PdfRgbColor.LightGray, 0.5);

            page.Graphics.DrawString("Post and transportation barcodes", titleFont, brush, 40, 20);
            for (int i = 0; i < 7; i++)
            {
                page.Graphics.DrawLine(lightGrayPen, 40, 50 + 100 * i, 570, 50 + 100 * i);
            }
            page.Graphics.DrawLine(lightGrayPen, 306, 50, 306, 750);

            string[] barcodes = new string[] { "FedEx Ground 96", "IATA 25", "Identcode", "Leitcode", "KIX", "Planet",
                "PostNet", "RM4SCC", "SCC-14", "SingaporePost", "SSCC-18", "USPS FIM", "USPS Horizontal", "USPS PIC" };
            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Brush = brush;
            sao.Font = barcodeFont;
            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.HorizontalAlign = PdfStringHorizontalAlign.Center;
            slo.VerticalAlign = PdfStringVerticalAlign.Top;

            slo.X = 173;
            int sign = 1;
            for (int i = 0; i < barcodes.Length; i++)
            {
                slo.Y = 55 + 100 * (i / 2);

                page.Graphics.DrawString(barcodes[i], sao, slo);

                slo.X = slo.X + sign * 266;
                sign = -sign;
            }

            // FedEx Ground 96
            PdfFedExGround96Barcode fedexGround96Barcode = new PdfFedExGround96Barcode();
            fedexGround96Barcode.Data = "962343237687543423123";
            fedexGround96Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(fedexGround96Barcode, 173 - fedexGround96Barcode.Width / 2, 70);

            // IATA 25
            PdfIata25Barcode iata25Barcode = new PdfIata25Barcode();
            iata25Barcode.Data = "54366436563";
            iata25Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(iata25Barcode, 173 + 266 - iata25Barcode.Width / 2, 70);

            // Identcode
            PdfIdentcodeBarcode identcodeBarcode = new PdfIdentcodeBarcode();
            identcodeBarcode.Data = "12435678214";
            identcodeBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(identcodeBarcode, 173 - identcodeBarcode.Width / 2, 170);

            // Leitcode
            PdfLeitcodeBarcode leitcodeBarcode = new PdfLeitcodeBarcode();
            leitcodeBarcode.Data = "1243657687321";
            leitcodeBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(leitcodeBarcode, 173 + 266 - leitcodeBarcode.Width / 2, 170);

            // KIX
            PdfKixBarcode kixBarcode = new PdfKixBarcode();
            kixBarcode.Data = "XFINIUMPDF";
            kixBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(kixBarcode, 173 - kixBarcode.Width / 2, 270);

            // Planet
            PdfPlanetBarcode planetBarcode = new PdfPlanetBarcode();
            planetBarcode.Data = "645316643300";
            planetBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(planetBarcode, 173 + 266 - planetBarcode.Width / 2, 270);

            // PostNet
            PdfPostNetBarcode postNetBarcode = new PdfPostNetBarcode();
            postNetBarcode.Data = "04231454322";
            postNetBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(postNetBarcode, 173 - postNetBarcode.Width / 2, 370);

            // RM4SCC
            PdfRm4sccBarcode rm4sccBarcode = new PdfRm4sccBarcode();
            rm4sccBarcode.Data = "XFINIUMPDF";
            rm4sccBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(rm4sccBarcode, 173 + 266 - rm4sccBarcode.Width / 2, 370);

            // SCC-14
            PdfScc14Barcode scc14Barcode = new PdfScc14Barcode();
            scc14Barcode.Data = "3255091205412";
            scc14Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(scc14Barcode, 173 - scc14Barcode.Width / 2, 470);

            // Singapore Post
            PdfSingaporePostBarcode singaporePostBarcode = new PdfSingaporePostBarcode();
            singaporePostBarcode.Data = "XFINIUMPDF";
            singaporePostBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(singaporePostBarcode, 173 + 266 - singaporePostBarcode.Width / 2, 470);

            // SSCC-18
            PdfSscc18Barcode sscc18Barcode = new PdfSscc18Barcode();
            sscc18Barcode.Data = "09876543219832435";
            sscc18Barcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(sscc18Barcode, 173 - sscc18Barcode.Width / 2, 570);

            // USPS FIM
            PdfUspsFimBarcode uspsFimBarcode = new PdfUspsFimBarcode();
            uspsFimBarcode.Data = "A";
            uspsFimBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(uspsFimBarcode, 173 + 266 - uspsFimBarcode.Width / 2, 570);

            // USPS Horizontal
            PdfUspsHorizontalBarcode uspsHorizontalBarcode = new PdfUspsHorizontalBarcode();
            uspsHorizontalBarcode.Data = "1111";
            uspsHorizontalBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.None;
            page.Graphics.DrawBarcode(uspsHorizontalBarcode, 173 - uspsHorizontalBarcode.Width / 2, 670);

            // USPS PIC
            PdfUspsPicBarcode uspsPicBarcode = new PdfUspsPicBarcode();
            uspsPicBarcode.Data = "914354657901234354019";
            uspsPicBarcode.BarcodeTextPosition = PdfBarcodeTextPosition.Bottom;
            page.Graphics.DrawBarcode(uspsPicBarcode, 173 + 266 - uspsPicBarcode.Width / 2, 670);

            page.Graphics.CompressAndClose();
        }

        private static void Draw2DBarcodes(PdfPage page, PdfFont titleFont, PdfFont barcodeFont)
        {
            PdfBrush brush = new PdfBrush();
            PdfPen lightGrayPen = new PdfPen(PdfRgbColor.LightGray, 0.5);

            page.Graphics.DrawString("2D barcodes", titleFont, brush, 40, 20);
            for (int i = 0; i < 3; i++)
            {
                page.Graphics.DrawLine(lightGrayPen, 40, 50 + 150 * i, 570, 50 + 150 * i);
            }
            page.Graphics.DrawLine(lightGrayPen, 306, 50, 306, 500);

            string[] barcodes = new string[] { "Codablock F", "Code 16K", "PDF417", "Micro PDF417", "DataMatrix", "QR" };
            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Brush = brush;
            sao.Font = barcodeFont;
            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.HorizontalAlign = PdfStringHorizontalAlign.Center;
            slo.VerticalAlign = PdfStringVerticalAlign.Top;

            slo.X = 173;
            int sign = 1;
            for (int i = 0; i < barcodes.Length; i++)
            {
                slo.Y = 55 + 150 * (i / 2);

                page.Graphics.DrawString(barcodes[i], sao, slo);

                slo.X = slo.X + sign * 266;
                sign = -sign;
            }

            // Codablock F
            PdfCodablockFBarcode codablockFBarcode = new PdfCodablockFBarcode();
            codablockFBarcode.Data = "*** Xfinium.Pdf ***";
            codablockFBarcode.Columns = 10;
            codablockFBarcode.Rows = 5;
            page.Graphics.DrawBarcode(codablockFBarcode, 173 - codablockFBarcode.Width / 2, 70);

            // Code 16K
            PdfCode16KBarcode code16KBarcode = new PdfCode16KBarcode();
            code16KBarcode.Data = "*** Xfinium.Pdf ***";
            code16KBarcode.Rows = 6;
            page.Graphics.DrawBarcode(code16KBarcode, 173 + 266 - code16KBarcode.Width / 2, 70);

            // PDF 417
            Pdf417RegularBarcode pdf417Barcode = new Pdf417RegularBarcode();
            pdf417Barcode.Data = "*** Xfinium.Pdf ***";
            pdf417Barcode.Columns = 10;
            pdf417Barcode.Rows = 0;
            page.Graphics.DrawBarcode(pdf417Barcode, 173 - pdf417Barcode.Width / 2, 220);

            // MicroPDF 417
            Pdf417MicroBarcode microPdf417Barcode = new Pdf417MicroBarcode();
            microPdf417Barcode.Data = "* Xfinium.Pdf *";
            microPdf417Barcode.BarcodeSize = Pdf417MicroBarcodeSize.Rows6Columns4;
            page.Graphics.DrawBarcode(microPdf417Barcode, 173 + 266 - microPdf417Barcode.Width / 2, 220);

            // DataMatrix
            PdfDataMatrixBarcode datamatrixBarcode = new PdfDataMatrixBarcode();
            datamatrixBarcode.Data = "*** Xfinium.Pdf ***";
            datamatrixBarcode.XDimension = 2;
            datamatrixBarcode.BarcodeSize = DataMatrixBarcodeSize.Auto;
            page.Graphics.DrawBarcode(datamatrixBarcode, 173 - datamatrixBarcode.Width / 2, 370);

            // QR Barcode
            PdfQrBarcode qrBarcode = new PdfQrBarcode();
            qrBarcode.XDimension = 2;
            qrBarcode.Data = "Xfinium.Pdf: http://www.xfiniumpdf.com/";
            qrBarcode.CharacterSet = PdfQrBarcodeCharacterSet.ISO88591;
            page.Graphics.DrawBarcode(qrBarcode, 173 + 266 - qrBarcode.Width / 2, 370);

            page.Graphics.CompressAndClose();
        }
           
    }
}