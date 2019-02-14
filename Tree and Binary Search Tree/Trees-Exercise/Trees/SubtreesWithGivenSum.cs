using System;
using System.Collections.Generic;
using System.Linq;

namespace RootNode
{
    public static class SubtreesWithGivenSum
    {
        public static void FindAllSubtrees(Tree<int> node, int targtetSum)
        {
            foreach (var child in node.Children)
            {
                FindAllSubtrees(child, targtetSum);
            }

            if (GetTreeSum(node) == targtetSum)
            {
                PrintSubtree(node);
            }
        }

        private static int GetTreeSum(Tree<int> root)
        {
            return GetTreeElements(root).Sum();
        }

        private static List<int> GetTreeElements(Tree<int> root)
        {
            List<int> result = new List<int>();
            GetTreeElements(root, result);

            return result;
        }

        private static void GetTreeElements(Tree<int> node, List<int> result)
        {
            result.Add(node.Value);

            foreach (var child in node.Children)
            {
                GetTreeElements(child, result);
            }
        }

        private static void PrintSubtree(Tree<int> root)
        {
            List<int> elements = GetTreeElements(root);
            Console.WriteLine(string.Join(" ", elements));
        }

        public static int SubtreeSumDFS(Tree<int> node, int targetSum, int currentSum)
        {
            currentSum = node.Value;

            foreach (var child in node.Children)
            {
                currentSum += SubtreeSumDFS(child, targetSum, currentSum);
            }

            if (currentSum == targetSum)
            {
                PrintSubtree(node);
            }

            return currentSum;
        }
    }
}


