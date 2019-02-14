using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> values = new List<int>();

        AVL<int> tree = new AVL<int>();
        tree.Insert(1);
        tree.Insert(3);
        tree.Insert(2);
        tree.Insert(12);
        tree.Insert(20);
        tree.Insert(0);
        tree.EachInOrder(values.Add);
        Console.WriteLine(string.Join(" ", values));

        tree.Delete(12);
        values.Clear();
        tree.EachInOrder(values.Add);

        Console.WriteLine(string.Join(" " , values));
        Console.WriteLine();
    }
}
