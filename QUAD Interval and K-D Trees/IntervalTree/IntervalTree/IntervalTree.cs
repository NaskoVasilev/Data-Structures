using System;
using System.Collections.Generic;

public class IntervalTree
{
    private class Node
    {
        internal Interval interval;
        internal double max;
        internal Node right;
        internal Node left;

        public Node(Interval interval)
        {
            this.interval = interval;
            this.max = interval.Hi;
        }
    }

    private Node root;

    public void Insert(double lo, double hi)
    {
        this.root = this.Insert(this.root, lo, hi);
    }

    public void EachInOrder(Action<Interval> action)
    {
        EachInOrder(this.root, action);
    }

    public Interval SearchAny(double lo, double hi)
    {
        Node current = this.root;

        while (current != null && !current.interval.Intersects(lo, hi))
        {
            if (current.left != null && current.left.max > lo)
            {
                current = current.left;
            }
            else
            {
                current = current.right;
            }
        }

        return current?.interval;
    }

    public IEnumerable<Interval> SearchAll(double lo, double hi)
    {
        List<Interval> intervals = new List<Interval>();
        this.SearchAll(root, intervals, lo, hi);

        return intervals;
    }

    private void SearchAll(Node node, List<Interval> intervals, double lo, double hi)
    {
        if(node == null)
        {
            return;
        }

        if(node.left != null && node.left.max > lo)
        {
            SearchAll(node.left, intervals, lo, hi);
        }

        if(node.interval.Intersects(lo, hi))
        {
            intervals.Add(node.interval);
        }

        if(node.right != null && node.right.interval.Lo < hi)
        {
            SearchAll(node.right, intervals, lo, hi);
        }
    }

    private void EachInOrder(Node node, Action<Interval> action)
    {
        if (node == null)
        {
            return;
        }

        EachInOrder(node.left, action);
        action(node.interval);
        EachInOrder(node.right, action);
    }

    private Node Insert(Node node, double lo, double hi)
    {
        if (node == null)
        {
            return new Node(new Interval(lo, hi));
        }

        int cmp = lo.CompareTo(node.interval.Lo);
        if (cmp < 0)
        {
            node.left = Insert(node.left, lo, hi);
        }
        else if (cmp > 0)
        {
            node.right = Insert(node.right, lo, hi);
        }

        UpdateMaxEndpoint(node);
        return node;
    }

    private void UpdateMaxEndpoint(Node node)
    {
        double maxEndpoint = Math.Max(GetEndpoint(node.left), GetEndpoint(node.right));
        node.max = Math.Max(node.max, maxEndpoint);
    }

    private double GetEndpoint(Node node)
    {
        if (node == null)
        {
            return Double.MinValue;
        }

        return node.max;
    }
}
