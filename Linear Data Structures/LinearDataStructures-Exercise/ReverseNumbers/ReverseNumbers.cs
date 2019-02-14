using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseNumbers
{
    class ReverseNumbers
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            if(input == "(empty)")
            {
                Console.WriteLine("(empty)");
                return;
            }

            int[] numbers = input
                .Split()
                .Select(int.Parse)
                .ToArray();

            Stack<int> reversedNumbers = new Stack<int>(numbers);

            Console.WriteLine(string.Join(" ",reversedNumbers));
        }
    }
}
