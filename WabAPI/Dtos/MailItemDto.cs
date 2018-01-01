using System;

namespace WabAPI.Dtos
{
    public class MailItemDto
    {
        public string Id { get; set; }
        public DateTime ReceivedOn { get; set; }
        public string Object { get; set; }
        public string Sender { get; set; }

        public int AttachmentsCount { get; set; }
    }
}
