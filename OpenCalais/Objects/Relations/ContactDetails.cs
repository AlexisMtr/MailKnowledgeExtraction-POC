using Newtonsoft.Json;

namespace OpenCalais.Objects.Relations
{
    public class ContactDetails : Relation
    {
        [JsonProperty("telephone1")]
        public string Telephone { get; set; }
        [JsonProperty("emailaddress1")]
        public string EmailAddress { get; set; }
    }
}
