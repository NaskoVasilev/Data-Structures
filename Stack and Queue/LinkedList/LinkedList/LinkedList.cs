using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedList<T> : IEnumerable<T>
{
    public class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }

    public Node Head { get; private set; }
    public Node Tail { get; private set; }
    public int Count { get; private set; }

    public LinkedList()
    {
        this.Head = null;
        this.Tail = null;
        this.Count = 0;
    }

    public bool IsEmpty()
    {
        return this.Count == 0;
    }

    public void AddFirst(T item)
    {
        Node oldHead = this.Head;
        this.Head = new Node(item);
        this.Head.Next = oldHead;

        if (this.IsEmpty())
        {
            this.Tail = this.Head;
        }

        this.Count++;
    }

    public void AddLast(T item)
    {
        Node oldNode = this.Tail;
        this.Tail = new Node(item);

        if (this.IsEmpty())
        {
            this.Head = this.Tail;
        }
        else
        {
            oldNode.Next = this.Tail;
        }

        this.Count++;
    }

    public T RemoveFirst()
    {
        if (this.IsEmpty())
        {
            throw new InvalidOperationException("List is empty!");
        }

        T elementToRemove = this.Head.Value;

        if (this.Count == 1)
        {
            this.Head = null;
            this.Tail = null;
        }
        else
        {
            this.Head = this.Head.Next;
        }

        this.Count--;
        return elementToRemove;
    }

    public T RemoveLast()
    {
        if (this.IsEmpty())
        {
            throw new InvalidOperationException("List is empty!");
        }

        T elementToRemove = this.Tail.Value;

        if (this.Count == 1)
        {
            this.Tail = null;
            this.Head = null;
        }
        else
        {
            Node parentNode = GetParentOfTail();
            parentNode.Next = null;
            this.Tail = parentNode;
        }

        this.Count--;
        return elementToRemove;
    }

    private Node GetParentOfTail()
    {
        Node start = this.Head;

        while (start.Next != this.Tail)
        {
            start = start.Next;
        }

        return start;
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node start = this.Head;

        while (start != null)
        {
            yield return start.Value;
            start = start.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
