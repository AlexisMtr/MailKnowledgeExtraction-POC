using Common.Dtos;
using OC = OpenCalais.Objects;
using System.Collections.Generic;
using System.Linq;

namespace Common.Converters
{
    public class PersonConverter
    {
        public static IEnumerable<Person> Convert(Dictionary<string, OC.Entity> entities, IEnumerable<OC.Relation> relations)
        {
            if (entities == null) return null;

            var personEntities = new List<Person>();
            
            foreach(var entity in entities)
            {
                if (entity.Value.Type != OC.ObjectType.Person) continue;

                OC.Entity person = entity.Value;
                OC.Entity email = null;

                if (relations != null
                    && relations.FirstOrDefault(e => e is OC.Relations.PersonEmailAddress association && association.PersonId == entity.Key) is OC.Relations.PersonEmailAddress relation)
                {
                    email = entities.FirstOrDefault((i) =>
                    {
                        return i.Value is OC.Entity e
                            && e.Type == OC.ObjectType.EmailAddress
                            && i.Key == relation.EmailAddressId;
                    }).Value as OC.Entity;
                }

                personEntities.Add(new Person
                {
                    Name = entity.Value.Name,
                    EmailAddress = email != null ? email.Name : string.Empty
                });
            }

            return personEntities;
        }
    }
}
