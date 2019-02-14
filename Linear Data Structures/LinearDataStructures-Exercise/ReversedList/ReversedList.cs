using System;
using System.Collections;
using System.Collections.Generic;

class ReversedList<T> : IEnumerable<T>
{
    private T[] elements;
    public int Count { get; private set; }
    public int Capacity { get; private set; }
    private const int InitialCapacity = 2;

    public ReversedList(int capacity = InitialCapacity)
    {
        this.Capacity = capacity;
        this.Count = 0;
        this.elements = new T[this.Capacity];
    }

    public T this[int index]
    {
        get
        {
            ThrowIndexOutOfRangeException(index);

            int targetIndex = this.Count - 1 - index;
            return this.elements[targetIndex];
        }

        set
        {
            ThrowIndexOutOfRangeException(index);

            int targetIndex = this.Count - 1 - index;
            this.elements[targetIndex] = value;
        }
    }

    public void Add(T element)
    {
        if (this.Count == this.Capacity)
        {
            this.Grow();
        }

        this.elements[this.Count] = element;
        this.Count++;
    }

    public T RemoveAt(int index)
    {
        ThrowIndexOutOfRangeException(index);

        int targetIndex = this.Count - 1 - index;
        T element = this.elements[index];
        this.elements[index] = default(T);
        ShiftLeft(index);
        this.Count --;

        return element;
    }

    private void ShiftLeft(int index)
    {
        for (int i = index; i < this.Count - 1; i++)
        {
            this.elements[index] = this.elements[index + 1];
        }
    }

    private void ThrowIndexOutOfRangeException(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            throw new IndexOutOfRangeException("Index is not in list bounds!");
        }
    }

    private void Grow()
    {
        T[] newArray = new T[this.Capacity * 2];
        this.Capacity *= 2;
        Array.Copy(this.elements, newArray, this.Count);
        this.elements = newArray;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = this.Count - 1; i >= 0; i--)
        {
            yield return this.elements[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();

    }
}
