using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
{
    private LinkedList<T> insertionOrder;
    private OrderedBag<LinkedListNode<T>> acscendingOrder;
    private OrderedBag<T> descendingOrder;

    public FirstLastList()
    {
        this.insertionOrder = new LinkedList<T>();
        this.acscendingOrder = new OrderedBag<LinkedListNode<T>>((x, y) => x.Value.CompareTo(y.Value));
        this.descendingOrder = new OrderedBag<T>((x, y) => y.CompareTo(x));
    }

    public int Count
    {
        get
        {
            return insertionOrder.Count;
        }
    }

    public void Add(T element)
    {
        LinkedListNode<T> node = new LinkedListNode<T>(element);
        insertionOrder.AddLast(node);
        acscendingOrder.Add(node);
        descendingOrder.Add(element);
    }

    public void Clear()
    {
        insertionOrder.Clear();
        acscendingOrder.Clear();
        descendingOrder.Clear();
    }

    public IEnumerable<T> First(int count)
    {
        if (this.ThereIsNotEnoughElements(count))
        {
            ThrowNotEnoughElementsException();
        }

        LinkedListNode<T> current = insertionOrder.First;

        while (count > 0)
        {
            yield return current.Value;
            current = current.Next;
            count--;
        }
    }

    public IEnumerable<T> Last(int count)
    {
        if (this.ThereIsNotEnoughElements(count))
        {
            ThrowNotEnoughElementsException();
        }

        LinkedListNode<T> current = insertionOrder.Last;

        while (count > 0)
        {
            yield return current.Value;
            current = current.Previous;
            count--;
        }
    }

    public IEnumerable<T> Max(int count)
    {

        if (this.ThereIsNotEnoughElements(count))
        {
            ThrowNotEnoughElementsException();
        }

        return descendingOrder.Take(count);
    }

    public IEnumerable<T> Min(int count)
    {
        if (this.ThereIsNotEnoughElements(count))
        {
            ThrowNotEnoughElementsException();
        }

        return acscendingOrder.Take(count).Select(x => x.Value);
    }

    public int RemoveAll(T element)
    {
        LinkedListNode<T> node = new LinkedListNode<T>(element);

        foreach (var listNode in acscendingOrder.Range(node, true, node, true))
        {
            insertionOrder.Remove(listNode);
        }

        int deletedElementsCount = acscendingOrder.RemoveAllCopies(node);
        descendingOrder.RemoveAllCopies(element);
        return deletedElementsCount;
    }

    private bool ThereIsNotEnoughElements(int count)
    {
        return count > this.Count;
    }

    private void ThrowNotEnoughElementsException()
    {
        throw new ArgumentOutOfRangeException("There is no enough element!");
    }
}
