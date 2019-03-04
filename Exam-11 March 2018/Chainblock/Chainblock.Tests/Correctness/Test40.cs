﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
public class Test40
{

    //GetBySenderOrderedByAmount
    [TestCase]
    public void GetBySenderOrderedByAmountDescending_ShouldWorkCorrectly_OnExistingSender()
    {
        //Arrange
        IChainblock cb = new Chainblock();
        Transaction tx1 = new Transaction(2, TransactionStatus.Successfull, "valq", "pesho", 14.8);
        Transaction tx2 = new Transaction(1, TransactionStatus.Successfull, "valq", "pesho", 14.8);
        Transaction tx3 = new Transaction(4, TransactionStatus.Successfull, "valq", "pesho", 15.6);
        Transaction tx4 = new Transaction(3, TransactionStatus.Successfull, "valq", "pesho", 15.6);
        Transaction tx5 = new Transaction(8, TransactionStatus.Failed, "valq", "pesho", 17.8);
        List<Transaction> expected = new List<Transaction>()
        {
           tx5, tx3, tx4, tx1, tx2
        };
        //Act
        cb.Add(tx1);
        cb.Add(tx3);
        cb.Add(tx2);
        cb.Add(tx4);
        cb.Add(tx5);
        //Assert
        List<Transaction> actual = cb.GetBySenderOrderedByAmountDescending("valq").ToList();
        CollectionAssert.AreEqual(expected, actual);
    }
}

