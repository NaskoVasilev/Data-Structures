﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

[TestFixture]
public class PerfTests03
{
    [Test]
    public void GetAtIndex_With100000Elements_ShouldWorkFast()
    {
        // Arrange
        IOrganization org = new Organization();
        const int count = 100_000;
        List<Person> people = new List<Person>();

        for (int i = 0; i < count; i++)
        {
            people.Add(new Person(i.ToString(), i));
            org.Add(people[i]);
        }

        // Act & Assert
        Stopwatch sw = Stopwatch.StartNew();
        Random rand = new Random();
        for (int i = 0; i < 50_000; i++)
        {
            int rnd = rand.Next(0, count - 1);
            Assert.AreEqual(people[rnd], org.GetAtIndex(rnd));
        }

        sw.Stop();
        Assert.Less(sw.ElapsedMilliseconds, 150);
    }
}