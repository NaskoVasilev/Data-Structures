using System;
using System.Collections;
using System.Collections.Generic;

public class CircularQueue<T> : IEnumerable<T>
{
    private const int DefaultCapacity = 4;

    public int Count { get; private set; }
    private T[] array;
    private int startIndex = 0;
    private int endIndex = 0;

    public CircularQueue(int capacity = DefaultCapacity)
    {
        this.Count = 0;
        this.array = new T[capacity];
    }

    public void Enqueue(T element)
    {
        if (this.Count == this.array.Length)
        {
            this.Resize();
        }

        this.array[this.endIndex] = element;
        this.endIndex = (this.endIndex + 1) % this.array.Length;
        this.Count++;
    }

    private void Resize()
    {
        T[] newArray = new T[this.array.Length * 2];
        this.CopyAllElements(newArray);
        this.array = newArray;
        this.startIndex = 0;
        this.endIndex = this.Count;
    }

    private void CopyAllElements(T[] newArray)
    {
        for (int i = 0; i < this.Count; i++)
        {
            int index = (this.startIndex + i) % this.array.Length;
            newArray[i] = this.array[index];
        }
    }

    // Should throw InvalidOperationException if the queue is empty
    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("Queue is empty!");
        }

        T element = this.array[this.startIndex];
        this.startIndex = (this.startIndex + 1) % this.array.Length;
        this.Count--;
        return element;
    }

    public T[] ToArray()
    {
        T[] newArray = new T[this.Count];
        this.CopyAllElements(newArray);
        return newArray;
    }

    public IEnumerator<T> GetEnumerator()
    {
        T[] newArray = this.ToArray();

        for (int i = 0; i < newArray.Length; i++)
        {
            yield return newArray[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}


public class Example
{
    public static void Main()
    {
        CircularQueue<int> queue = new CircularQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);
        Console.WriteLine("Foreach all elements");

        foreach (var item in queue)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("------------------");
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        int first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}
