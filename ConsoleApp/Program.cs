using Common;
using MongoDB.Driver;
using OpenCalais.Callers;
using System;
using System.IO;
using System.Net.Http;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("JSON Entry : ");
            var entry = File.ReadAllText(@"F:\developpement\POC-GC\body.txt");

            using (var caller = new OCCaller(new HttpClient(), "aZJJDBVG8tmdWjaALvTlVnTl1boVd6DXFa", "English"))
            {
                var doc = caller.TranformFromResult<Document>(entry).Result;

                var item = new MailItem
                {
                    Id = Guid.NewGuid().ToString(),
                    Object = "Test",
                    ReceivedOn = DateTime.Now,
                    Sender = "sender@fai.com",
                    Body = doc
                };

                var db = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("ViseoGC");
                db.GetCollection<MailItem>("mails").InsertOne(item);
            }

            Console.ReadLine();
        }
    }
}
