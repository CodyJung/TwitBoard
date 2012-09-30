using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tweetinvi.Utils
{
    public static class DictionaryExtension
    {
        public static object GetProp(this Dictionary<String, object> dictionary, string prop_name)
        {
            object result = null;
            dictionary.TryGetValue(prop_name, out result);            
            return result;
        }
    }
}
