using Newtonsoft.Json;

namespace OpenCalais.Objects
{
    public class SocialTag : OpenCalaisObject
    {
        public string Name { get; set; }
        public int Importance { get; set; }
        public string OriginalValue { get; set; }
        public string Tag { get; set; }
    }
}
