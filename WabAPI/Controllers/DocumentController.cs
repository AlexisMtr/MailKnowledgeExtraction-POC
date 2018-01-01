using AutoMapper;
using Common;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using WabAPI.Dtos;

namespace WabAPI.Controllers
{
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        private readonly IMongoClient client;
        private readonly IMongoDatabase database;

        public DocumentController()
        {
            this.client = new MongoClient("mongodb://127.0.0.1:27017");
            this.database = this.client.GetDatabase("ViseoGC");
        }


        [HttpGet]
        public IActionResult Get()
        {
            var mails = this.database.GetCollection<MailItem>("mails").AsQueryable().ToList();
            return Ok(Mapper.Map<IEnumerable<MailItemDto>>(mails));
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var mail = this.database.GetCollection<MailItem>("mails").AsQueryable().FirstOrDefault(e => e.Id.Equals(id));
            return Ok(mail);
        }
    }
}
