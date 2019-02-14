using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LinkedQueue<T> : IEnumerable<T>
{
    public int Count { get; private set; }

    private class QueueNode<T>
    {
        public T Value { get; private set; }
        public QueueNode<T> NextNode { get; set; }

        public QueueNode(T value)
        {
            this.Value = value;
        }
    }

    private QueueNode<T> head;
    private QueueNode<T> tail;

    public LinkedQueue()
    {
        this.head = null;
        this.tail = null;
    }

    public void Enqueue(T element)
    {
        QueueNode<T> newNode = new QueueNode<T>(element);

        if (this.Count == 0)
        {
            this.head = newNode;
            this.tail = this.head;
        }
        else
        {
            this.tail.NextNode = newNode;
            this.tail = newNode;
        }

        this.Count++;
    }

    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("Queue is empty!");
        }

        QueueNode<T> firstNode = this.head;
        T element = firstNode.Value;

        if (this.Count == 1)
        {
            this.head = null;
            this.tail = null;
        }
        else
        {
            this.head = this.head.NextNode;
            firstNode.NextNode = null;
        }

        this.Count--;
        return element;
    }

    public T[] ToArray()
    {
        T[] elements = new T[this.Count];
        int index = 0;
        QueueNode<T> currentNode = this.head;

        while (currentNode != null)
        {
            elements[index] = currentNode.Value;
            index++;
            currentNode = currentNode.NextNode;
        }

        return elements;
    }

    public IEnumerator<T> GetEnumerator()
    {
        QueueNode<T> currentNode = this.head;

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
}
