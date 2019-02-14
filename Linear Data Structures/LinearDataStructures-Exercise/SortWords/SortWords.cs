using System;
using System.Collections.Generic;
using System.Linq;

namespace SortWords
{
    class SortWords
    {
        static void Main(string[] args)
        {
            List<string> words = Console.ReadLine()
                .Split(' ')
                .OrderBy(x => x)
                .ToList();

            Console.WriteLine(string.Join(" ",words));
        }
    }
}
