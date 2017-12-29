using Newtonsoft.Json.Linq;
using OpenCalais.Objects;
using OpenCalais.Objects.Relations;
using System;

namespace OpenCalais.Converters
{
    public class OpenCalaisConverter : JsonCreationConverter<OpenCalaisObject>
    {
        protected override OpenCalaisObject Create(Type objectType, JObject jObject)
        {
            if(FieldHasValue("_typeGroup", ObjectGroupType.Entities.ToString(), jObject))
            {
                return new Entity();
            }
            if (FieldHasValue("_typeGroup", ObjectGroupType.Topics.ToString(), jObject))
            {
                return new Topic();
            }
            if (FieldHasValue("_typeGroup", ObjectGroupType.SocialTag.ToString(), jObject))
            {
                return new SocialTag();
            }
            if (FieldHasValue("_typeGroup", ObjectGroupType.Relations.ToString(), jObject))
            {
                if (FieldHasValue("_type", ObjectType.PersonCareer.ToString(), jObject))
                    return new PersonCareer();
                if (FieldHasValue("_type", ObjectType.PersonEmailAddress.ToString(), jObject))
                    return new PersonEmailAddress();
            }

            return new OpenCalaisObject();
        }

        private bool FieldHasValue(string fieldName, string value, JObject jObject)
        {
            return jObject[fieldName] != null && jObject[fieldName].Value<string>().ToLower() == value.ToLower();
        }
    }
}
