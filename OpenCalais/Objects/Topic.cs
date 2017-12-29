using Newtonsoft.Json;

namespace OpenCalais.Objects
{
    public class Topic : OpenCalaisObject
    {
        public double Score { get; set; }
        public string Name { get; set; }
    }
}
