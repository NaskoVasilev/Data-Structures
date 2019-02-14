using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    private class ListNode<T>
    {
        public T Value { get; private set; }

        public ListNode<T> NextNode { get; set; }

        public ListNode<T> PrevNode { get; set; }

        public ListNode(T value)
        {
            this.Value = value;
        }
    }
    private ListNode<T> head;
    private ListNode<T> tail;
    public int Count { get; private set; }

    public DoublyLinkedList()
    {
        this.head = null;
        this.tail = null;
    }

    public void AddFirst(T element)
    {
        ListNode<T> currentNode = new ListNode<T>(element);
        if (this.Count == 0)
        {
            this.head = currentNode;
            this.tail = this.head;
        }
        else
        {
            currentNode.NextNode = this.head;
            this.head.PrevNode = currentNode;
            this.head = currentNode;
        }

        this.Count++;
    }

    public void AddLast(T element)
    {
        ListNode<T> currentNode = new ListNode<T>(element);

        if (this.Count == 0)
        {
            this.head = this.tail = currentNode;
        }
        else
        {
            this.tail.NextNode = currentNode;
            currentNode.PrevNode = this.tail;
            this.tail = currentNode;
        }

        this.Count++;
    }

    public T RemoveFirst()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("List is empty!");
        }

        ListNode<T> currentNode = this.head;
        T element = currentNode.Value;

        if (this.Count == 1)
        {
            this.head = null;
            this.tail = null;
        }
        else
        {
            this.head = this.head.NextNode;
            currentNode.NextNode = null;
            this.head.PrevNode = null;
        }

        this.Count--;
        return element;
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("List is empty!");
        }
        ListNode<T> currentNode = this.tail;
        T element = currentNode.Value;

        if (this.Count == 1)
        {
            this.head = null;
            this.tail = null;
        }
        else
        {
            this.tail = this.tail.PrevNode;
            this.tail.NextNode = null;
            currentNode.PrevNode = null;
        }

        this.Count--;
        return element;
    }

    public void ForEach(Action<T> action)
    {
        ListNode<T> currentNode = this.head;

        while (currentNode != null)
        {
            action(currentNode.Value);
            currentNode = currentNode.NextNode;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        ListNode<T> currentNode = this.head;

        while (currentNode != null)
        {
            yield return currentNode.Value;
            currentNode = currentNode.NextNode;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public T[] ToArray()
    {
        T[] array = new T[this.Count];
        int index = 0;
        ListNode<T> currentNode = this.head;

        while (currentNode != null)
        {
            array[index] = currentNode.Value;
            index++;
            currentNode = currentNode.NextNode;
        }

        return array;
    }
}


class Example
{
    static void Main()
    {
        var list = new DoublyLinkedList<int>();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.AddLast(5);
        list.AddFirst(3);
        list.AddFirst(2);
        list.AddLast(10);
        Console.WriteLine("Count = {0}", list.Count);

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveFirst();
        list.RemoveLast();
        list.RemoveFirst();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveLast();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");
    }
}
