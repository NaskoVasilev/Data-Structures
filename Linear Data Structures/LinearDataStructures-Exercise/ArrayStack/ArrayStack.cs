using System;
using System.Collections.Generic;
using System.Linq;

public class ArrayStack<T>
{
    private T[] elements;
    public int Count { get; private set; }
    private const int InitialCapacity = 16;

    public ArrayStack(int capacity = InitialCapacity)
    {
        this.Count = 0;
        this.elements = new T[capacity];
    }

    public void Push(T element)
    {
        if(this.elements.Length == this.Count)
        {
            this.Grow();
        }

        this.elements[this.Count] = element;
        this.Count++;
    }

    public T Pop()
    {
        if(this.Count == 0)
        {
            throw new InvalidOperationException("Stack is empty!");
        }

        T element = this.elements[this.Count - 1];
        this.elements[this.Count - 1] = default(T);
        this.Count--;

        if (this.Count * 4 < this.elements.Length)
        {
            this.Shrink();
        }

        return element;
    }

    public T[] ToArray()
    {
        LinkedList<T> list = new LinkedList<T>();

        for (int i = 0; i < this.Count; i++)
        {
            list.AddFirst(this.elements[i]);
        }

        return list.ToArray();
    }

    private void Shrink()
    {
        T[] newArray = new T[this.elements.Length / 2];
        Array.Copy(this.elements, newArray, this.Count);
        this.elements = newArray;
    }

    private void Grow()
    {
        T[] newArray = new T[this.elements.Length * 2];
        Array.Copy(this.elements, newArray, this.Count);
        this.elements = newArray;
    }
}
