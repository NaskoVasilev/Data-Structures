using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    private Node root;

    private class Node
    {
        public T Value { get; private set; }

        public Node Left { get; set; }

        public Node Right { get; set; }

        public Node(T value)
        {
            this.Value = value;
            this.Left = null;
            this.Right = null;
        }
    }

    public BinarySearchTree()
    {
        this.root = null;
    }

    private BinarySearchTree(Node node)
    {
        this.Copy(node);
    }

    private void Copy(Node node)
    {
        if (node != null)
        {
            this.Insert(node.Value);
            this.Copy(node.Left);
            this.Copy(node.Right);
        }
    }

    public void Insert(T value)
    {
        Node newNode = new Node(value);

        if (this.root == null)
        {
            this.root = newNode;
            return;
        }

        Node parent = null;
        Node current = this.root;

        while (current != null)
        {
            int compare = current.Value.CompareTo(value);

            if (compare > 0)
            {
                // current.Value > value
                parent = current;
                current = current.Left;
            }
            else if (compare < 0)
            {
                // current.Value < value
                parent = current;
                current = current.Right;
            }
            else
            {
                // current.Value = value
                return;
            }
        }

        if (parent.Value.CompareTo(value) > 0)
        {
            parent.Left = newNode;
        }
        else
        {
            parent.Right = newNode;
        }
    }

    public void InsertRecursive(T value)
    {
        this.root = this.InsertRecursive(this.root, value);
    }

    private Node InsertRecursive(Node node, T value)
    {
        if (node == null)
        {
            return new Node(value);
        }

        int compare = node.Value.CompareTo(value);

        if (compare > 0)
        {
            node.Left = this.InsertRecursive(node.Left, value);
        }
        else if (compare < 0)
        {
            node.Right = this.InsertRecursive(node.Right, value);
        }

        return node;
    }

    public bool Contains(T value)
    {
        Node current = this.root;

        while (current != null)
        {
            int compare = current.Value.CompareTo(value);

            if (compare > 0)
            {
                // current.Value > value
                current = current.Left;
            }
            else if (compare < 0)
            {
                // current.value < value
                current = current.Right;
            }
            else
            {
                return true;
            }
        }

        return false;
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            return;
        }

        Node current = this.root;
        Node parent = null;

        while (current.Left != null)
        {
            parent = current;
            current = current.Left;
        }

        if (parent == null)
        {
            this.root = this.root.Right;
        }
        else
        {
            parent.Left = current.Right;
        }
    }

    public BinarySearchTree<T> Search(T item)
    {
        Node current = this.root;

        while (current != null)
        {
            int compare = current.Value.CompareTo(item);

            if (compare > 0)
            {
                current = current.Left;
            }
            else if (compare < 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        if (current == null)
        {
            return null;
        }
        return new BinarySearchTree<T>(current);
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        List<T> result = new List<T>();
        this.Range(this.root, result, startRange, endRange);
        return result;
    }

    private void Range(Node node, List<T> result, T start, T end)
    {
        if (node == null)
        {
            return;
        }

        int lowerBoundCompare = node.Value.CompareTo(start);
        int upperBoundCompare = node.Value.CompareTo(end);

        if (lowerBoundCompare > 0)
        {
            this.Range(node.Left, result, start, end);
        }

        if (lowerBoundCompare >= 0 && upperBoundCompare <= 0)
        {
            result.Add(node.Value);
        }

        if (upperBoundCompare < 0)
        {
            this.Range(node.Right, result, start, end);
        }
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node node, Action<T> action)
    {
        if (node != null)
        {
            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.InsertRecursive(10);
        tree.InsertRecursive(5);
        tree.InsertRecursive(15);
        tree.InsertRecursive(20);
        tree.InsertRecursive(12);

        List<int> result = new List<int>();
        tree.EachInOrder(result.Add);
        Console.WriteLine(string.Join(" ", result));

    }
}
