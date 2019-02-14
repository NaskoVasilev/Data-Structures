using System;

public class HeapExample
{
    static void Main()
    {
        //Console.WriteLine("Created an empty heap.");
        //var heap = new BinaryHeap<int>();
        //heap.Insert(5);
        //heap.Insert(8);
        //heap.Insert(1);
        //heap.Insert(3);
        //heap.Insert(12);
        //heap.Insert(-4);

        //Console.WriteLine("Heap elements (max to min):");
        //while (heap.Count > 0)
        //{
        //    var max = heap.Pull();
        //    Console.WriteLine(max);
        //}

        //heap.Insert(20);
        //heap.Insert(25);
        //heap.Insert(2);
        //heap.Insert(0);
        //heap.Insert(100);

        //Console.WriteLine("Heap elements (max to min):");
        //while (heap.Count > 0)
        //{
        //    var max = heap.Pull();
        //    Console.WriteLine(max);
        //}

        //Heap sort example

        int[] arr = new int[] { 2, 4, 1, 3, 5};

        Heap<int>.Sort(arr);

        Console.WriteLine(string.Join(" ",arr));
    }
}
