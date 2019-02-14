using System.Collections.Generic;
using System.Collections;

namespace LimitedMemory
{
    public class LimitedMemoryCollection<K, V> : ILimitedMemoryCollection<K, V>
    {
        LinkedList<Pair<K, V>> priopity;
        Dictionary<K, LinkedListNode<Pair<K, V>>> keyByNode;

        public LimitedMemoryCollection(int capacity)
        {
            this.Capacity = capacity;
            this.keyByNode = new Dictionary<K, LinkedListNode<Pair<K, V>>>();
            this.priopity = new LinkedList<Pair<K, V>>();
        } 

        public IEnumerator<Pair<K, V>> GetEnumerator()
        {
            return this.priopity.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Capacity { get; private set; }

        public int Count => this.priopity.Count;

        public void Set(K key, V value)
        {
            if (this.keyByNode.ContainsKey(key))
            {
                LinkedListNode<Pair<K, V>> node = this.keyByNode[key];
                this.priopity.Remove(node);
                node.Value.Value = value;
                this.priopity.AddFirst(node);
            }
            else
            {
                RemoveLastElementIfNecessary(key);
                LinkedListNode<Pair<K, V>> node = new LinkedListNode<Pair<K, V>>(new Pair<K, V>(key, value));
                this.priopity.AddFirst(node);
                this.keyByNode.Add(key, node);
            }
        }

        public V Get(K key)
        {
            if (!this.keyByNode.ContainsKey(key))
            {
                throw new KeyNotFoundException("Key does not exists in the collection!");
            }

            LinkedListNode<Pair<K, V>> node = this.keyByNode[key];
            this.priopity.Remove(node);
            this.priopity.AddFirst(node);

            return node.Value.Value;
        }

        private void RemoveLastElementIfNecessary(K key)
        {
            if (this.Capacity <= this.Count)
            {
                var lastNode = this.priopity.Last;
                this.priopity.RemoveLast();
                this.keyByNode.Remove(lastNode.Value.Key);
            }
        }
    }
}
