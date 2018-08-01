using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoDragons.Core.Common
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            collection.ToList().ForEach(action);
        }

        public static void ForEachIndex<T>(this IEnumerable<T> collection, Action<T, int> indexAction)
        {
            var coll = collection.ToList();
            for (var i = 0; i < coll.Count; i++)
                indexAction(coll[i], i);
        }

        public static T ValueOrDefault<TKey, T>(this IDictionary<TKey, T> dictionary, TKey key, T defaultValue)
        {
            return dictionary.TryGetValue(key, out var val) ? val : defaultValue;
        }
    }
}
