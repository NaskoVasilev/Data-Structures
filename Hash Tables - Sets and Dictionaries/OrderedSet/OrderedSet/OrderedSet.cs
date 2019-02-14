using System;
using System.Collections;
using System.Collections.Generic;

namespace OrderedSet
{
    public class OrderedSet<T> : IOrderedSet<T> where T : IComparable<T>
    {
        private RedBlackTree<T> redBlackTree;

        public OrderedSet()
        {
            this.redBlackTree = new RedBlackTree<T>();
        }

        public int Count => redBlackTree.Count;

        public void Add(T element)
        {
            redBlackTree.Insert(element);
        }

        public bool Contains(T element)
        {
            return redBlackTree.Contains(element);
        }

        public void Remove(T element)
        {
            redBlackTree.Remove(element);
        }

        public IEnumerator<T> GetEnumerator()
        {
            IEnumerable<T> elements = redBlackTree.InOrederTraversal();
            return elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
