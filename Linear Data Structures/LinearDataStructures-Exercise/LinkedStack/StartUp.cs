using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class StartUp
    {
        static void Main(string[] args)
        {
            LinkedStack<int> stack = new LinkedStack<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Count);
            Console.WriteLine(string.Join(" ", stack.ToArray()));
        }
    }
