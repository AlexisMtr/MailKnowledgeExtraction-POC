using Newtonsoft.Json;

namespace OpenCalais.Objects.Relations
{
    public class PersonCareer : Relation
    {
        [JsonProperty("person")]
        public string PersonId { get; set; }
        public string Position { get; set; }
    }
}
