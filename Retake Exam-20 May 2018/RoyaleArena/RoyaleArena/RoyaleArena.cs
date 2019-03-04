using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class RoyaleArena : IArena
{
    Comparison<Battlecard> swagComparer =
        (x, y) =>
        {
            int compare = y.Swag.CompareTo(x.Swag);
            if (compare == 0)
            {
                return x.Id.CompareTo(y.Id);
            }

            return compare;
        };

    Comparison<Battlecard> swagComparerASC =
       (x, y) =>
       {
           int compare = x.Swag.CompareTo(y.Swag);

           if (compare == 0)
           {
               return x.Id.CompareTo(y.Id);
           }

           return compare;
       };

    private LinkedList<Battlecard> byInsertion = new LinkedList<Battlecard>();
    private Dictionary<int, LinkedListNode<Battlecard>> byId = new Dictionary<int, LinkedListNode<Battlecard>>();
    private Dictionary<CardType, OrderedBag<Battlecard>> byType =
        new Dictionary<CardType, OrderedBag<Battlecard>>();
    private Dictionary<string, OrderedBag<Battlecard>> byNameAndSwag =
       new Dictionary<string, OrderedBag<Battlecard>>();
    private OrderedBag<Battlecard> bySwag;

    public RoyaleArena()
    {
        this.byType.Add(CardType.MELEE, new OrderedBag<Battlecard>());
        this.byType.Add(CardType.RANGED, new OrderedBag<Battlecard>());
        this.byType.Add(CardType.SPELL, new OrderedBag<Battlecard>());
        this.byType.Add(CardType.BUILDING, new OrderedBag<Battlecard>());

        this.bySwag = new OrderedBag<Battlecard>(swagComparerASC);
    }

    public int Count => this.byInsertion.Count;

    public void Add(Battlecard card)
    {
        LinkedListNode<Battlecard> node = new LinkedListNode<Battlecard>(card);
        this.byId.Add(card.Id, node);
        this.byInsertion.AddLast(node);

        if (!this.byNameAndSwag.ContainsKey(card.Name))
        {
            this.byNameAndSwag.Add(card.Name, new OrderedBag<Battlecard>(swagComparer));
        }

        this.byNameAndSwag[card.Name].Add(card);
        this.byType[card.Type].Add(card);
        this.bySwag.Add(card);
    }

    public void ChangeCardType(int id, CardType type)
    {
        if (!this.byId.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        this.byId[id].Value.Type = type;
    }

    public bool Contains(Battlecard card)
    {
        return this.byId.ContainsKey(card.Id);
    }

    public IEnumerable<Battlecard> FindFirstLeastSwag(int n)
    {
        if (this.bySwag.Count < n)
        {
            throw new InvalidOperationException();
        }

        return this.bySwag.Take(n);
    }

    public IEnumerable<Battlecard> GetAllByNameAndSwag()
    {
        foreach (var name in this.byNameAndSwag)
        {
            if(name.Value.Count > 0)
            {
                yield return name.Value.GetFirst();
            }
        }
    }

    public IEnumerable<Battlecard> GetAllInSwagRange(double lo, double hi)
    {
        Battlecard low = new Battlecard(0, CardType.BUILDING, "lo", 0, lo);
        Battlecard hig = new Battlecard(0, CardType.BUILDING, "hig", 0, hi);

        return this.bySwag.Range(low, true, hig, true);
    }

    public IEnumerable<Battlecard> GetByCardType(CardType type)
    {
        if (this.byType[type].Count == 0)
        {
            throw new InvalidOperationException();
        }

        return this.byType[type];
    }

    public IEnumerable<Battlecard> GetByCardTypeAndMaximumDamage(CardType type, double damage)
    {
        if (this.byType[type].Count == 0)
        {
            throw new InvalidOperationException();
        }
        return this.byType[type].RangeFrom(new Battlecard(0, CardType.RANGED, "aha", damage, 15), true);
    }

    public Battlecard GetById(int id)
    {
        if (!this.byId.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }

        return this.byId[id].Value;
    }

    public IEnumerable<Battlecard> GetByNameAndSwagRange(string name, double lo, double hi)
    {
        Battlecard low = new Battlecard(0, CardType.BUILDING, "lo", 0, lo);
        Battlecard hig = new Battlecard(0, CardType.BUILDING, "hig", 0, hi);

        if (this.byNameAndSwag[name].Count == 0)
        {
            throw new InvalidOperationException();
        }

        return this.byNameAndSwag[name].Range(hig, false, low, true);
    }

    public IEnumerable<Battlecard> GetByNameOrderedBySwagDescending(string name)
    {
        if (!this.byNameAndSwag.ContainsKey(name) || this.byNameAndSwag[name].Count == 0)
        {
            throw new InvalidOperationException();
        }

        return this.byNameAndSwag[name];
    }

    public IEnumerable<Battlecard> GetByTypeAndDamageRangeOrderedByDamageThenById(CardType type, int lo, int hi)
    {
        if (this.byType[type].Count == 0)
        {
            throw new InvalidOperationException();
        }

        Battlecard low = new Battlecard(0, CardType.BUILDING, "lo", lo, 0);
        Battlecard hig = new Battlecard(0, CardType.BUILDING, "hig", hi, 0);

        return this.byType[type].Range(hig, false, low, false);
    }

    public IEnumerator<Battlecard> GetEnumerator()
    {
        return this.byInsertion.GetEnumerator();
    }

    public void RemoveById(int id)
    {
        LinkedListNode<Battlecard> toRemove = this.byId[id];
        this.byInsertion.Remove(toRemove);
        this.byId.Remove(id);
        this.bySwag.Remove(toRemove.Value);
        this.byNameAndSwag[toRemove.Value.Name].Remove(toRemove.Value);
        this.byType[toRemove.Value.Type].Remove(toRemove.Value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}