using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OddOcurrances
{
    class OddOcurrances
    {
        static void Main(string[] args)
        {
            List<int> numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();

            List<int> targetNumbers = new List<int>();

            RemoveOddOccurances(numbers, targetNumbers);

            Console.WriteLine(string.Join(" ", targetNumbers));
        }

        private static void RemoveOddOccurances(List<int> numbers, List<int> targetNumbers)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                int numberCount = numbers.Count(x => x == numbers[i]);

                if (numberCount % 2 == 0)
                {
                    targetNumbers.Add(numbers[i]);
                }
            }
        }
    }
}
