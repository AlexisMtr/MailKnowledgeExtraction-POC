using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Common
{
    public class MailItem
    {
        [BsonId]
        public string Id { get; set; }
        [BsonElement]
        public DateTime ReceivedOn { get; set; }
        [BsonElement]
        public string Object { get; set; }
        [BsonElement]
        public string Sender { get; set; }
        [BsonElement]
        public IEnumerable<string> Receivers { get; set; }

        [BsonElement]
        public Document Body { get; set; }
        [BsonElement]
        public IList<Document> Attachments { get; set; }

        public MailItem()
        {
            this.Attachments = new List<Document>();
        }
    }
}
