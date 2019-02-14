using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestSubsequence
{
    class LongestSubsequence
    {
        static void Main(string[] args)
        {
            List<int> input = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();

            List<int> longestSequence = FindLongestSubsequence(input);

            Console.WriteLine(string.Join(" ",longestSequence));

        }

        private static List<int> FindLongestSubsequence(List<int> numbers)
        {
            int maxLength = 0;
            int bestNumber = 0;
            int currentLength = 1;

            for (int i = 0; i < numbers.Count -1; i++)
            {
                if(numbers[i]==numbers[i+1])
                {
                    currentLength++;

                    if (currentLength > maxLength)
                    {
                        maxLength = currentLength;
                        bestNumber = numbers[i];
                    }
                }
                else
                {
                    currentLength = 1;
                }
            }

            List<int> sequence = new List<int>();

            for (int i = 0; i < maxLength; i++)
            {
                sequence.Add(bestNumber);
            }

            return sequence;
        }
    }
}
