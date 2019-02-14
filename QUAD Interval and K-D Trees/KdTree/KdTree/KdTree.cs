using System;

public class KdTree
{
    private Node root;

    public class Node
    {
        public Node(Point2D point)
        {
            this.Point = point;
        }

        public Point2D Point { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public Node Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(Point2D point)
    {
        Node node = GetNode(this.root, point.X, point.Y, 0);
        return node != null;
    }

    public void Insert(Point2D point)
    {
        this.root = this.Insert(this.root, point, 0);   
    }
    
    public void EachInOrder(Action<Point2D> action)
    {
        this.EachInOrder(this.root, action);
    }

    private Node Insert(Node node, Point2D point, int depth)
    {
        if(node == null)
        {
            return new Node(point);
        }

        int compare = 0;
        if(depth % 2 == 0)
        {
            compare = node.Point.X.CompareTo(point.X);
        }
        else
        {
            compare = node.Point.Y.CompareTo(point.Y);
        }

        if (compare > 0)
        {
            node.Left = this.Insert(node.Left, point, depth + 1);
        }
        else
        {
            node.Right = this.Insert(node.Right, point, depth + 1);
        }

        return node;
    }

    private void EachInOrder(Node node, Action<Point2D> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Point);
        this.EachInOrder(node.Right, action);
    }

    private Node GetNode(Node node, double x, double y, int depth)
    {
        if (node == null)
        {
            return null;
        }

        int compare = depth % 2 == 0 ? node.Point.X.CompareTo(x) : node.Point.Y.CompareTo(y);

        if (compare > 0)
        {
            return GetNode(node.Left, x, y, depth + 1);
        }
        else if(compare > 0)
        {
            return GetNode(node.Right, x, y, depth + 1);
        }

        return node;
    }
}
