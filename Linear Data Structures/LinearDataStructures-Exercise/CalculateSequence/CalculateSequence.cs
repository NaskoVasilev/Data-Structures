using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateSequence
{
    class CalculateSequence
    {
        static void Main(string[] args)
        {
            Queue<int> queue = new Queue<int>();
            int startNumber = int.Parse(Console.ReadLine());
            List<int> numbers = new List<int>() { startNumber };
            queue.Enqueue(startNumber);

            while (numbers.Count < 49)
            {
                int currentElement = queue.Dequeue();

                queue.Enqueue(currentElement + 1);
                queue.Enqueue(currentElement * 2 + 1);
                queue.Enqueue(currentElement + 2);

                numbers.Add(currentElement + 1);
                numbers.Add(currentElement * 2 + 1);
                numbers.Add(currentElement + 2);
            }

            numbers.Add(queue.Dequeue() + 1);

            Console.WriteLine(string.Join(", ", numbers));
        }
    }
}
