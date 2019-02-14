using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootNode
{
    public static class Utilities
    {
        public static void AddEdge(int parentValue, int childValue)
        {
            Tree<int> parentNode = GetNodeByValue(parentValue);
            Tree<int> childNode = GetNodeByValue(childValue);

            parentNode.Children.Add(childNode);
            childNode.Parent = parentNode;
        }

        public static Tree<int> GetNodeByValue(int value)
        {
            if (!StartUp.nodeByValue.ContainsKey(value))
            {
                StartUp.nodeByValue.Add(value, new Tree<int>(value));
            }

            return StartUp.nodeByValue[value];
        }

        public static Tree<int> GetRootNode()
        {
            return StartUp.nodeByValue.Values.FirstOrDefault(x => x.Parent == null);
        }
    }
}
