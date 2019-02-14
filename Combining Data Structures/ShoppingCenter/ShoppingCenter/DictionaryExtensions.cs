using System.Collections.Generic;

namespace ShoppingCenter
{
    public static class DictionaryExtensions
    {
        public static void AppendValueToKey<TKey, TValue, TCollection>(
            this IDictionary<TKey, TCollection> dict, TKey key, TValue value)
            where TCollection : ICollection<TValue>, new()
        {
            TCollection collection;
            if (!dict.TryGetValue(key, out collection))
            {
                collection = new TCollection();
                dict.Add(key, collection);
            }
            dict[key].Add(value);
        }
    }
}
