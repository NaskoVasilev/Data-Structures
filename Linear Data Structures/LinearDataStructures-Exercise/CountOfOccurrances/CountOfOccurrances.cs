using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountOfOccurrances
{
    class CountOfOccurrances
    {
        static void Main(string[] args)
        {
            Dictionary<int, int> occurrances = new Dictionary<int, int>();

            List<int> numbers = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToList();

            foreach (var element in numbers)
            {
                if (!occurrances.ContainsKey(element))
                {
                    occurrances.Add(element, 0);
                }
                occurrances[element]++;
            }

            foreach (var pair in occurrances.OrderBy(x => x.Key))
            {
                Console.WriteLine($"{pair.Key} -> {pair.Value} times");
            }
        }
    }
}
