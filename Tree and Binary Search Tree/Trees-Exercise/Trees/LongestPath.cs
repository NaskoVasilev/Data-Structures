using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootNode
{
    public static class LongestPath
    {
        public static List<int> FindLongestPath(Tree<int> root)
        {
            Tree<int> current = DeepestNode.GetDeepestNode(root);
            List<int> elements = new List<int>();

            while (current != null)
            {
                elements.Add(current.Value);
                current = current.Parent;
            }

            elements.Reverse();

            return elements;
        }

        public static void PrintLongestPath(Tree<int> root)
        {
            List<int> elements = FindLongestPath(root);

            Console.WriteLine($"Longest path: {string.Join(" ", elements)}");
        }

    }
}
