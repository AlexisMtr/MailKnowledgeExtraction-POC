using Newtonsoft.Json;

namespace OpenCalais.Objects.Relations
{
    public class PersonEmailAddress : Relation
    {
        [JsonProperty("person")]
        public string PersonId { get; set; }
        [JsonProperty("emailAddress")]
        public string EmailAddressId { get; set; }
    }
}
