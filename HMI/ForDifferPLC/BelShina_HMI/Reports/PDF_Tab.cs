﻿using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Shapes;
using System.Diagnostics;
using System.Globalization;
using System.Data;
using BelShina_HMI.ViewModels;

namespace BelShina_HMI.Reports
{
    class PDF_Tab
    {
        /// <summary>
        /// The MigraDoc document that represents the invoice.
        /// </summary>
        Document _document;

        /// <summary>
        /// The root navigator for the XML document.
        /// </summary>
        readonly XPathNavigator _navigator;

        /// <summary>
        /// The text frame of the MigraDoc document that contains the address.
        /// </summary>
        TextFrame _addressFrame;

        /// <summary>
        /// The table of the MigraDoc document that contains the invoice items.
        /// </summary>
        Table _table;
        DataTable dataTable;

        /// <summary>
        /// Initializes a new instance of the class InvoiceForm and opens the specified XML document.
        /// </summary>
        TestType testType;
        public PDF_Tab(string filename, DataTable dataTable, TestType testType)
        {
            this.dataTable = dataTable;
            this.testType = testType;
            //dataTable.Columns.Add("Parametr");
            //dataTable.Columns.Add("Value");
            //dataTable.Columns.Add("Unit");
            var invoice = new XmlDocument();
            // An XML invoice based on a sample created with Microsoft InfoPath.
            invoice.Load(filename);
            _navigator = invoice.CreateNavigator();
        }

        /// <summary>
        /// Creates the invoice document.
        /// </summary>
        public Document CreateDocument()
        {
            // Create a new MigraDoc document.
            _document = new Document();
            _document.Info.Title = "A sample invoice";
            _document.Info.Subject = "Demonstrates how to create an invoice.";
            _document.Info.Author = "Stefan Lange";

            DefineStyles();

            CreatePage();

            FillContent();

            return _document;
        }

        /// <summary>
        /// Defines the styles used to format the MigraDoc document.
        /// </summary>
        void DefineStyles()
        {
            // Get the predefined style Normal.
            var style = _document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Segoe UI";

            style = _document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = _document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Create a new style called Table based on style Normal.
            style = _document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Segoe UI Semilight";
            style.Font.Size = 9;

            // Create a new style called Title based on style Normal.
            style = _document.Styles.AddStyle("Title", "Normal");
            style.Font.Name = "Segoe UI Semibold";
            style.Font.Size = 9;

            // Create a new style called Reference based on style Normal.
            style = _document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
        }

        /// <summary>
        /// Creates the static parts of the invoice.
        /// </summary>
        void CreatePage()
        {
            // Each MigraDoc document needs at least one section.
            var section = _document.AddSection();

            // Define the page setup. We use an image in the header, therefore the
            // default top margin is too small for our invoice.
            section.PageSetup = _document.DefaultPageSetup.Clone();
            // We increase the TopMargin to prevent the document body from overlapping the page header.
            // We have an image of 3.5 cm height in the header.
            // The default position for the header is 1.25 cm.
            // We add 0.5 cm spacing between header image and body and get 5.25 cm.
            // Default value is 2.5 cm.
            section.PageSetup.TopMargin = "5.25cm";

            // Put the logo in the header.
            var image = section.Headers.Primary.AddImage(@"D:\Chart\Belshina.png"/*"../../../../images/Belshina.png"*/);
            image.Height = "0.5cm";
            image.Width = "4.2cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Right;
            image.WrapFormat.Style = WrapStyle.Through;

            // Create the footer.
            var paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText("Протокол испытаний");
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // Create the text frame for the address.
            _addressFrame = section.AddTextFrame();
            _addressFrame.Height = "3.0cm";
            _addressFrame.Width = "15.0cm";
            _addressFrame.Left = ShapePosition.Center;
            _addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            _addressFrame.Top = "5.0cm";
            _addressFrame.RelativeVertical = RelativeVertical.Page;

            // Show the sender in the address frame.
            paragraph = _addressFrame.AddParagraph("Определение жесткостных характеристик грузовых ЦМК шин.");
            paragraph.Format.Font.Size = 13;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.SpaceAfter = 3;

            // Add the print date field.
            paragraph = section.AddParagraph();
            // We use an empty paragraph to move the first text line below the address field.
            paragraph.Format.LineSpacing = "5.25cm";
            paragraph.Format.LineSpacingRule = LineSpacingRule.Exactly;
            // And now the paragraph with text.
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = 0;
            paragraph.Style = "Reference";
            paragraph.AddFormattedText("Протокол испытаний", TextFormat.Bold);
            paragraph.AddTab();
            paragraph.AddText("Бобруйск, ");
            paragraph.AddDateField("dd.MM.yyyy");

            // Create the item table.
            _table = section.AddTable();
            _table.Style = "Table";
            _table.Borders.Color = TableBorder;
            _table.Borders.Width = 0.25;
            _table.Borders.Left.Width = 0.5;
            _table.Borders.Right.Width = 0.5;
            _table.Rows.LeftIndent = 0;

            // Before you can add a row, you must define the columns.
            var column = _table.AddColumn("5cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = _table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = _table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = _table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = _table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = _table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            // Create the header of the table.
            var row = _table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = TableBlue;
            row.Cells[0].AddParagraph("Формула");
            row.Cells[0].Format.Font.Bold = false;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[0].MergeDown = 1;
            row.Cells[1].AddParagraph(testType.ForceName);
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
            //row.Cells[1].MergeRight = 3;
            row.Cells[1].MergeDown = 1;
            row.Cells[2].AddParagraph(testType.HalfForceName);
            row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[2].MergeDown = 1;
            row.Cells[3].AddParagraph(testType.WayName);
            row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[3].MergeDown = 1;
            row.Cells[4].AddParagraph(testType.HalfWayName);
            row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
            //row.Cells[4].MergeDown = 1;
            row.Cells[5].AddParagraph(testType.KoefName);
            row.Cells[5].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[5].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[5].MergeDown = 1;

            row = _table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = TableBlue;
            //row.Cells[1].AddParagraph("Quantity");
            //row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            //row.Cells[2].AddParagraph("Unit Price");
            //row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            //row.Cells[3].AddParagraph("Discount (%)");
            //row.Cells[3].Format.Alignment = ParagraphAlignment.Left;
            //row.Cells[4].AddParagraph("Taxable");
            //row.Cells[4].Format.Alignment = ParagraphAlignment.Left;

            _table.SetEdge(0, 0, 6, 2, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);
        }

        /// <summary>
        /// Creates the dynamic parts of the invoice.
        /// </summary>
        void FillContent()
        {
            const double vat = 0.07;

            // Fill the address in the address text frame.
            var item = SelectItem("/invoice/to");
            //var paragraph = _addressFrame.AddParagraph();
            //paragraph.AddText(GetValue(item, "name/singleName"));
            //paragraph.AddLineBreak();
            //paragraph.AddText(GetValue(item, "address/line1"));
            //paragraph.AddLineBreak();
            //paragraph.AddText(GetValue(item, "address/postalCode") + " " + GetValue(item, "address/city"));
            var paragraph = _addressFrame.AddParagraph();
            paragraph.AddText("Подразделение: " + dataTable.Rows[0][1].ToString());
            paragraph.AddLineBreak();
            paragraph.AddText("Оператор: " + dataTable.Rows[1][1].ToString());
            paragraph.AddLineBreak();
            paragraph.AddText("Тип шины: " + dataTable.Rows[2][1].ToString());
            paragraph.AddLineBreak();
            paragraph.AddText("Номер шины: " + dataTable.Rows[3][1].ToString());
            paragraph.AddLineBreak();
            paragraph.AddText("Размер шины: " + dataTable.Rows[4][1].ToString());
            paragraph.AddLineBreak();
            paragraph.AddText("Методика: " + dataTable.Rows[5][1].ToString());
            paragraph.AddLineBreak();
            paragraph.AddText("Температура в помещении: " + dataTable.Rows[6][1].ToString() + "°С");
            // Iterate the invoice items.
            double totalExtendedPrice = 0;
            var iter = _navigator.Select("/invoice/items/*");
            //while (iter.MoveNext())
            //{
            //    item = iter.Current;
            //    var quantity = GetValueAsDouble(item, "quantity");
            //    var price = GetValueAsDouble(item, "price");
            //    var discount = GetValueAsDouble(item, "discount");

            //    // Each item fills two rows.
            //    var row1 = this._table.AddRow();
            //    var row2 = this._table.AddRow();
            //    row1.TopPadding = 1.5;
            //    row1.Cells[0].Shading.Color = TableGray;
            //    row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            //    row1.Cells[0].MergeDown = 1;
            //    row1.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            //    row1.Cells[1].MergeRight = 3;
            //    row1.Cells[5].Shading.Color = TableGray;
            //    row1.Cells[5].MergeDown = 1;

            //    row1.Cells[0].AddParagraph(GetValue(item, "itemNumber"));
            //    paragraph = row1.Cells[1].AddParagraph();
            //    var formattedText = new FormattedText() { Style = "Title" };
            //    formattedText.AddText(GetValue(item, "title"));
            //    paragraph.Add(formattedText);
            //    paragraph.AddFormattedText(" by ", TextFormat.Italic);
            //    paragraph.AddText(GetValue(item, "author"));
            //    row2.Cells[1].AddParagraph(GetValue(item, "quantity"));
            //    row2.Cells[2].AddParagraph(price.ToString("0.00") + " €");
            //    if (discount > 0)
            //        row2.Cells[3].AddParagraph(discount.ToString("0") + '%');
            //    row2.Cells[4].AddParagraph();
            //    row2.Cells[5].AddParagraph(price.ToString("0.00"));
            //    var extendedPrice = quantity * price;
            //    extendedPrice = extendedPrice * (100 - discount) / 100;
            //    row1.Cells[5].AddParagraph(extendedPrice.ToString("0.00") + " €");
            //    row1.Cells[5].VerticalAlignment = VerticalAlignment.Bottom;
            //    totalExtendedPrice += extendedPrice;

            //    _table.SetEdge(0, _table.Rows.Count - 2, 6, 2, Edge.Box, BorderStyle.Single, 0.75);
            //}
            var row1 = this._table.AddRow();
            int index = 0;
            for (int i = 0; i < 6; i++)
            {
                
                row1.Cells[i].Shading.Color = TableGray;
                row1.Cells[i].VerticalAlignment = VerticalAlignment.Center;
                //row1.Cells[index].AddParagraph(dataTable.Rows[i][1].ToString());
                //row1.Cells[1].Shading.Color = TableGray;
                //row1.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                //row1.Cells[1].AddParagraph(dataTable.Rows[i][1].ToString());
                //row1.Cells[2].Shading.Color = TableGray;
                //row1.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                //row1.Cells[2].AddParagraph(dataTable.Rows[i][2].ToString());
                _table.SetEdge(0, _table.Rows.Count - 2, 6, 2, Edge.Box, BorderStyle.Single, 0.75);
                index++;
            }
            row1.Cells[0].AddParagraph(testType.Formula);
            row1.Cells[1].AddParagraph(testType.ForceValue.ToString());
            row1.Cells[2].AddParagraph(testType.HalfForceValue.ToString());
            row1.Cells[3].AddParagraph(testType.WayValue.ToString());
            row1.Cells[4].AddParagraph(testType.HalfWayValue.ToString());
            row1.Cells[5].AddParagraph(testType.KoefValue.ToString());


            // Add the notes paragraph.
            paragraph = _document.LastSection.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            paragraph.Format.SpaceBefore = "1cm";
            //paragraph.Format.Borders.Width = 0.75;
            //paragraph.Format.Borders.Distance = 3;
            //paragraph.Format.Borders.Color = TableBorder;
            //paragraph.Format.Shading.Color = TableGray;
            //item = SelectItem("/invoice");
            paragraph.AddText("Где: ");
            paragraph.AddLineBreak();
            paragraph.AddText(testType.ForceName + " - " + testType.ForceDiscr);
            paragraph.AddLineBreak();
            paragraph.AddText(testType.HalfForceName + " - " + testType.HalfForceDiscr);
            paragraph.AddLineBreak();
            paragraph.AddText(testType.WayName + " - " + testType.WayDiscr);
            paragraph.AddLineBreak();
            paragraph.AddText(testType.HalfWayName + " - " + testType.HalfWayDiscr);
            paragraph.AddLineBreak();
            paragraph.AddText(testType.KoefName + " - " + testType.KoefForceDiscr);

            //// Add an invisible row as a space line to the table.
            //var row = _table.AddRow();
            //row.Borders.Visible = false;

            //// Add the total price row.
            //row = _table.AddRow();
            //row.Cells[0].Borders.Visible = false;
            //row.Cells[0].AddParagraph("Total Price");
            //row.Cells[0].Format.Font.Bold = true;
            //row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            //row.Cells[0].MergeRight = 4;
            //row.Cells[2].AddParagraph(totalExtendedPrice.ToString("0.00") + " €");
            //row.Cells[2].Format.Font.Name = "Segoe UI";

            //// Add the VAT row.
            //row = _table.AddRow();
            //row.Cells[0].Borders.Visible = false;
            //row.Cells[0].AddParagraph("VAT (" + (vat * 100) + "%)");
            //row.Cells[0].Format.Font.Bold = true;
            //row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            //row.Cells[0].MergeRight = 4;
            //row.Cells[2].AddParagraph((vat * totalExtendedPrice).ToString("0.00") + " €");

            //// Add the additional fee row.
            //row = _table.AddRow();
            //row.Cells[0].Borders.Visible = false;
            //row.Cells[0].AddParagraph("Shipping and Handling");
            //row.Cells[2].AddParagraph(0.ToString("0.00") + " €");
            //row.Cells[0].Format.Font.Bold = true;
            //row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            //row.Cells[0].MergeRight = 4;

            //// Add the total due row.
            //row = _table.AddRow();
            //row.Cells[0].AddParagraph("Total Due");
            //row.Cells[0].Borders.Visible = false;
            //row.Cells[0].Format.Font.Bold = true;
            //row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            //row.Cells[0].MergeRight = 4;
            //totalExtendedPrice += vat * totalExtendedPrice;
            //row.Cells[2].AddParagraph(totalExtendedPrice.ToString("0.00") + " €");
            //row.Cells[2].Format.Font.Name = "Segoe UI";
            //row.Cells[2].Format.Font.Bold = true;

            //// Set the borders of the specified cell range.
            //_table.SetEdge(3, _table.Rows.Count - 4, 1, 4, Edge.Box, BorderStyle.Single, 0.75);

            //// Add the notes paragraph.
            //paragraph = _document.LastSection.AddParagraph();
            //paragraph.Format.Alignment = ParagraphAlignment.Center;
            //paragraph.Format.SpaceBefore = "1cm";
            //paragraph.Format.Borders.Width = 0.75;
            //paragraph.Format.Borders.Distance = 3;
            //paragraph.Format.Borders.Color = TableBorder;
            //paragraph.Format.Shading.Color = TableGray;
            ////item = SelectItem("/invoice");
            ////paragraph.AddText(GetValue(item, "notes"));
        }

        /// <summary>
        /// Selects a subtree in the XML data.
        /// </summary>
        XPathNavigator SelectItem(string path)
        {
            var iter = _navigator.Select(path);
            iter.MoveNext();
            return iter.Current;
        }

        /// <summary>
        /// Gets an element value from the XML data.
        /// </summary>
        static string GetValue(XPathNavigator nav, string name)
        {
            //nav = nav.Clone();
            var iter = nav.Select(name);
            iter.MoveNext();
            return iter.Current.Value;
        }

        /// <summary>
        /// Gets an element value as double from the XML data.
        /// </summary>
        static double GetValueAsDouble(XPathNavigator nav, string name)
        {
            try
            {
                var value = GetValue(nav, name);
                if (value.Length == 0)
                    return 0;
                return Double.Parse(value, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return 0;
        }

        // Some pre-defined colors...
#if true
        // ... in RGB.
        readonly static Color TableBorder = new Color(81, 125, 192);
        readonly static Color TableBlue = new Color(235, 240, 249);
        readonly static Color TableGray = new Color(242, 242, 242);
#else
        // ... in CMYK.
        readonly static Color TableBorder = Color.FromCmyk(100, 50, 0, 30);
        readonly static Color TableBlue = Color.FromCmyk(0, 80, 50, 30);
        readonly static Color TableGray = Color.FromCmyk(30, 0, 0, 0, 100);
#endif
    
    }
}
