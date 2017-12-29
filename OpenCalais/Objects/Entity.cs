using Newtonsoft.Json;
using System.Collections.Generic;

namespace OpenCalais.Objects
{
    public class Entity : OpenCalaisObject
    {
        [JsonProperty("_type")]
        public ObjectType Type { get; set; }
        public string Name { get; set; }
        public double Relevance { get; set; }
        public IEnumerable<Instance> Instances { get; set; }
        public IEnumerable<Resolution> Resolutions { get; set; }
        public double ConfidenceLevel { get; set; }
    }
}
