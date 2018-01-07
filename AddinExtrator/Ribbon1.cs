using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using AddinExtrator.ItemProcess;
using Common;
using Common.Persistence;
using Microsoft.Office.Tools.Ribbon;
using OpenCalais.Callers;
using OpenCalais.Objects;

namespace AddinExtrator
{
    public partial class Ribbon1
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
        }

        private void RaiseBtnClick(object sender, RibbonControlEventArgs e)
        {
            // Get the Application object
            Microsoft.Office.Interop.Outlook.Application application = Globals.ThisAddIn.Application;

            // Get the active Inspector object and check if is type of MailItem
            Microsoft.Office.Interop.Outlook.Inspector inspector = application.ActiveInspector();
            if (inspector.CurrentItem is Microsoft.Office.Interop.Outlook.MailItem mail)
            {
                var receivers = new List<string>();
                if(mail.CC != null)
                {
                    foreach (var receiver in mail.CC.Split(';'))
                    {
                        receivers.Add(receiver.Trim());
                    }
                }
                foreach(Microsoft.Office.Interop.Outlook.Recipient receiver in mail.Recipients)
                {
                    receivers.Add(receiver.Address);
                }

                var mailItem = new MailItem
                {
                    Id = Guid.NewGuid().ToString(),
                    ReceivedOn = mail.CreationTime,
                    Receivers = receivers,
                    Object = mail.ConversationTopic,
                    Sender = mail.Sender.Address
                };

                try
                {
                    using (var dbHelper = new MongoHelper())
                    using (var caller = new OCCaller(new WebClient(), "<apikey>", string.Empty))
                    using (var processor = new MailProcessor(caller))
                    {
                        foreach (Microsoft.Office.Interop.Outlook.Attachment attachment in mail.Attachments)
                        {
                            var ext = attachment.FileName.Split('.').Last();
                            if (!ext.Equals("pdf") && !ext.Equals("txt") && !ext.Equals("docx")) continue;

                            var attachmentDocument = processor.AnalyzeAttachment<Document>(attachment).Result;
                            if (string.IsNullOrEmpty(attachmentDocument.Name))
                            {
                                attachmentDocument.Name = attachment.DisplayName;
                            }
                            mailItem.Attachments.Add(attachmentDocument);
                        }
                        var mailBodyDocument = processor.AnalyazeMailBody<Document>(mail.Body).Result;
                        mailBodyDocument.Name = "Body";
                        mailItem.Body = mailBodyDocument;

                        dbHelper.Save(mailItem);
                    }

                    MessageBox.Show("Traitement terminé");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erreur pendant le traitement");
                }
            }
        }
    }
}
