using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootNode
{
    public static class DeepestNode
    {
        private static Tree<int> deepestNode;
        private static int maxDepth = -1;

        public static void FindDeepestNode(Tree<int> root)
        {
            FindDeepestNode(root, 0);
            Console.WriteLine($"Deepest node: {deepestNode.Value}");
        }

        public static Tree<int> GetDeepestNode(Tree<int> root)
        {
            FindDeepestNode(root, 0);
            return deepestNode;
        }

        public static void FindDeepestNode(Tree<int> node,  int depth)
        {
            if (depth > maxDepth)
            {
                maxDepth = depth;
                deepestNode = node;
            }

            foreach (var child in node.Children)
            {
                FindDeepestNode(child, depth + 1);
            }
        }
    }
}
