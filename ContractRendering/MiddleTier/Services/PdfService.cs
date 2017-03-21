using Microsoft.Practices.Unity;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Services.Interfaces;
using Sabio.Web.Services.S3Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.XPath;

namespace Sabio.Web.Services
{
    public class PdfService : BaseService, IPdfService
    {

        [Dependency]
        public IBidService _BidService { get; set; }


        public Document CreateDocument()
        {
            Document document = new Document();

            Section section = document.AddSection();

            Paragraph paragraph = section.AddParagraph();

            paragraph.Format.Font.Color = Color.FromCmyk(100, 30, 20, 50);

            paragraph.AddFormattedText("Hello, World!", TextFormat.Bold);

            return document;
        }


        public byte[] GenerateDocument()
        {
            Document document = CreateDocument();
            document.UseCmykColor = true;

            const bool unicode = false;

            const PdfFontEmbedding embedding = PdfFontEmbedding.Always;

            DocumentRenderer renderer = new DocumentRenderer(document);

            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(unicode, embedding);

            pdfRenderer.Document = document;

            pdfRenderer.RenderDocument();

            MemoryStream stream = new MemoryStream();

            pdfRenderer.PdfDocument.Save(stream, false);

            byte[] bytes = stream.ToArray();

            return bytes;

        }



        public byte[] GenerateBidDocument(int BidId)
        {
            Document document = CreateBidDocument(BidId);
            document.UseCmykColor = true;

            const bool unicode = false;

            const PdfFontEmbedding embedding = PdfFontEmbedding.Always;

            DocumentRenderer renderer = new DocumentRenderer(document);

            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(unicode, embedding);

            pdfRenderer.Document = document;

            pdfRenderer.RenderDocument();

            MemoryStream stream = new MemoryStream();

            pdfRenderer.PdfDocument.Save(stream, false);

            byte[] bytes = stream.ToArray();

            return bytes;

        }


        public Document CreateBidDocument(int BidId)
        {
            Document document = new Document();
            document.Info.Title = "Sample Bid From Database";
            document.Info.Subject = "Display The Winning Bid";
            document.Info.Author = "Bid Winner";

            try
            {
                DefineStyles(document, BidId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return document;
        }


        public void DefineStyles(Document document, int BidId)
        {
            Style style = document.Styles["Normal"];

            CreatePage(document, BidId);
        }


        public void CreatePage(Document document, int BidId)
        {
            BidDomain bid = _BidService.BidGetById(BidId);

            Section section = document.AddSection();

            // Create the text frame for the address
            TextFrame addressFrame = section.AddTextFrame();
            addressFrame.Height = "3.0cm";
            addressFrame.Width = "7.0cm";
            addressFrame.Left = ShapePosition.Left;
            addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            addressFrame.Top = "5.0cm";
            addressFrame.RelativeVertical = RelativeVertical.Page;

            // Add  content to address frame
            Paragraph paragraph = addressFrame.AddParagraph();
            paragraph.AddText(bid.Address1 + ", " + bid.City);
            paragraph.AddLineBreak();
            paragraph.AddText(bid.State + " " + bid.ZipCode);

            Paragraph paragraph2 = section.AddParagraph(); 
            paragraph2.Format.SpaceBefore = "8cm";

            // Create the item table
            Table table = section.AddTable();
            table.Style = "Table";
            //table.Borders.Color = TableBorder;
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;

            // Before you can add a row, you must define the columns
            Column column = table.AddColumn("1cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn("2.5cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = table.AddColumn("3.5cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            //column = table.AddColumn("2cm");
            //column.Format.Alignment = ParagraphAlignment.Center;

            //column = table.AddColumn("4cm");
            //column.Format.Alignment = ParagraphAlignment.Right;

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            //row.Shading.Color = TableBlue;
            row.Cells[0].AddParagraph("Item");
            row.Cells[0].Format.Font.Bold = false;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
            row.Cells[0].MergeDown = 1;
            //row.Cells[1].AddParagraph("Title and Author");
            //row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            //row.Cells[1].MergeRight = 3;
            //row.Cells[5].AddParagraph("Extended Price");
            //row.Cells[5].Format.Alignment = ParagraphAlignment.Left;
            //row.Cells[5].VerticalAlignment = VerticalAlignment.Bottom;
            //row.Cells[5].MergeDown = 1;

            row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            //row.Shading.Color = TableBlue;
            row.Cells[1].AddParagraph("Quote Request");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[2].AddParagraph("Quote Request Item");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[3].AddParagraph("Amount");
            row.Cells[3].Format.Alignment = ParagraphAlignment.Left;
            //row.Cells[4].AddParagraph("Taxable");
            //row.Cells[4].Format.Alignment = ParagraphAlignment.Left;


            string quoteRequestName = bid.QrName;
            string quoteRequestItemName = bid.QriName;
            decimal bidAmount = bid.Amount;

            Row row1 = table.AddRow();
            row1.Cells[0].AddParagraph("1");
            row1.Cells[1].AddParagraph(quoteRequestName);
            row1.Cells[2].AddParagraph(quoteRequestItemName);
            row1.Cells[3].AddParagraph("$" + bidAmount.ToString("0.00"));


            //table.SetEdge(0, 0, 6, 6, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);


        }



        // FOR CONTRACTS
        public string GenerateContractDocument(ContractInsertRequest model)
        {
            Document document = CreateContractDocument(model);
            document.UseCmykColor = true;

            const bool unicode = false;

            const PdfFontEmbedding embedding = PdfFontEmbedding.Always;

            DocumentRenderer renderer = new DocumentRenderer(document);

            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(unicode, embedding);

            pdfRenderer.Document = document;

            pdfRenderer.RenderDocument();

            MemoryStream stream = new MemoryStream();

            pdfRenderer.PdfDocument.Save(stream, false);

            S3Request s3Request = new S3Request();

            s3Request.NewFileName = Guid.NewGuid().ToString() + ".pdf";

            s3Request.FileStream = stream;

            S3Handler s3Handler = new S3Handler();

            string filename = s3Handler.UploadStream(s3Request);

            return filename;

            //byte[] bytes = stream.ToArray();

            //return bytes;

        }


        public Document CreateContractDocument(ContractInsertRequest model)
        {
            Document document = new Document();
            document.Info.Title = "Contract";
            document.Info.Subject = "Genera Contract";
            document.Info.Author = "QuoteMule";

            try
            {
                DefineContractStyles(document, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return document;
        }


        public void DefineContractStyles(Document document, ContractInsertRequest model)
        {
            Style style = document.Styles["Normal"];

            CreateContract(document, model);
        }


        public void CreateContract(Document document, ContractInsertRequest model)
        {
            Section section = document.AddSection();

            // Add  content to address frame
            Paragraph paragraph = section.AddParagraph();
            paragraph.AddText(model.ContractTerms);

        }


    }
}