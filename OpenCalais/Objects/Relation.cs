using System.Collections.Generic;

namespace OpenCalais.Objects
{
    public abstract class Relation : OpenCalaisObject
    {
        public ObjectType Type { get; set; }
        public IEnumerable<Instance> Instances { get; set; }
    }
}
