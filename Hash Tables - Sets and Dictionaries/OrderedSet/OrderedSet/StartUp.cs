using System;

namespace OrderedSet
{
    class StartUp
    {
        static void Main(string[] args)
        {
            OrderedSet<int> set = new OrderedSet<int>();

            for (int i = 1000; i >= 1; i--)
            {
                set.Add(i);
            }

            for (int i = 250; i <= 750; i++)
            {
                set.Remove(i);
            }

            foreach (var item in set)
            {
                Console.WriteLine(item);
            }
        }
    }
}
