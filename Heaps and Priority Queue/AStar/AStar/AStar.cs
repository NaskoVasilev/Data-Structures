using System;
using System.Collections.Generic;

public class AStar
{
    private char[,] map;
    private const char WallSign = 'W';
    //GCost is the distance between the current point and the start point
    private Dictionary<Node, int> gCost;
    //Parent point is the point where the node comes from
    private Dictionary<Node, Node> parents;
    private PriorityQueue<Node> priorityQueue;

    public AStar(char[,] map)
    {
        this.map = map;
        gCost = new Dictionary<Node, int>();
        parents = new Dictionary<Node, Node>();
        priorityQueue = new PriorityQueue<Node>();
    }

    public static int GetH(Node current, Node goal)
    {
        //HCost is the distance between the current point and the goal point
        int deltaY = Math.Abs(current.Row - goal.Row);
        int deltaX = Math.Abs(current.Col - goal.Col);

        return deltaX + deltaY;
    }

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {
        priorityQueue.Clear();

        priorityQueue.Enqueue(start);
        gCost[start] = 0;
        parents[start] = null;

        while (priorityQueue.Count > 0)
        {
            //Get node with the lowest FCost
            Node current = priorityQueue.Dequeue();

            if (current.Equals(goal))
            {
                break;
            }

            List<Node> neighbourNodes = this.GetNeighbourNodes(current);
            int newCost = gCost[current] + 1;

            foreach (var neighbourNode in neighbourNodes)
            {
                if (!gCost.ContainsKey(neighbourNode) || newCost < gCost[neighbourNode])
                {
                    neighbourNode.F = newCost + GetH(neighbourNode, goal);
                    gCost[neighbourNode] = newCost;
                    parents[neighbourNode] = current;
                    priorityQueue.Enqueue(neighbourNode);
                }
            }
        }

        IEnumerable<Node> path = this.ReconstructPath(parents, start, goal);
        return path;
    }

    private IEnumerable<Node> ReconstructPath(Dictionary<Node, Node> parents, Node start, Node goal)
    {
        Stack<Node> path = new Stack<Node>();

        if (!parents.ContainsKey(goal))
        {
            path.Push(start);
            return path;
        }

        Node current = goal;

        while (!current.Equals(start))
        {
            path.Push(current);
            current = parents[current];
        }

        path.Push(start);
        return path;
    }

    private List<Node> GetNeighbourNodes(Node current)
    {
        int col = current.Col;
        int row = current.Row;

        int colLeft = col - 1;
        int colRight = col + 1;
        int rowUp = row - 1;
        int rowDown = row + 1;

        List<Node> neighbourNodes = new List<Node>();
        AddToNeighbourNodes(neighbourNodes, row, colLeft);
        AddToNeighbourNodes(neighbourNodes, row, colRight);
        AddToNeighbourNodes(neighbourNodes, rowUp, col);
        AddToNeighbourNodes(neighbourNodes, rowDown, col);

        return neighbourNodes;
    }

    private void AddToNeighbourNodes(List<Node> neighbourNodes, int row, int col)
    {
        if (this.IsInside(row, col) && !this.IsWall(row, col))
        {
            neighbourNodes.Add(new Node(row, col));
        }
    }

    bool IsWall(int row, int col)
    {
        return this.map[row, col] == WallSign;
    }

    bool IsInside(int row, int col)
    {
        return row >= 0 && row < this.map.GetLength(0)
            && col >= 0 && col < this.map.GetLength(1);
    }
}

