using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Utils.Extensions
{
    public static class DictionaryExtension
    {
        public static TValue TryGet<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key)
        {
            TValue value = default(TValue);

            if (!dic.ContainsKey(key))
                return value;
            else
                return dic[key];
        }
    }
}
