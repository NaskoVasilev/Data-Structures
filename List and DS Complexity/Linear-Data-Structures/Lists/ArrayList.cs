using System;
using System.Collections;
using System.Collections.Generic;

public class ArrayList<T>:IEnumerable<T>
{
    public int Count { get; private set; }
    public int Capacity { get; set; }
    private T[] array;

    public ArrayList(int capacity = 2)
    {
        this.Capacity = capacity;
        this.Count = 0;
        this.array = new T[capacity];
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException("Index was not in array bound!");
            }
            return this.array[index];
        }

        set
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException("Index was not in array bound!");
            }
            this.array[index] = value;
        }
    }

    public void Add(T item)
    {
        if (this.Count == this.Capacity)
        {
            this.Grow();
        }
        this.array[this.Count] = item;
        this.Count++;
    }

    public T RemoveAt(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            throw new ArgumentOutOfRangeException("Index is not in array bound!");
        }
        T removedElement = this[index];
        this[index] = default(T);
        this.ShiftLeft(index);
        if (this.Capacity / 4 > this.Count - 1)
        {
            this.Shrink();
        }
        this.Count--;
        return removedElement;
    }

    private void Grow()
    {
        T[] newArray = new T[this.Capacity * 2];
        this.Capacity *= 2;
        this.array.CopyTo(newArray, 0);
        this.array = newArray;
    }

    private void ShiftLeft(int index)
    {
        for (int i = index; i < this.Count - 1; i++)
        {
            array[i] = array[i + 1];
        }
    }

    private void Shrink()
    {
        T[] newArray = new T[this.Capacity / 2];
        this.Capacity /= 2;
        this.array.CopyTo(newArray,0);
        this.array = newArray;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < this.Count; i++)
        {
            yield return array[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
