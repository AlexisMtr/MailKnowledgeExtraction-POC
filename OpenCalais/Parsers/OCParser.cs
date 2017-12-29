using Newtonsoft.Json;
using OpenCalais.Converters;
using OpenCalais.Objects;
using System.Collections.Generic;

namespace OpenCalais.Parsers
{
    public static class OCParser
    {
        public static Dictionary<string, OpenCalaisObject> Parse(string json)
        {
            Dictionary<string, OpenCalaisObject> document = new Dictionary<string, OpenCalaisObject>();

            var keyDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            foreach(var key in keyDictionary)
            {
                var item = JsonConvert.DeserializeObject<OpenCalaisObject>(key.Value.ToString(), new OpenCalaisConverter());
                document.Add(key.Key, item);
            }

            return document;
        }
    }
}
