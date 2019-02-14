﻿using System;

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

    public void Delete(T value)
    {
        this.root = Delete(this.root, value);
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            return;
        }

        this.root = DeleteMin(this.root);
    }

    private Node<T> DeleteMin(Node<T> node)
    {
        if (node.Left == null)
        {
            return node.Right;
        }

        node.Left = DeleteMin(node.Left);

        node = Balance(node);
        UpdateHeight(node);
        return node;
    }

    private Node<T> Delete(Node<T> node, T value)
    {
        if (node == null)
        {
            return null;
        }

        int compare = node.Value.CompareTo(value);
        if (compare > 0)
        {
            node.Left = Delete(node.Left, value);
        }
        else if (compare < 0)
        {
            node.Right = Delete(node.Right, value);
        }
        else
        {
            Node<T> minNode = FindMinElement(node.Right);
            if (minNode == null)
            {
                return node.Left;
            }
            else
            {
                node.Value = minNode.Value;
                node.Right = DeleteMin(node.Right);
            }
        }

        node = Balance(node);
        UpdateHeight(node);

        return node;
    }

    private Node<T> FindMinElement(Node<T> node)
    {
        if (node == null)
        {
            return null;
        }
        Node<T> current = node;

        while (current.Left != null)
        {
            current = current.Left;
        }

        return current;
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

        node = Balance(node);
        UpdateHeight(node);

        return node;
    }

    private Node<T> Balance(Node<T> node)
    {
        var balance = Height(node.Left) - Height(node.Right);
        if (balance > 1)
        {
            var childBalance = Height(node.Left.Left) - Height(node.Left.Right);
            if (childBalance < 0)
            {
                node.Left = RotateLeft(node.Left);
            }

            node = RotateRight(node);
        }
        else if (balance < -1)
        {
            var childBalance = Height(node.Right.Left) - Height(node.Right.Right);
            if (childBalance > 0)
            {
                node.Right = RotateRight(node.Right);
            }

            node = RotateLeft(node);
        }

        return node;
    }

    private void UpdateHeight(Node<T> node)
    {
        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
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

    private int Height(Node<T> node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Height;
    }

    private Node<T> RotateRight(Node<T> node)
    {
        var left = node.Left;
        node.Left = left.Right;
        left.Right = node;

        UpdateHeight(node);

        return left;
    }

    private Node<T> RotateLeft(Node<T> node)
    {
        var right = node.Right;
        node.Right = right.Left;
        right.Left = node;

        UpdateHeight(node);

        return right;
    }
}
