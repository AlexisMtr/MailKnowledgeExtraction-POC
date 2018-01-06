using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Common.Converters;
using Common.Dtos;
using MongoDB.Bson.Serialization.Attributes;
using OpenCalais.Converters;
using OC = OpenCalais.Objects;

namespace Common
{
    public class Document : ITransform<Document>
    {
        [BsonId]
        public string Id { get; set; }
        [BsonElement]
        public string Name { get; set; }

        [BsonElement]
        public IEnumerable<Company> Companies { get; set; }
        [BsonElement]
        public IEnumerable<Person> Persons { get; set; }
        [BsonElement]
        public IEnumerable<Entity> Technologies { get; set; }
        [BsonElement]
        public IEnumerable<Tag> Tags { get; set; }
        [BsonElement]
        public IEnumerable<Topic> Topics { get; set; }
        [BsonElement]
        public IEnumerable<Entity> IndustryTerms { get; set; }
        [BsonElement]
        public IEnumerable<Entity> Urls { get; set; }
        [BsonElement]
        public IEnumerable<Contact> Contacts { get; set; }
        [BsonElement]
        public IEnumerable<Entity> ProgrammingLanguage { get; set; }


        public Document Transform(Dictionary<string, OC.OpenCalaisObject> source)
        {
            MapperConfiguration.ConfigureMapper();

            var sourceTag = source.Where(e => e.Value is OC.SocialTag).Select(e => e.Value as OC.SocialTag);
            this.Tags = Mapper.Map<IEnumerable<Tag>>(sourceTag);

            var sourceTopics = source.Where(e => e.Value is OC.Topic).Select(e => e.Value as OC.Topic);
            this.Topics = Mapper.Map<IEnumerable<Topic>>(sourceTopics);

            var sourceTechnologies = source.Where(e =>
            {
                if (e.Value is OC.Entity i) return i.Type == OC.ObjectType.Technology;
                return false;
            }).Select(e => e.Value as OC.Entity);
            this.Technologies = Mapper.Map<IEnumerable<Entity>>(sourceTechnologies);

            var sourceUrls = source.Where(e =>
            {
                if (e.Value is OC.Entity i) return i.Type == OC.ObjectType.Url;
                return false;
            }).Select(e => e.Value as OC.Entity);
            this.Urls = Mapper.Map<IEnumerable<Entity>>(sourceUrls);

            var sourceIndustryTerms = source.Where(e =>
            {
                if (e.Value is OC.Entity i) return i.Type == OC.ObjectType.IndustryTerm;
                return false;
            }).Select(e => e.Value as OC.Entity);
            this.IndustryTerms = Mapper.Map<IEnumerable<Entity>>(sourceIndustryTerms);

            var contactsSource = source.Where(e =>
            {
                if (e.Value is OC.Relations.ContactDetails i) return i.Type == OC.ObjectType.ContactDetails;
                return false;
            }).Select(e => e.Value as OC.Relations.ContactDetails);
            this.Contacts = Mapper.Map<IEnumerable<Contact>>(contactsSource);

            var languageSource = source.Where(e =>
            {
                if (e.Value is OC.Entity i) return i.Type == OC.ObjectType.ProgrammingLanguage;
                return false;
            }).Select(e => e.Value as OC.Entity);
            this.ProgrammingLanguage = Mapper.Map<IEnumerable<Entity>>(languageSource);

            this.Persons = PersonConverter.Convert(source.Where(e => e.Value is OC.Entity).ToDictionary(e => e.Key, e => e.Value as OC.Entity),
                source.Where(e => e.Value is OC.Relation).Select(e => e.Value as OC.Relation));

            this.Companies = CompanyConverter.Convert(source.Where(e => e.Value is OC.Entity c && c.Type == OC.ObjectType.Company).Select(e => e.Value as OC.Entity));

            return this;
        }
    }
}
