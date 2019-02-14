using System;
using System.Collections;
using System.Collections.Generic;

namespace HashSet
{
    public class HashSet<T> : IHasSet<T>
    {
        private const double LoadFactor = 0.75;
        private const int InitialCapacity = 16;
        private LinkedList<T>[] slots;

        public HashSet(int capacity = InitialCapacity)
        {
            this.Count = 0;
            this.slots = new LinkedList<T>[capacity];
        }

        public HashSet(IEnumerable<T> values) : this()
        {
            this.AddRange(values);
        }

        public int Count { get; private set; }

        public void Add(T value)
        {
            GrowIfNeeded();
            int index = GetIndex(value);

            if (this.slots[index] == null)
            {
                this.slots[index] = new LinkedList<T>();
                this.slots[index].AddLast(value);
            }
            else
            {
                foreach (var element in this.slots[index])
                {
                    if (element.Equals(value))
                    {
                        return;
                    }
                }

                this.slots[index].AddLast(value);
            }

            this.Count++;
        }

        public void AddRange(IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                this.Add(value);
            }
        }

        public void Clear()
        {
            this.slots = new LinkedList<T>[InitialCapacity];
            this.Count = 0;
        }

        public bool Contains(T value)
        {
            int index = GetIndex(value);

            if (this.slots[index] != null)
            {
                foreach (var element in this.slots[index])
                {
                    if (element.Equals(value))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void ExceptWith(IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                if (this.Contains(value))
                {
                    this.Remove(value);
                }
            }
        }

        public void IntersectWith(IEnumerable<T> values)
        {
            HashSet<T> newHashSet = new HashSet<T>();

            foreach (var value in values)
            {
                if (this.Contains(value))
                {
                    newHashSet.Add(value);
                }
            }

            this.slots = newHashSet.slots;
        }

        public bool Remove(T value)
        {
            int index = GetIndex(value);
            var elements = this.slots[index];

            if (elements != null)
            {
                LinkedListNode<T> current = elements.First;

                while (current != null)
                {
                    if (current.Value.Equals(value))
                    {
                        elements.Remove(current);
                        this.Count--;
                        return true;
                    }

                    current = current.Next;
                }
            }

            return false;
        }

        public void SymetricExceptEith(IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                if (this.Contains(value))
                {
                    this.Remove(value);
                }
                else
                {
                    this.Add(value);
                }
            }
        }

        public void UnionWith(IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                this.Add(value);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var element in this.slots)
            {
                if (element != null)
                {
                    foreach (var value in element)
                    {
                        yield return value;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private int GetIndex(T value)
        {
            return Math.Abs(value.GetHashCode()) % this.slots.Length;
        }

        private void GrowIfNeeded()
        {
            double loadFactor = (this.Count + 1) / this.slots.Length;

            if (loadFactor >= LoadFactor)
            {
                HashSet<T> hashSet = new HashSet<T>(this.slots.Length * 2);

                foreach (var value in this)
                {
                    hashSet.Add(value);
                }

                this.slots = hashSet.slots;
            }
        }
    }
}