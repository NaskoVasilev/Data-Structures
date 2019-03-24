﻿using NUnit.Framework;
using System;

public class Test28
{
    [TestCase]
    public void ExtendDeadline_Should_Work_Correct()
    {
        //Arrange

        var agency = new Agency();
        var invoice = new Invoice("123", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2000, 10, 28));
        var invoice2 = new Invoice("435", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2000, 10, 28));
        var invoice3 = new Invoice("444", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 09, 28));
        var invoice4 = new Invoice("test3", "VMWare", 1200, Department.Sells, new DateTime(2000, 10, 28), new DateTime(2001, 11, 20));
        var invoice5 = new Invoice("test", "Musala", 1200, Department.Sells, new DateTime(2000, 05, 28), new DateTime(2001, 11, 20));


        //Act

        agency.Create(invoice);
        agency.Create(invoice2);
        agency.Create(invoice3);
        agency.Create(invoice4);
        agency.Create(invoice5);
        var expectedDate = new DateTime(2001, 11, 25);
        agency.ExtendDeadline(new DateTime(2001, 11, 20), 5);

        Assert.AreEqual(expectedDate, invoice4.DueDate);
        Assert.AreEqual(expectedDate, invoice5.DueDate);
    }
}
