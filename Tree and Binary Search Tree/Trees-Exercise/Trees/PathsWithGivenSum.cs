using System;
using System.Collections.Generic;

namespace RootNode
{
    public static class PathsWithGivenSum
    {
        public static void PrintPathsWithGivenSum(Tree<int> root, int targetSum)
        {
            GetPathSum(root, targetSum);
        }

        private static void GetPathSum(Tree<int> node, int targetSum, int currentSum = 0)
        {
            currentSum += node.Value;

            if (currentSum == targetSum)
            {
                PrintPath(node);
            }

            foreach (var child in node.Children)
            {
                GetPathSum(child, targetSum, currentSum);
            }
        }

        private static void PrintPath(Tree<int> deepestNode)
        {
            Tree<int> current = deepestNode;
            List<int> elements = new List<int>();

            while (current != null)
            {
                elements.Add(current.Value);
                current = current.Parent;
            }

            elements.Reverse();
            Console.WriteLine(string.Join(" ", elements));
        }

        private static void GetAllLeaves(List<Tree<int>> result, Tree<int> node)
        {
            if (node.Children.Count == 0)
            {
                result.Add(node);
            }

            foreach (var child in node.Children)
            {
                GetAllLeaves(result, child);
            }
        }

        public static void PrintPaths(Tree<int> root, int targetSum)
        {
            List<Tree<int>> leaves = new List<Tree<int>>();
            GetAllLeaves(leaves, root);

            foreach (var leaf in leaves)
            {
                if (GetPathSum(leaf) == targetSum)
                {
                    PrintPath(leaf);
                }
            }
        }

        private static int GetPathSum(Tree<int> leaf)
        {
            Tree<int> current = leaf;
            int sum = 0;

            while (current != null)
            {
                sum += current.Value;
                current = current.Parent;
            }

            return sum;
        }
    }
}
