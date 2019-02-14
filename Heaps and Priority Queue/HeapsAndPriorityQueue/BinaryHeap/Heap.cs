using System;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        ConstructHeap(arr);
        SortArray(arr);
    }

    private static void SortArray(T[] arr)
    {
        for (int i = arr.Length - 1; i >= 0; i--)
        {
            Swap(0, i, arr);
            HeapifyDown(0, arr, i);
        }
    }

    private static void ConstructHeap(T[] arr)
    {
        for (int i = arr.Length / 2 - 1; i >= 0; i--)
        {
            HeapifyDown(i, arr, arr.Length);
        }
    }

    private static void HeapifyDown(int parentIndex, T[] arr, int arrayLength)
    {
        while (parentIndex < arrayLength / 2)
        {
            int childIndex = GetChildIndex(parentIndex, arr, arrayLength);
            int compare = arr[parentIndex].CompareTo(arr[childIndex]);

            if (compare < 0)
            {
                Swap(childIndex, parentIndex, arr);
            }
            else
            {
                break;
            }

            parentIndex = childIndex;
        }
    }

    private static void Swap(int childIndex, int parentIndex, T[] arr)
    {
        T temp = arr[childIndex];
        arr[childIndex] = arr[parentIndex];
        arr[parentIndex] = temp;
    }

    private static int GetChildIndex(int parentIndex, T[] arr, int arrayLength)
    {
        int childIndex = parentIndex * 2 + 1;

        if (childIndex + 1 < arrayLength
            && arr[childIndex].CompareTo(arr[childIndex + 1]) < 0)
        {
            childIndex++;
        }

        return childIndex;
    }
}
