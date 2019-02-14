using System;

public class AVL<T> where T : IComparable<T>
{
    private Node<T> root;

    public Node<T> Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(T item)
    {
        var node = this.Search(this.root, item);
        return node != null;
    }

    public void Insert(T item)
    {
        this.root = this.Insert(this.root, item);
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private Node<T> Insert(Node<T> node, T item)
    {
        if (node == null)
        {
            return new Node<T>(item);
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            node.Left = this.Insert(node.Left, item);
        }
        else if (cmp > 0)
        {
            node.Right = this.Insert(node.Right, item);
        }

        UpdateHeight(node);

        int balanceFactor = GetBalanceFactor(node);

        //Left child is heavy
        if (balanceFactor > 1)
        {
            int childBalanceFactor = GetBalanceFactor(node.Left);
            if (childBalanceFactor <= -1)
            {
                node.Left = this.RotateLeft(node.Left);
            }

            node = this.RotateRight(node);
        }
        //Right child is heavy
        else if (balanceFactor < -1)
        {
            int childBalanceFactor = GetBalanceFactor(node.Right);
            if (childBalanceFactor >= 1)
            {
                node.Right = this.RotateRight(node.Right);
            }
            node = this.RotateLeft(node);
        }

        return node;
    }

    private Node<T> RotateLeft(Node<T> node)
    {
        Node<T> newRoot = node.Right;
        node.Right = newRoot.Left;
        newRoot.Left = node;
        UpdateHeight(node);
        UpdateHeight(newRoot);

        return newRoot;
    }

    private Node<T> RotateRight(Node<T> node)
    {
        Node<T> newRoot = node.Left;
        node.Left = newRoot.Right;
        newRoot.Right = node;
        UpdateHeight(node);
        UpdateHeight(newRoot);

        return newRoot;
    }

    private void UpdateHeight(Node<T> node)
    {
        node.Height = 1 + Math.Max(this.Height(node.Left), this.Height(node.Right));
    }

    private int GetBalanceFactor(Node<T> node)
    {
        return this.Height(node.Left) - this.Height(node.Right);
    }

    private int Height(Node<T> node)
    {
        return node == null ? 0 : node.Height;
    }

    private Node<T> Search(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            return Search(node.Left, item);
        }
        else if (cmp > 0)
        {
            return Search(node.Right, item);
        }

        return node;
    }

    private void EachInOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }
}
