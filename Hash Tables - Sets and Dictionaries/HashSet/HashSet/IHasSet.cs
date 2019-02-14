using System.Collections.Generic;

namespace HashSet
{
    public interface IHasSet<T> : IEnumerable<T>
    {
        int Count { get; }

        void Add(T value);

        void AddRange(IEnumerable<T> values);

        bool Remove(T value);

        bool Contains(T value);

        void Clear();

        void IntersectWith(IEnumerable<T> values);

        void UnionWith(IEnumerable<T> values);

        void ExceptWith(IEnumerable<T> values);

        void SymetricExceptEith(IEnumerable<T> values);
    }
}
