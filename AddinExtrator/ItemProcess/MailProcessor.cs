using DocumentFormat.OpenXml.Packaging;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using OpenCalais.Callers;
using OpenCalais.Converters;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AddinExtrator.ItemProcess
{
    public class MailProcessor : IDisposable
    {
        private readonly OCCaller caller;

        public MailProcessor(OCCaller caller)
        {
            this.caller = caller;
        }

        public async Task<T> AnalyzeAttachment<T>(Microsoft.Office.Interop.Outlook.Attachment attachment) where T : ITransform<T>
        {
            try
            {
                attachment.SaveAsFile($@"C:\poc-gc\{attachment.FileName}");
            }
            catch(DirectoryNotFoundException)
            {
                Directory.CreateDirectory(@"C:\poc-gc\");
                attachment.SaveAsFile($@"C:\poc-gc\{attachment.FileName}");
            }

            var ext = this.GetExtensionFile(attachment.FileName);
            string fileAsString = string.Empty;
            switch(ext)
            {
                case "pdf":
                    fileAsString = this.ExtractTextFromPdf($@"C:\poc-gc\{attachment.FileName}");
                    break;
                case "txt":
                    fileAsString = File.ReadAllText($@"C:\poc-gc\{attachment.FileName}");
                    break;
                case "docx":
                    fileAsString = this.ExtractTextFromDocx($@"C:\poc-gc\{attachment.FileName}");
                    break;
                default:
                    throw new InvalidDataException("File format is not valid");
            }

            return await caller.TranformFromResult<T>(fileAsString);
        }
        
        public async Task<T> AnalyazeMailBody<T>(string mailBody) where T : ITransform<T>
        {
            return await caller.TranformFromResult<T>(mailBody);
        }

        private string GetExtensionFile(string fileName)
        {
            var parts = fileName.Split('.');
            var index = parts.Length - 1;
            return parts[index];
        }

        private string ExtractTextFromDocx(string path)
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

        private string ExtractTextFromPdf(string path)
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

        public void Dispose()
        {
            if(this.caller != null)
                this.caller.Dispose();
        }
    }
}
