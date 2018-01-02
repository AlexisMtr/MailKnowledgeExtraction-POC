using Common;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using MongoDB.Driver;
using OpenCalais.Callers;
using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("JSON Entry : ");
            var entry = File.ReadAllText(@"F:\developpement\POC-GC\body.txt");
            var attachmentFileName = @"F:\developpement\POC-GC\attachment.pdf";

            using (var caller = new OCCaller(new HttpClient(), "ZJJDBVG8tmdWjaALvTlVnTl1boVd6DXF", string.Empty))
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
    }
}
