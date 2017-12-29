using Newtonsoft.Json;

namespace OpenCalais.Objects
{
    public class OpenCalaisObject
    {
        [JsonProperty("_typeGroup")]
        public ObjectGroupType GroupType { get; set; }
        public bool ForEndUserDisplay { get; set; }
    }
}
