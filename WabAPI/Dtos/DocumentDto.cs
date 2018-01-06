using Common.Dtos;
using System.Collections.Generic;

namespace WabAPI.Dtos
{
    public class DocumentDto
    {
        public string Name { get; set; }
        public IEnumerable<string> Technologies { get; set; }
        public IEnumerable<string> IndustryTerms { get; set; }
        public IEnumerable<string> Urls { get; set; }
        public IEnumerable<Topic> Topics { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<Company> Companies { get; set; }
        public IEnumerable<Person> Persons { get; set; }
        public IEnumerable<Contact> Contacts { get; set; }
        public IEnumerable<string> ProgrammingLanguages { get; set; }
    }
}
