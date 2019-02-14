using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootNode
{
    public static class MiddleNodes
    {
        public static List<int> FindMiddleNodes()
        {
            return StartUp.nodeByValue
                .Values.Where(n => n.Parent != null && n.Children.Count > 0)
                .Select(n => n.Value)
                .OrderBy(n => n)
                .ToList();
        }

        public static void PrintMiddleLeaves()
        {
            List<int> middleLeaves = FindMiddleNodes();
            Console.WriteLine("Middle nodes: " + string.Join(" ", middleLeaves));
        }
    }
}
