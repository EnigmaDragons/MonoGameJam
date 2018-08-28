using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoDragons.Core.Common
{
    public static class EnumerableExtensions
    {

        public static TItem Added<TCollectionItem, TItem>(this List<TCollectionItem> list, TItem item)
            where TItem : TCollectionItem
        {
            list.Add(item);
            return item;
        }
        
        public static void ForEach<T>(this List<T> list, Action<T> action) => list.ForEach(action);
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action) => collection.ToList().ForEach(action);

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

        public static List<T> Preferred<T>(this IEnumerable<T> collection, Func<T, bool> preferredCondition)
        {
            var preferred = new List<T>();
            var nonPreferred = new List<T>();
            collection.ForEach(x =>
            {
                if (preferredCondition(x))
                    preferred.Add(x);
                else
                    nonPreferred.Add(x);
            });
            return preferred.Any() ? preferred : nonPreferred;
        }
    }
}
