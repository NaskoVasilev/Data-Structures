using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootNode
{
    public static class PrintInOrder
    {
        public static void PrintTree(Tree<int> root, int indent)
        {
            Console.WriteLine($"{new string(' ', indent)}{root.Value}");

            foreach (var child in root.Children)
            {
                PrintTree(child, indent + 2);
            }
        }
    }
}
