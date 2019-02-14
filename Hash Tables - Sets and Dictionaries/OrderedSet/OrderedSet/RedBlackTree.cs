using System;
using System.Collections.Generic;

namespace OrderedSet
{
    public class RedBlackTree<T> where T : IComparable<T>
    {
        private const bool RED = true;
        private const bool BLACK = false;
        private Node root;

        public RedBlackTree()
        {
            this.root = null;
            this.Count = 0;
        }

        public int Count { get; private set; }

        public void Insert(T value)
        {
            this.root = this.Insert(this.root, value);
            this.root.Color = BLACK;
        }

        public bool Contains(T value)
        {
            Node node = this.Find(this.root, value);
            return node != null;
        }

        public void Remove(T value)
        {
            if (this.root == null)
            {
                throw new ArgumentException("There is no enough elements!");
            }

            this.root = this.Remove(this.root, value);
        }

        public IEnumerable<T> InOrederTraversal()
        {
            List<T> elements = new List<T>(this.Count);
            InOrderTraversal(this.root, elements);
            return elements;
        }

        private void InOrderTraversal(Node node, List<T> elements)
        {
            if (node == null)
            {
                return;
            }

            InOrderTraversal(node.Left, elements);
            elements.Add(node.Value);
            InOrderTraversal(node.Right, elements);
        }

        private Node Remove(Node node, T value)
        {
            if (node == null)
            {
                return null;
            }

            int compare = node.Value.CompareTo(value);
            if (compare > 0)
            {
                node.Left = this.Remove(node.Left, value);
            }
            else if (compare < 0)
            {
                node.Right = this.Remove(node.Right, value);
            }
            else
            {
                this.Count--;

                if (node.Right == null)
                {
                    return node.Left;
                }
                if (node.Left == null)
                {
                    return node.Right;
                }

                Node minNode = this.FindMin(node.Right);
                node.Value = minNode.Value;
                node.Right = DeleteMin(node.Right);
            }

            return node;
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
            {
                return node.Right;
            }

            node.Left = DeleteMin(node.Left);
            return node;
        }

        private Node FindMin(Node node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return FindMin(node.Left);
        }

        private Node Find(Node node, T value)
        {
            if (node == null)
            {
                return null;
            }

            int compare = node.Value.CompareTo(value);
            if (compare > 0)
            {
                return Find(node.Left, value);
            }
            else if (compare < 0)
            {
                return Find(node.Right, value);
            }

            return node;
        }

        private Node Insert(Node node, T value)
        {
            if (node == null)
            {
                this.Count++;
                return new Node(value);
            }

            int compare = node.Value.CompareTo(value);
            if (compare > 0)
            {
                node.Left = this.Insert(node.Left, value);
            }
            else if (compare < 0)
            {
                node.Right = this.Insert(node.Right, value);
            }

            node = Balance(node);
            return node;
        }

        private Node Balance(Node node)
        {
            if (IsRed(node.Right) && !IsRed(node.Left))
            {
                node = this.RotateLeft(node);
            }
            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = this.RotateRight(node);
            }
            if (IsRed(node.Left) && IsRed(node.Right))
            {
                node = FlipColors(node);
            }

            return node;
        }

        private bool IsRed(Node node)
        {
            if (node == null)
            {
                return false;
            }

            return node.Color == RED;
        }

        private Node RotateLeft(Node node)
        {
            Node temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;
            temp.Color = node.Color;
            node.Color = RED;

            return temp;
        }

        private Node RotateRight(Node node)
        {
            Node temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;
            temp.Color = node.Color;
            node.Color = RED;

            return temp;
        }

        private Node FlipColors(Node node)
        {
            node.Color = RED;
            node.Left.Color = BLACK;
            node.Right.Color = BLACK;

            return node;
        }

        private class Node
        {
            public Node(T value)
            {
                Value = value;
                this.Color = RED;
            }

            public bool Color { get; set; }
            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }
    }
}