﻿
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
public class Test17
{

    [TestCase]
    public void CB_ShouldReturn_TransactionsInCorrectOrder_AfterDelete()
    {
        //Arrange
        IChainblock cb = new Chainblock();
        Transaction tx1 = new Transaction(5, TransactionStatus.Successfull, "joro", "pesho", 5);
        Transaction tx2 = new Transaction(6, TransactionStatus.Successfull, "joro", "pesho", 5);
        Transaction tx3 = new Transaction(7, TransactionStatus.Successfull, "joro", "pesho", 5);
        List<Transaction> expected = new List<Transaction>()
        {
            tx2
        };
        //Act
        cb.Add(tx1);
        cb.Add(tx3);
        cb.Add(tx2);
        cb.RemoveTransactionById(5);
        cb.RemoveTransactionById(7);
        List<Transaction> actual = cb.Take(cb.Count).ToList();
        //Assert
        CollectionAssert.AreEqual(expected, actual);
    }
}

