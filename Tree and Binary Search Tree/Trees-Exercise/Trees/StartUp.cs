using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootNode
{
    class StartUp
    {
        public static Dictionary<int, Tree<int>> nodeByValue;

        static void Main(string[] args)
        {
            nodeByValue = new Dictionary<int, Tree<int>>();
            int numberOfNodes = int.Parse(Console.ReadLine());

            for (int i = 1; i < numberOfNodes; i++)
            {
                string[] data = Console.ReadLine().Split();
                int parentValue = int.Parse(data[0]);
                int childValue = int.Parse(data[1]);
                Utilities.AddEdge(parentValue, childValue);
            }

            Tree<int> rootNode = Utilities.GetRootNode();

            //Problem 2. Print Tree
            //PrintInOrder.PrintTree(rootNode, 0);

            //Problem 3. Leaf Nodes
            //LeafNodes.PrintLeafNodes(rootNode);

            //Problem 4. Middle Nodes
            //MiddleNodes.PrintMiddleLeaves();

            //Problem 5. Deepset Node
            //DeepestNode.FindDeepestNode(rootNode);

            //Problem 6. Longest Path
            //LongestPath.PrintLongestPath(rootNode);

            //Problem 7. All Paths With a Given Sum
            //int targetSum = int.Parse(Console.ReadLine());
            //Console.WriteLine($"Paths of sum {targetSum}:");
            //PathsWithGivenSum.PrintPathsWithGivenSum(rootNode, targetSum);
            //PathsWithGivenSum.PrintPaths(rootNode,targetSum);

            //Problem 8. * All Subtrees With a Given Sum
            int targetSum = int.Parse(Console.ReadLine());
            Console.WriteLine($"Subtrees of sum {targetSum}:");
            //SubtreesWithGivenSum.FindAllSubtrees(rootNode, targetSum);
            SubtreesWithGivenSum.SubtreeSumDFS(rootNode, targetSum, 0);
        }
    }
}
