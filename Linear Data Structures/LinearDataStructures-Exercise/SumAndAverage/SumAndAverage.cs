using System;
using System.Collections.Generic;
using System.Linq;

namespace SumAndAverage
{
    class SumAndAverage
    {
        static void Main(string[] args)
        {
            List<int> numbers = Console.ReadLine()
                .Split()
                .Select(p=>int.Parse(p))
                .ToList();
            int sum = numbers.Sum();
            double average = (double)sum/numbers.Count;

            Console.WriteLine($"Sum={sum}; Average={average:F2}");

        }
    }
}
