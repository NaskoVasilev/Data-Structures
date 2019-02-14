using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class StartUp
{
    static void Main(string[] args)
    {
        ReversedList<int> reversed = new ReversedList<int>();

        reversed.Add(1);
        reversed.Add(2);
        reversed.Add(3);
        reversed.Add(4);
        reversed.Add(5);

        Console.WriteLine(reversed.RemoveAt(1));

        foreach (var item in reversed)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();
        Console.WriteLine(reversed[1]);
        reversed[1] = 5;
        Console.WriteLine(reversed[1]);
    }
}
