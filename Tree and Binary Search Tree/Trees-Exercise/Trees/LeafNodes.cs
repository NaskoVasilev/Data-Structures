using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootNode
{
    public static class LeafNodes
    {
        public static void PrintLeafNodes(Tree<int> root)
        {
            List<int> leaves = FindAllLeaves(root);
            Console.WriteLine("Leaf nodes: " + string.Join(" ", leaves));
        }


        public static List<int> FindLeaves(Tree<int> root)
        {
            List<int> result = new List<int>();
            Queue<Tree<int>> queue = new Queue<Tree<int>>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Tree<int> currentTree = queue.Dequeue();

                foreach (var child in currentTree.Children)
                {
                    queue.Enqueue(child);
                }

                if (!currentTree.Children.Any())
                {
                    result.Add(currentTree.Value);
                }
            }

            return result.OrderBy(x => x).ToList();
        }

        public static List<int> FindAllLeaves(Tree<int> root)
        {
            return StartUp.nodeByValue.Values.Where(x => x.Children.Count == 0)
                .Select(x => x.Value)
                .OrderBy(x => x)
                .ToList();
        }
    }
}
