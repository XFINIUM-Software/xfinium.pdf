using System.IO;
using Xfinium.Pdf.FlowDocument;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Graphics.FormattedContent;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// HeadersAndFooters sample.
    /// </summary>
    public class HeadersAndFooters
    {
        private int currentPage;

        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream verdanaFontStream, Stream verdanaBoldFontStream)
        {
            HeadersAndFooters hf = new HeadersAndFooters();
            PdfFlowDocument document = hf.CreateDocument(verdanaFontStream, verdanaBoldFontStream);

            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.headersandfooters.pdf") };
            return output;
        }

        public PdfFlowDocument CreateDocument(Stream verdanaFontStream, Stream verdanaBoldFontStream)
        {
            PdfAnsiTrueTypeFont verdana = new PdfAnsiTrueTypeFont(verdanaFontStream, 10, true);
            PdfAnsiTrueTypeFont verdanaBold = new PdfAnsiTrueTypeFont(verdanaBoldFontStream, 10, true);

            PdfFlowDocumentDefaults documentDefaults = new PdfFlowDocumentDefaults();
            documentDefaults.PageDefaults.Margins.Top = 72;
            documentDefaults.PageDefaults.Margins.Bottom = 72;
            PdfFlowDocument document = new PdfFlowDocument(documentDefaults);
            document.PageCompleted += Document_PageCompleted;

            CreateHeadersAndFooters(document, verdana);

            CreateIntroPage(document, verdana, verdanaBold);

            for (int i = 0; i < 5; i++)
            {
                CreateContentPage(document, verdana, i);
            }

            return document;
        }

        private void Document_PageCompleted(object sender, PdfFlowPageEventArgs e)
        {
            currentPage++;

            PdfFlowDocument document = (PdfFlowDocument)sender;

            PdfFlowTableContent mainHeaderTable = document.HeadersFooters.MainHeader.Content[0] as PdfFlowTableContent;
            (mainHeaderTable.Rows[0].Cells[0] as PdfFlowTableStringCell).Content = string.Format("Page {0}", currentPage);
            PdfFlowTableContent evenPagesHeaderTable = document.HeadersFooters.EvenPagesHeader.Content[0] as PdfFlowTableContent;
            (evenPagesHeaderTable.Rows[0].Cells[0] as PdfFlowTableStringCell).Content = string.Format("Page {0}", currentPage);

            PdfFlowTableContent mainFooterTable = document.HeadersFooters.MainFooter.Content[0] as PdfFlowTableContent;
            (mainFooterTable.Rows[0].Cells[0] as PdfFlowTableStringCell).Content = string.Format("Page {0}", currentPage);
            PdfFlowTableContent evenPagesFooterTable = document.HeadersFooters.EvenPagesFooter.Content[0] as PdfFlowTableContent;
            (evenPagesFooterTable.Rows[0].Cells[0] as PdfFlowTableStringCell).Content = string.Format("Page {0}", currentPage);
        }

        private void CreateHeadersAndFooters(PdfFlowDocument document, PdfAnsiTrueTypeFont verdana)
        {
            PdfFlowDocumentHeader mainHeader = new PdfFlowDocumentHeader();
            mainHeader.TopMargin = 24;
            PdfFlowTableContent mainHeaderTable = new PdfFlowTableContent(1);
            mainHeaderTable.Rows.AddRowWithCells("Page");
            mainHeaderTable.Rows[0].MinHeight = 15;
            PdfFlowTableStringCell mainHeaderTableCell = mainHeaderTable.Rows[0].Cells[0] as PdfFlowTableStringCell;
            mainHeaderTableCell.Font = new PdfAnsiTrueTypeFont(verdana);
            mainHeaderTableCell.Font.Size = 8;
            mainHeaderTableCell.HorizontalAlign = PdfGraphicAlign.Far;
            mainHeaderTableCell.VerticalAlign = PdfGraphicAlign.Center;
            mainHeaderTableCell.Borders = new PdfFlowContentBorders();
            mainHeaderTableCell.Borders.Bottom = new PdfPen(PdfRgbColor.Black, 1);
            mainHeader.Content.Add(mainHeaderTable);
            document.HeadersFooters.MainHeader = mainHeader;

            PdfFlowDocumentHeader evenPagesHeader = new PdfFlowDocumentHeader();
            evenPagesHeader.TopMargin = 24;
            PdfFlowTableContent evenPagesHeaderTable = new PdfFlowTableContent(1);
            evenPagesHeaderTable.Rows.AddRowWithCells("Page");
            evenPagesHeaderTable.Rows[0].MinHeight = 15;
            PdfFlowTableStringCell evenPagesHeaderTableCell = evenPagesHeaderTable.Rows[0].Cells[0] as PdfFlowTableStringCell;
            evenPagesHeaderTableCell.Font = new PdfAnsiTrueTypeFont(verdana);
            evenPagesHeaderTableCell.Font.Size = 8;
            evenPagesHeaderTableCell.HorizontalAlign = PdfGraphicAlign.Near;
            evenPagesHeaderTableCell.VerticalAlign = PdfGraphicAlign.Center;
            evenPagesHeaderTableCell.Borders = new PdfFlowContentBorders();
            evenPagesHeaderTableCell.Borders.Bottom = new PdfPen(PdfRgbColor.Black, 1);
            evenPagesHeader.Content.Add(evenPagesHeaderTable);
            document.HeadersFooters.EvenPagesHeader = evenPagesHeader;

            document.HeadersFooters.FirstPageHeader = null;

            PdfFlowDocumentFooter mainFooter = new PdfFlowDocumentFooter();
            mainFooter.BottomMargin = 24;
            PdfFlowTableContent mainFooterTable = new PdfFlowTableContent(1);
            mainFooterTable.Rows.AddRowWithCells("Page");
            mainFooterTable.Rows[0].MinHeight = 15;
            PdfFlowTableStringCell mainFooterTableCell = mainFooterTable.Rows[0].Cells[0] as PdfFlowTableStringCell;
            mainFooterTableCell.Font = new PdfAnsiTrueTypeFont(verdana);
            mainFooterTableCell.Font.Size = 8;
            mainFooterTableCell.HorizontalAlign = PdfGraphicAlign.Far;
            mainFooterTableCell.VerticalAlign = PdfGraphicAlign.Center;
            mainFooterTableCell.Borders = new PdfFlowContentBorders();
            mainFooterTableCell.Borders.Top = new PdfPen(PdfRgbColor.Black, 1);
            mainFooter.Content.Add(mainFooterTable);
            document.HeadersFooters.MainFooter = mainFooter;

            PdfFlowDocumentFooter evenPagesFooter = new PdfFlowDocumentFooter();
            evenPagesFooter.BottomMargin = 24;
            PdfFlowTableContent evenPagesFooterTable = new PdfFlowTableContent(1);
            evenPagesFooterTable.Rows.AddRowWithCells("Page");
            evenPagesFooterTable.Rows[0].MinHeight = 15;
            PdfFlowTableStringCell evenPagesFooterTableCell = evenPagesFooterTable.Rows[0].Cells[0] as PdfFlowTableStringCell;
            evenPagesFooterTableCell.Font = new PdfAnsiTrueTypeFont(verdana);
            evenPagesFooterTableCell.Font.Size = 8;
            evenPagesFooterTableCell.HorizontalAlign = PdfGraphicAlign.Near;
            evenPagesFooterTableCell.VerticalAlign = PdfGraphicAlign.Center;
            evenPagesFooterTableCell.Borders = new PdfFlowContentBorders();
            evenPagesFooterTableCell.Borders.Top = new PdfPen(PdfRgbColor.Black, 1);
            evenPagesFooter.Content.Add(evenPagesFooterTable);
            document.HeadersFooters.EvenPagesFooter = evenPagesFooter;

            document.HeadersFooters.FirstPageFooter = null;
        }

        private void CreateIntroPage(PdfFlowDocument document, PdfAnsiTrueTypeFont verdana, PdfAnsiTrueTypeFont verdanaBold)
        {
            PdfFormattedParagraph titleParagraph = new PdfFormattedParagraph("DOCUMENT INTRO PAGE");
            titleParagraph.SpacingBefore = 300;
            (titleParagraph.Blocks[0] as PdfFormattedTextBlock).Font = new PdfAnsiTrueTypeFont(verdanaBold);
            (titleParagraph.Blocks[0] as PdfFormattedTextBlock).Font.Size = 36;
            titleParagraph.HorizontalAlign = PdfStringHorizontalAlign.Center;
            PdfFormattedParagraph subtitleParagraph = new PdfFormattedParagraph("no headers and footers");
            (subtitleParagraph.Blocks[0] as PdfFormattedTextBlock).Font = new PdfAnsiTrueTypeFont(verdana);
            (subtitleParagraph.Blocks[0] as PdfFormattedTextBlock).Font.Size = 10;
            subtitleParagraph.HorizontalAlign = PdfStringHorizontalAlign.Center;

            PdfFormattedContent fc = new PdfFormattedContent();
            fc.Paragraphs.Add(titleParagraph);
            fc.Paragraphs.Add(subtitleParagraph);

            document.AddContent(new PdfFlowTextContent(fc));
        }

        private void CreateContentPage(PdfFlowDocument document, PdfAnsiTrueTypeFont verdana, int textIndex)
        {
            string[] text = new string[] {
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras commodo elementum odio, non venenatis risus efficitur a. " +
                "Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Fusce sit amet purus eget sem tristique faucibus. " +
                "Nam ut felis vel ex ullamcorper pretium at quis nisl. Nam ac lacus tincidunt, vulputate sapien vel, tempus sem. Aliquam vel ligula dui. " +
                "Vivamus porttitor nunc vitae mi interdum, vitae ullamcorper turpis bibendum. Vestibulum sagittis lorem ante, at tincidunt arcu suscipit eu. " +
                "Morbi augue eros, tristique a consectetur ac, egestas nec turpis. Praesent non purus quis sem consequat tempor sed ac augue. Integer at mauris ac ipsum bibendum aliquam vitae id mi. " +
                "Praesent efficitur tortor in ligula mattis scelerisque. Fusce in placerat augue. Mauris pretium, dui ac accumsan aliquet, justo sem posuere purus, " +
                "sit amet tristique mi tortor malesuada lorem. Sed congue sem a neque tristique tristique et a odio. Curabitur quis aliquam turpis, tincidunt ullamcorper velit. " +
                "Phasellus posuere, justo auctor convallis luctus, mi tortor interdum lorem, ac tempor nisi lorem in erat. Maecenas dapibus tristique lacus id egestas. " +
                "Vivamus id risus vitae velit porta lacinia. Aliquam erat volutpat. Nulla facilisi. Donec tempor arcu eu rhoncus fringilla.",
                "Vivamus ullamcorper ligula sit amet interdum imperdiet. Nulla facilisi. Suspendisse et euismod elit. Quisque vitae magna nunc. " +
                "Mauris condimentum at magna blandit semper. Pellentesque in lacus odio. Sed nec molestie lacus, eget scelerisque lorem. " +
                "Etiam rutrum tellus at auctor vehicula. Mauris consequat, tortor vitae finibus efficitur, tellus arcu feugiat leo, in condimentum elit felis ut risus. " +
                "Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Fusce tincidunt libero sem, ut hendrerit turpis dapibus ut. " +
                "Donec lacinia luctus scelerisque. Sed vulputate egestas accumsan. Cras volutpat enim neque, malesuada varius lorem volutpat eget. Mauris lobortis risus ut leo malesuada, " +
                "in volutpat felis finibus. Proin a gravida velit. Ut lorem urna, sollicitudin sit amet nibh at, vestibulum pharetra mauris. Maecenas metus mi, dapibus in erat at, " +
                "congue tincidunt sapien. Vestibulum in pellentesque risus, id accumsan mauris. Donec non ex consequat, pretium ante at, suscipit lacus. Fusce ac consectetur erat. " +
                "Pellentesque maximus justo quis ante ornare condimentum. ",
                "Proin accumsan orci a nulla gravida tincidunt. Nulla et nisl eget diam rhoncus euismod. Maecenas tellus eros, semper vitae pharetra a, tincidunt ut dolor. " +
                "Nullam tempor at sapien vel efficitur. Duis vel aliquet felis, vitae tincidunt dolor. Sed tortor urna, dictum eu leo quis, feugiat eleifend ligula. Quisque vitae nisi venenatis, " +
                "pretium augue id, consequat velit. Sed dignissim justo velit, id faucibus leo scelerisque sed. In vestibulum blandit ipsum et rhoncus. Aliquam erat lorem, interdum vitae ligula at, " +
                "vulputate feugiat nunc. Fusce condimentum quis ligula ac dictum. Aliquam et viverra purus. Duis sollicitudin dolor eget diam pretium tempus. Nullam in magna eu tortor facilisis placerat non " +
                "vitae eros. Ut vitae magna dictum felis lacinia aliquam facilisis nec sem. ",
                "Suspendisse potenti. Vivamus maximus mi consequat lectus tincidunt consectetur. Proin vulputate velit lectus, eu lobortis quam lobortis congue. Sed gravida magna non " +
                "eleifend malesuada. Donec tincidunt lorem et semper dignissim. Fusce ut ex vestibulum urna lobortis aliquet in lobortis ex. Integer vehicula erat sed quam dictum varius id sed magna. " +
                "Cras maximus lacus est, ut elementum neque faucibus et. Praesent malesuada egestas scelerisque. Donec interdum ex maximus, auctor nisl non, ornare enim. " +
                "Nulla finibus quis felis non iaculis. Maecenas fringilla placerat enim non pellentesque. Proin justo orci, elementum ut porttitor in, scelerisque vel nisl. " +
                "Nam sit amet pellentesque justo, et molestie dolor. Cras ipsum justo, facilisis eget diam non, lacinia iaculis libero. Curabitur convallis, velit nec finibus mattis, " +
                "tellus erat elementum ligula, quis viverra quam sapien et enim. Morbi tempor fringilla mattis. ",
                "Aenean porttitor, augue pretium semper tincidunt, justo orci volutpat odio, malesuada convallis lacus lorem at nisi. Nulla at dolor tincidunt, tempor orci et, blandit metus. " +
                "Pellentesque malesuada augue et odio interdum, sit amet laoreet odio sagittis. Maecenas porttitor consectetur eros nec tempor. Morbi ut pharetra nunc. Phasellus non massa congue, varius tortor nec, " +
                "maximus massa. Cras erat mauris, pulvinar eu nibh ac, scelerisque maximus sem. Nam sed fringilla dolor, finibus tincidunt purus. Sed in dui ut enim interdum sagittis. Cras neque quam, " +
                "ultricies euismod dignissim sit amet, elementum eget eros. Donec in sem vel nunc vulputate pharetra.Fusce rhoncus turpis id turpis aliquet pharetra. Vivamus tristique eros lectus, eget venenatis nulla dictum et. " +
                "Vestibulum volutpat mi eu consequat blandit. Quisque ornare pellentesque tellus, in congue turpis viverra vel. Donec quis velit non nulla aliquet maximus. Maecenas ultricies nisi dui, non dapibus libero tincidunt ut. " +
                "Quisque vel interdum diam, ultricies aliquet nisi. Vivamus dui erat, tincidunt quis nibh et, aliquet ultricies erat. Nullam sit amet sodales nibh, a gravida diam. Fusce efficitur ultrices pellentesque. " +
                "Pellentesque a suscipit justo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nam lectus tellus, semper ut vehicula vitae, ullamcorper id sem. " +
                "Cras quis ipsum et ligula rutrum dignissim at sit amet leo. Etiam ut tortor in velit feugiat commodo a ut nunc. Proin nec efficitur augue. Phasellus non erat non dolor bibendum viverra in ut nunc. " +
                "Maecenas et sollicitudin nisi, in imperdiet mi. Sed luctus quam felis, a efficitur eros pellentesque ut. Integer suscipit dignissim quam sit amet feugiat. Morbi in odio quis ligula ultrices convallis. " +
                "Duis a dui tristique, pulvinar tellus vel, tristique ex. Sed metus velit, ornare sit amet felis at, finibus gravida ante. Nunc felis massa, viverra eget pellentesque ac, sollicitudin non odio. " +
                "Aliquam dictum nulla mauris, finibus venenatis dolor suscipit at. Donec sagittis consequat diam, non varius dolor ullamcorper et. Proin dictum magna eget massa posuere tincidunt. " +
                "Vivamus ut libero vel diam maximus posuere sagittis maximus mauris. Donec luctus, diam in porttitor interdum, nibh elit ultricies sem, eu porta nulla sapien id nisi. Mauris odio odio, " +
                "mattis quis enim et, laoreet consequat ligula. Praesent feugiat lacus sagittis, laoreet massa sit amet, luctus ipsum. Suspendisse id malesuada velit. Etiam sit amet risus diam. " +
                "Vestibulum non ligula vitae nunc bibendum ornare id ut ante. Morbi at orci mollis, commodo dui et, bibendum augue. Curabitur nibh arcu, vulputate eu sollicitudin et, egestas sed dui. " +
                "Mauris ante enim, cursus et scelerisque eu, vestibulum et neque. Sed gravida ultricies ante sit amet efficitur. Cras est augue, auctor vel commodo sit amet, consectetur at quam. " +
                "Phasellus vulputate convallis neque, vitae bibendum mi rutrum in. Pellentesque pharetra ultricies urna, vitae semper nulla congue non. Praesent venenatis ullamcorper risus, non luctus purus. " +
                "Integer rutrum magna id pulvinar viverra. Aliquam et scelerisque turpis. Quisque sed nisl eu orci congue tempus nec in tellus. Nam eget magna lacus. Nunc eu sapien in velit ultrices tincidunt. " +
                "Curabitur purus libero, viverra non mollis non, hendrerit sit amet tellus. Maecenas congue ut lectus et gravida. Nam commodo lacus at leo sollicitudin gravida. " +
                "Nullam felis ante, dapibus a laoreet a, sagittis id dolor. Sed eget risus id eros faucibus aliquet. Sed rhoncus nibh quam, at congue massa convallis ac. Nullam lobortis ex mauris, " +
                "nec viverra lacus fringilla sed.Pellentesque at sapien quis lectus ultrices fringilla a eu dolor. Pellentesque imperdiet ipsum a odio laoreet vehicula. Aenean vestibulum in dolor non suscipit. " +
                "Curabitur sed felis non mi malesuada mattis quis quis est. Maecenas finibus dolor et libero semper, et posuere sem interdum. Aenean posuere eleifend sapien, ac bibendum lorem egestas id. " +
                "Integer vehicula, quam in laoreet feugiat, neque elit scelerisque dui, eget facilisis dui turpis vel sapien. "
            };

            PdfFormattedContent fc = new PdfFormattedContent(text[textIndex]);
            (fc.Paragraphs[0].Blocks[0] as PdfFormattedTextBlock).Font = new PdfAnsiTrueTypeFont(verdana);
            (fc.Paragraphs[0].Blocks[0] as PdfFormattedTextBlock).Font.Size = 10;

            document.StartNewPage();
            document.AddContent(new PdfFlowTextContent(fc));
        }
    }
}