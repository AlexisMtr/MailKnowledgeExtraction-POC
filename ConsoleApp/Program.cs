using Common;
using DocumentFormat.OpenXml.Packaging;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using MongoDB.Driver;
using OpenCalais.Callers;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Xml;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("JSON Entry : ");
            var entry = File.ReadAllText(@"F:\developpement\POC-GC\body.txt");
            var attachmentFileName = @"F:\developpement\POC-GC\attachment.pdf";

            using (var caller = new OCCaller(new HttpClient(), "<apikey>", string.Empty))
            {
                var pdfAsText = ExtractTextFromPdf(attachmentFileName);

                var doc = caller.TranformFromResult<Document>(entry).Result;
                var attachment = caller.TranformFromResult<Document>(pdfAsText).Result;

                var item = new MailItem
                {
                    Id = Guid.NewGuid().ToString(),
                    Object = "TestWithAttachment",
                    ReceivedOn = DateTime.Now,
                    Sender = "sender@fai.com",
                    Body = doc
                };
                item.Attachments.Add(attachment);

                var db = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("ViseoGC");
                db.GetCollection<MailItem>("mails").InsertOne(item);
            }

            Console.ReadLine();
        }


        private static string ExtractTextFromPdf(string path)
        {
            ITextExtractionStrategy its = new LocationTextExtractionStrategy();

            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    string thePage = PdfTextExtractor.GetTextFromPage(reader, i, its);
                    string[] theLines = thePage.Split('\n');
                    foreach (var theLine in theLines)
                    {
                        text.AppendLine(theLine);
                    }
                }
                return text.ToString();

            }
        }

        private static string ExtractTextFromDocx(string path)
        {
            const string wordmlNamespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

            StringBuilder textBuilder = new StringBuilder();
            using (WordprocessingDocument wdDoc = WordprocessingDocument.Open(path, false))
            {
                // Manage namespaces to perform XPath queries.  
                NameTable nt = new NameTable();
                XmlNamespaceManager nsManager = new XmlNamespaceManager(nt);
                nsManager.AddNamespace("w", wordmlNamespace);

                // Get the document part from the package.  
                // Load the XML in the document part into an XmlDocument instance.  
                XmlDocument xdoc = new XmlDocument(nt);
                xdoc.Load(wdDoc.MainDocumentPart.GetStream());

                XmlNodeList paragraphNodes = xdoc.SelectNodes("//w:p", nsManager);
                foreach (XmlNode paragraphNode in paragraphNodes)
                {
                    XmlNodeList textNodes = paragraphNode.SelectNodes(".//w:t", nsManager);
                    foreach (XmlNode textNode in textNodes)
                    {
                        textBuilder.Append(textNode.InnerText);
                    }
                    textBuilder.Append(Environment.NewLine);
                }

            }
            return textBuilder.ToString();

        }
    }
}
