namespace Hierarchy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private Node root;
        private Dictionary<T, Node> nodesByValue;

        public Hierarchy(T root)
        {
            this.root = new Node(root);
            this.nodesByValue = new Dictionary<T, Node>();
            nodesByValue.Add(root, this.root);
        }

        public int Count
        {
            get
            {
                return this.nodesByValue.Count;
            }
        }

        public void Add(T element, T child)
        {
            if (!this.nodesByValue.ContainsKey(element))
            {
                throw new ArgumentException("Element does not exists in the hierarchy!");
            }
            else if (this.nodesByValue.ContainsKey(child))
            {
                throw new ArgumentException("Child already exists in the hierarchy!");
            }

            Node parent = this.nodesByValue[element];
            Node childNode = new Node(child, parent);
            parent.Children.Add(childNode);
            this.nodesByValue.Add(child, childNode);
        }

        public void Remove(T element)
        {
            if (!this.nodesByValue.ContainsKey(element))
            {
                throw new ArgumentException("Element does not exists in the Hierarchy!");
            }

            Node node = this.nodesByValue[element];

            if (node.Parent == null)
            {
                throw new InvalidOperationException("The element is root of the hierarchy!");
            }

            foreach (var childNode in node.Children)
            {
                childNode.Parent = node.Parent;
                node.Parent.Children.Add(childNode);
            }

            this.nodesByValue.Remove(node.Value);
            node.Parent.Children.Remove(node);
        }

        public IEnumerable<T> GetChildren(T item)
        {
            if (!this.nodesByValue.ContainsKey(item))
            {
                throw new ArgumentException("Element does not exist!");
            }

            Node node = this.nodesByValue[item];
            return node.Children.Select(n => n.Value);
        }

        public T GetParent(T item)
        {
            if (!this.nodesByValue.ContainsKey(item))
            {
                throw new ArgumentException("Element does not exists!");
            }

            Node itemNode = this.nodesByValue[item];

            return itemNode.Parent == null ? default(T) : itemNode.Parent.Value;
        }

        public bool Contains(T value)
        {
            return this.nodesByValue.ContainsKey(value);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            //more easy way
            return new HashSet<T>(this.nodesByValue.Keys).Intersect(other.nodesByValue.Keys);

            //List<T> values = new List<T>();

            //foreach (var value in this.nodesByValue.Keys)
            //{
            //    if (other.nodesByValue.ContainsKey(value))
            //    {
            //        values.Add(value);
            //    }
            //}

            //return values;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(this.root);

            while (queue.Count > 0)
            {
                Node element = queue.Dequeue();
                yield return element.Value;

                foreach (var child in element.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class Node
        {
            public Node(T value, Node parent = null)
            {
                this.Value = value;
                this.Parent = parent;
                this.Children = new List<Node>();
            }

            public T Value { get; private set; }

            public Node Parent { get; set; }

            public List<Node> Children { get; private set; }
        }
    }
}