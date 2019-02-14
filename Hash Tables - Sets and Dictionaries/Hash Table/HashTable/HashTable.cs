using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{
    private const int InitialCapacity = 16;
    private const double LoadFactor = 0.75;
    private LinkedList<KeyValue<TKey, TValue>>[] slots;

    public int Count { get; private set; }

    public int Capacity => this.slots.Length;

    public HashTable(int capacity = InitialCapacity)
    {
        this.slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
        this.Count = 0;
    }

    public void Add(TKey key, TValue value)
    {
        GrowIfNeeded();
        int index = GetIndex(key);

        if (this.slots[index] == null)
        {
            this.slots[index] = new LinkedList<KeyValue<TKey, TValue>>();
        }
        else
        {
            foreach (var keyValue in this.slots[index])
            {
                if (keyValue.Key.Equals(key))
                {
                    throw new ArgumentException($"The key - {key} already exists in the table!");
                }
            }
        }

        KeyValue<TKey, TValue> kvp = new KeyValue<TKey, TValue>(key, value);
        this.slots[index].AddLast(kvp);
        this.Count++;
    }

    public bool AddOrReplace(TKey key, TValue value)
    {
        GrowIfNeeded();
        int index = GetIndex(key);
        KeyValue<TKey, TValue> kvp = null;

        if (this.slots[index] == null)
        {
            this.slots[index] = new LinkedList<KeyValue<TKey, TValue>>();
            kvp = new KeyValue<TKey, TValue>(key, value);
            this.slots[index].AddLast(kvp);
            this.Count++;
            return true;
        }
        else
        {
            foreach (var keyValue in this.slots[index])
            {
                if (keyValue.Key.Equals(key))
                {
                    kvp = keyValue;
                    break;
                }
            }
        }

        kvp.Value = value;
        return false;
    }

    public TValue Get(TKey key)
    {
        KeyValue<TKey, TValue> keyValue = this.Find(key);

        if (keyValue == null)
        {
            throw new KeyNotFoundException($"The key - {key} does not exists in the hash table!");
        }

        return keyValue.Value;
    }

    public TValue this[TKey key]
    {
        get => this.Get(key);
        set => this.AddOrReplace(key, value);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        KeyValue<TKey, TValue> keyValue = this.Find(key);

        if (keyValue == null)
        {
            value = default(TValue);
            return false;
        }

        value = keyValue.Value;
        return true;
    }

    public KeyValue<TKey, TValue> Find(TKey key)
    {
        int index = GetIndex(key);

        if (this.slots[index] != null)
        {
            foreach (var keyValue in this.slots[index])
            {
                if (keyValue.Key.Equals(key))
                {
                    return keyValue;
                }
            }
        }

        return null;
    }

    public bool ContainsKey(TKey key)
    {
        KeyValue<TKey, TValue> keyValue = this.Find(key);
        return keyValue != null;
    }

    public bool Remove(TKey key)
    {
        int index = GetIndex(key);
        LinkedList<KeyValue<TKey, TValue>> elements = this.slots[index];

        if (elements != null)
        {
            LinkedListNode<KeyValue<TKey, TValue>> currentElement = elements.First;

            while (currentElement != null)
            {
                if (currentElement.Value.Key.Equals(key))
                {
                    elements.Remove(currentElement);
                    this.Count--;
                    return true;
                }
                currentElement = currentElement.Next;
            }
        }

        return false;
    }

    public void Clear()
    {
        this.slots = new LinkedList<KeyValue<TKey, TValue>>[InitialCapacity];
        this.Count = 0;
    }

    public IEnumerable<TKey> Keys => this.Select(x => x.Key);

    public IEnumerable<TValue> Values => this.Select(x => x.Value);

    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {
        return this.slots.Where(x => x != null)
            .SelectMany(x => x)
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private int GetIndex(TKey key)
    {
        int index = Math.Abs(key.GetHashCode()) % this.slots.Length;
        return index;
    }

    private void GrowIfNeeded()
    {
        int loadFactor = (this.Count + 1) / this.Capacity;
        if (loadFactor >= LoadFactor)
        {
            HashTable<TKey, TValue> newTable = new HashTable<TKey, TValue>(this.Capacity * 2);

            foreach (var keyValue in this)
            {
                newTable.Add(keyValue.Key, keyValue.Value);
            }

            this.slots = newTable.slots;
        }
    }
}