using System.Collections.Generic;

namespace OrderedSet
{
    public interface IOrderedSet<T> : IEnumerable<T>
    {
        int Count { get; }

        void Add(T element);

        bool Contains(T element);

        void Remove(T element);
    }
}
