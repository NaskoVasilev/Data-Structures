using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class StartUp
{
    static void Main(string[] args)
    {
        ArrayStack<int> stack = new ArrayStack<int>();

        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        stack.Push(4);

        Console.WriteLine(stack.Pop());
        Console.WriteLine(stack.Pop());
        Console.WriteLine(string.Join(" ",stack.ToArray()));
        Console.WriteLine(stack.Count);
    }
}
