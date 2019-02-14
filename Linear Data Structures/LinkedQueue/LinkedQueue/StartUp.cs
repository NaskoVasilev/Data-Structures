using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class StartUp
{
    static void Main(string[] args)
    {
        LinkedQueue<int> nums = new LinkedQueue<int>();

        nums.Enqueue(1);
        nums.Enqueue(2);
        nums.Enqueue(3);
        nums.Enqueue(4);

        foreach (var item in nums)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine(string.Join(" ",nums.ToArray()));

        Console.WriteLine(nums.Count);

        nums.Dequeue();
        Console.WriteLine(nums.Count);
        Console.WriteLine(nums.Dequeue());
        Console.WriteLine(nums.Dequeue());
        Console.WriteLine(nums.Dequeue());
        Console.WriteLine(nums.Count);
    }
}
