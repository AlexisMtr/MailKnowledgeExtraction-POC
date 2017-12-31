using OpenCalais.Objects;
using System.Collections.Generic;

namespace OpenCalais.Converters
{
    public interface ITransform<TResult>
    {
        TResult Transform(Dictionary<string, OpenCalaisObject> source);
    }
}
