using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Chainblock : IChainblock
{
    private Dictionary<int, LinkedListNode<Transaction>> byId;
    private Dictionary<TransactionStatus, OrderedDictionary<double, LinkedList<Transaction>>> byStatus;

    public Chainblock()
    {
        this.byId = new Dictionary<int, LinkedListNode<Transaction>>();
        this.byStatus = new Dictionary<TransactionStatus, OrderedDictionary<double, LinkedList<Transaction>>>()
        {
            {TransactionStatus.Aborted, new OrderedDictionary<double, LinkedList<Transaction>>((x, y) => y.CompareTo(x))},
            {TransactionStatus.Failed, new OrderedDictionary<double, LinkedList<Transaction>>((x, y) => y.CompareTo(x)) },
            {TransactionStatus.Successfull, new OrderedDictionary<double, LinkedList<Transaction>>((x, y) => y.CompareTo(x)) },
            {TransactionStatus.Unauthorized, new OrderedDictionary<double, LinkedList<Transaction>>((x, y) => y.CompareTo(x)) }
        };
    }

    public int Count => this.byId.Count;

    public void Add(Transaction tx)
    {
        var node = new LinkedListNode<Transaction>(tx);
        this.byId.Add(tx.Id, node);
        if (!this.byStatus[tx.Status].ContainsKey(tx.Amount))
        {
            this.byStatus[tx.Status].Add(tx.Amount, new LinkedList<Transaction>());
        }
        this.byStatus[tx.Status][tx.Amount].AddLast(node);
    }

    public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
    {
        if (!byId.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        LinkedListNode<Transaction> node = this.byId[id];
        Transaction transaction = node.Value;
        this.byStatus[transaction.Status][transaction.Amount].Remove(node);
        node.Value.Status = newStatus;

        if (!this.byStatus[newStatus].ContainsKey(transaction.Amount))
        {
            this.byStatus[newStatus].Add(transaction.Amount, new LinkedList<Transaction>());
        }
        this.byStatus[newStatus][transaction.Amount].AddLast(node);
    }

    public bool Contains(Transaction tx)
    {
        return this.byId.ContainsKey(tx.Id);
    }

    public bool Contains(int id)
    {
        return this.byId.ContainsKey(id);
    }

    public IEnumerable<Transaction> GetAllInAmountRange(double lo, double hi)
    {
        return this.byId.Values.Select(t => t.Value)
            .Where(t => t.Amount >= lo && t.Amount <= hi);
    }

    public IEnumerable<Transaction> GetAllOrderedByAmountDescendingThenById()
    {
        return this.byId.Values
            .Select(t => t.Value)
            .OrderByDescending(t => t.Amount)
            .ThenBy(t => t.Id);
    }

    public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
    {
        var result = this.GetByTransactionStatus(status).Select(x => x.To).ToList();

        if (result.Count == 0)
        {
            throw new InvalidOperationException();
        }
        return result;
    }

    public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
    {
        var result = this.GetByTransactionStatus(status).Select(x => x.From).ToList();

        if (result.Count == 0)
        {
            throw new InvalidOperationException();
        }
        return result;
    }

    public Transaction GetById(int id)
    {
        if (!this.byId.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }

        return this.byId[id].Value;
    }

    public IEnumerable<Transaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
    {
        var result = this.byId.Values.Select(t => t.Value)
            .Where(t => t.To == receiver && t.Amount >= lo && t.Amount < hi)
            .OrderByDescending(t => t.Amount)
            .ThenBy(t => t.Id)
            .ToList();

        if (result.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public IEnumerable<Transaction> GetByReceiverOrderedByAmountThenById(string receiver)
    {
        var result = this.byId.Values.Select(t => t.Value)
            .Where(t => t.To == receiver)
            .OrderByDescending(t => t.Amount)
            .ThenBy(t => t.Id)
            .ToList();

        if (result.Count == 0)
        {
            throw new InvalidOperationException();
        }
        return result;
    }

    public IEnumerable<Transaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
    {
        var result = this.byId.Values.Select(t => t.Value)
            .Where(t => t.From == sender && t.Amount > amount)
            .OrderByDescending(t => t.Amount)
            .ToList();

        if (result.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public IEnumerable<Transaction> GetBySenderOrderedByAmountDescending(string sender)
    {
        var result = this.byId.Values
           .Select(t => t.Value)
           .Where(t => t.From == sender)
           .OrderByDescending(t => t.Amount)
           .ToList();

        if (result.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public IEnumerable<Transaction> GetByTransactionStatus(TransactionStatus status)
    {
        if (this.byStatus[status].Count == 0)
        {
            throw new InvalidOperationException();
        }

        var result = this.byStatus[status].Select(x => x.Value).SelectMany(x => x);
        return result;
    }

    public IEnumerable<Transaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
    {
        foreach (var item in this.byStatus[status].RangeFrom(amount, true))
        {
            foreach (var product in item.Value)
            {
                yield return product;
            }
        }
    }

    public void RemoveTransactionById(int id)
    {
        if (!this.Contains(id))
        {
            throw new InvalidOperationException();
        }

        LinkedListNode<Transaction> node = this.byId[id];
        Transaction transaction = node.Value;
        this.byId.Remove(id);
        this.byStatus[transaction.Status][transaction.Amount].Remove(node);
    }

    public IEnumerator<Transaction> GetEnumerator()
    {
        return this.byId.Values.Select(x => x.Value).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}