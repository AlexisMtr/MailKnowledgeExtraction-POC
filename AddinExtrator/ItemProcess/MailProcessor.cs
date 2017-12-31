using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using OpenCalais.Callers;
using OpenCalais.Converters;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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
            attachment.SaveAsFile($@"C:\poc-gc\{attachment.FileName}");

            var ext = this.GetExtensionFile(attachment.FileName);
            string fileAsString = string.Empty;
            if (ext == "pdf")
            {
                fileAsString = this.ExtractTextFromPdf($@"C:\poc-gc\{attachment.FileName}");
            }
            else if(ext == "txt")
            {
                fileAsString = File.ReadAllText($@"C:\poc-gc\{attachment.FileName}");
            }
            else
            {
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
