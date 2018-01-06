using AddinExtrator.ItemProcess;
using Common;
using OpenCalais.Callers;
using System;
using System.Net.Http;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace AddinExtrator
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
        }

        private async void Items_ItemAdd(object item)
        {
            Outlook.MailItem mail = (Outlook.MailItem)item;
            if (mail != null)
            {
                var mailItem = new MailItem
                {
                    ReceivedOn = mail.CreationTime,
                    Receivers = mail.CC.Split(';'),
                    Object = mail.ConversationTopic,
                    Sender = mail.Sender.Address
                };

                var caller = new OCCaller(new HttpClient(), "<apikey>", "French");
                using (var processor = new MailProcessor(caller))
                {
                    var mailBodyDocumentTask = processor.AnalyazeMailBody<Document>(mail.Body);
                    foreach (Outlook.Attachment attachment in mail.Attachments)
                    {
                        var attachmentDocument = await processor.AnalyzeAttachment<Document>(attachment);
                        if(string.IsNullOrEmpty(attachmentDocument.Name))
                        {
                            attachmentDocument.Name = attachment.DisplayName;
                        }
                        mailItem.Attachments.Add(attachmentDocument);
                    }
                    var mailBodyDocument = await mailBodyDocumentTask;
                    mailBodyDocument.Name = "Body";
                    mailItem.Body = mailBodyDocument;
                }
            }
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
