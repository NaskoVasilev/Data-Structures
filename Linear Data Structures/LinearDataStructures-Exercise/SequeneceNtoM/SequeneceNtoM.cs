using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequeneceNtoM
{
    class SequeneceNtoM
    {
        static void Main(string[] args)
        {
            string[] data = Console.ReadLine().Split(' ');
            int startNumber = int.Parse(data[0]);
            int endNumber = int.Parse(data[1]);

            if (startNumber > endNumber)
            {
                return;
            }

            Queue<Item> queue = new Queue<Item>();
            queue.Enqueue(new Item(startNumber));

            while (queue.Count > 0)
            {
                Item currentItem = queue.Dequeue();
                int value = currentItem.Value;

                if (value == endNumber)
                {
                    PtintSequence(currentItem);
                    return;
                }
                else if (value > endNumber)
                {
                    continue;
                }

                queue.Enqueue(new Item(value + 1, currentItem));
                queue.Enqueue(new Item(value + 2, currentItem));
                queue.Enqueue(new Item(value * 2, currentItem));
            }
        }

        private static void PtintSequence(Item start)
        {
            Stack<int> sequence = new Stack<int>();
            Item current = start;

            while (current != null)
            {
                sequence.Push(current.Value);
                current = current.Previous;
            }

            Console.WriteLine(string.Join(" -> ", sequence));
        }

        private class Item
        {
            public int Value { get; set; }

            public Item Previous { get; set; }

            public Item(int value, Item previous = null)
            {
                Value = value;
                Previous = previous;
            }

        }
    }
}
