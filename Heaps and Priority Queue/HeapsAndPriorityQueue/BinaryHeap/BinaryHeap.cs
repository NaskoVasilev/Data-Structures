using System;
using System.Collections.Generic;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        heap = new List<T>();
    }

    public int Count
    {
        get
        {
            return heap.Count;
        }
    }

    public void Insert(T item)
    {
        heap.Add(item);
        HeapifyUp(this.heap.Count - 1);
    }

    private void HeapifyUp(int index)
    {
        int childIndex = index;
        int parentIndex = 0;
        int compare = 0;

        while (childIndex > 0)
        {
            parentIndex = (childIndex - 1) / 2;
            compare = this.heap[childIndex].CompareTo(this.heap[parentIndex]);

            if (compare > 0)
            {
                this.Swap(childIndex, parentIndex);
            }
            else
            {
                break;
            }

            childIndex = parentIndex;
        }
    }

    private void Swap(int childIndex, int parentIndex)
    {
        T temp = this.heap[childIndex];
        this.heap[childIndex] = this.heap[parentIndex];
        this.heap[parentIndex] = temp;
    }

    public T Peek()
    {
        return this.heap[0];
    }

    public T Pull()
    {
        if (heap.Count == 0)
        {
            throw new InvalidOperationException("Heap is empty!");
        }

        T element = this.heap[0];
        this.Swap(0, heap.Count - 1);
        this.heap.RemoveAt(this.heap.Count - 1);
        HeapifyDown(0);
        return element;
    }

    private void HeapifyDown(int parentIndex)
    {
        while (parentIndex < this.heap.Count / 2)
        {
            int childIndex = GetChildIndex(parentIndex);
            int compare = this.heap[parentIndex].CompareTo(this.heap[childIndex]);

            if (compare < 0)
            {
                this.Swap(childIndex, parentIndex);
            }
            else
            {
                break;
            }

            parentIndex = childIndex;
        }
    }

    private int GetChildIndex(int parentIndex)
    {
        int childIndex = parentIndex * 2 + 1;

        if (childIndex + 1 < this.heap.Count
            && this.heap[childIndex].CompareTo(this.heap[childIndex + 1]) < 0)
        {
            childIndex++;
        }

        return childIndex;
    }

}
