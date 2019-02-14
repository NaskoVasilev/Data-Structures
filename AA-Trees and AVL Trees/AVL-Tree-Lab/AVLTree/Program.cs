using System;

class Program
{
    static void Main(string[] args)
    {
        AVL<int> tree = new AVL<int>();
        tree.Insert(5);
        tree.Insert(3);
        tree.Insert(7);
        tree.Insert(1);

        tree.EachInOrder(x => Console.WriteLine(x));
        Console.WriteLine(tree.Contains(1));
    }
}
