﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

[TestFixture]
public class Perf11
{
    //GetByTransactionStatusAndMaximumAmount
    [TestCase]
    public void GetByTransactionStatusAndMaximumAmount_ShouldWorkFast()
    {
        IChainblock cb = new Chainblock();
        List<Transaction> txs = new List<Transaction>();
        Random rand = new Random();
        for (int i = 0; i < 100000; i++)
        {
            int amount = rand.Next(0, 1000);
            Transaction tx = new Transaction(i, TransactionStatus.Successfull,
                i.ToString(), i.ToString(), amount);
            cb.Add(tx);
            if (amount <= 500) txs.Add(tx);
        }
        txs = txs.OrderByDescending(x => x.Amount).ToList();
        int count = cb.Count;
        Assert.AreEqual(100000, count);
        Stopwatch watch = new Stopwatch();
        watch.Start();

        IEnumerable<Transaction> all = cb.GetByTransactionStatusAndMaximumAmount(
            TransactionStatus.Successfull, 500);
        int c = 0;
        foreach (Transaction tx in all)
        {
            Assert.AreSame(tx, txs[c]);
            c++;
        }

        watch.Stop();
        long l1 = watch.ElapsedMilliseconds;

        Assert.IsTrue(l1 < 150);
        Assert.AreEqual(txs.Count, c);
    }
}


