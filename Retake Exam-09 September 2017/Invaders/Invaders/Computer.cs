using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Computer : IComputer
{
    private int energy;
    private LinkedList<Invader> byInsertion;
    private OrderedBag<LinkedListNode<Invader>> byPriority;

    public Computer(int energy)
    {
        if (energy < 0)
        {
            throw new ArgumentException();
        }

        this.Energy = energy;
        this.byInsertion = new LinkedList<Invader>();
        this.byPriority = new OrderedBag<LinkedListNode<Invader>>(new InvaderComparator());
    }

    public int Energy
    {
        get
        {
            if (this.energy < 0)
            {
                return 0;
            }
            return this.energy;
        }
        private set
        {
            this.energy = value;
        }
    }

    public void Skip(int turns)
    {
        List<LinkedListNode<Invader>> invdersToRemove = new List<LinkedListNode<Invader>>();

        foreach (var node in this.byPriority)
        {
            node.Value.Distance -= turns;
            if (node.Value.Distance <= 0)
            {
                invdersToRemove.Add(node);
                this.Energy -= node.Value.Damage;
            }
        }

        DestroyInvaders(invdersToRemove);
    }

    public void AddInvader(Invader invader)
    {
        LinkedListNode<Invader> node = new LinkedListNode<Invader>(invader);
        this.byInsertion.AddLast(node);
        this.byPriority.Add(node);
    }

    public void DestroyHighestPriorityTargets(int count)
    {
        List<LinkedListNode<Invader>> invadersToRemove = this.byPriority.Take(count).ToList();
        DestroyInvaders(invadersToRemove);
    }

    public void DestroyTargetsInRadius(int radius)
    {
        List<LinkedListNode<Invader>> invadersToRemove = new List<LinkedListNode<Invader>>();

        foreach (var node in this.byPriority)
        {
            if (node.Value.Distance > radius)
            {
                break;
            }

            invadersToRemove.Add(node);
        }

        DestroyInvaders(invadersToRemove);
    }

    public IEnumerable<Invader> Invaders()
    {
        return this.byInsertion;
    }

    private void DestroyInvaders(List<LinkedListNode<Invader>> invdersToRemove)
    {
        foreach (var node in invdersToRemove)
        {
            this.byPriority.Remove(node);
            this.byInsertion.Remove(node);
        }
    }

    private class InvaderComparator : IComparer<LinkedListNode<Invader>>
    {
        public int Compare(LinkedListNode<Invader> x, LinkedListNode<Invader> y)
        {
            int compare = x.Value.Distance.CompareTo(y.Value.Distance);
            if (compare == 0)
            {
                compare = y.Value.Damage.CompareTo(x.Value.Damage);
            }
            return compare;
        }
    }
}