using System.Collections.Generic;

namespace OpenCalais.Objects
{
    public abstract class Relation : OpenCalaisObject
    {
        public ObjectType Type { get; set; }
        public IEnumerable<Instance> Instances { get; set; }
        public ObjectGroupType GroupType { get; set; }
        public bool ForEndUserDisplay { get; set; }
    }
}
