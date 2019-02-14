using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LinkedStack<T>
{
    public int Count { get; set; }
    private Node top;
    private class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; }

        public Node(T value)
        {
            this.Value = value;
        }
    }

    public LinkedStack()
    {
        this.Count = 0;
        this.top = null;
    }

    public void Push(T element)
    {
        Node currentNode = new Node(element);

        if (this.Count == 0)
        {
            top = currentNode;
        }
        else
        {
            currentNode.Next = top;
            top = currentNode;
        }

        this.Count++;
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("Stack is empty!");
        }

        Node currentNode = this.top;
        T element = currentNode.Value;

        this.top = this.top.Next;
        currentNode.Next = null;

        this.Count--;
        return element;
    }

    public T[] ToArray()
    {
        T[] array = new T[this.Count];
        int index = 0;
        Node start = this.top;

        while (start != null)
        {
            array[index] = start.Value;
            index++;

            start = start.Next;
        }

        return array;
    }
}
