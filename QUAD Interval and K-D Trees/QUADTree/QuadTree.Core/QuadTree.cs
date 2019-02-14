using System;
using System.Collections.Generic;
using System.Linq;

public class QuadTree<T> where T : IBoundable
{
    public const int DefaultMaxDepth = 5;

    public readonly int MaxDepth;

    private Node<T> root;

    public QuadTree(int width, int height, int maxDepth = DefaultMaxDepth)
    {
        this.root = new Node<T>(0, 0, width, height);
        this.Bounds = this.root.Bounds;
        this.MaxDepth = maxDepth;
    }

    public int Count { get; private set; }

    public Rectangle Bounds { get; private set; }

    public void ForEachDfs(Action<List<T>, int, int> action)
    {
        this.ForEachDfs(this.root, action);
    }

    public bool Insert(T item)
    {
        Node<T> current = this.root;
        if (!item.Bounds.IsInside(current.Bounds))
        {
            return false;
        }

        int depth = 1;
        while (current.Children != null)
        {
            int quadrant = this.GetQuadrant(current, item.Bounds);
            if (quadrant == -1)
            {
                break;
            }
            current = current.Children[quadrant];
            depth++;
        }

        current.Items.Add(item);
        Split(current, depth);
        this.Count++;
        return true;
    }

    public List<T> Report(Rectangle bounds)
    {
        List<T> result = new List<T>();
        this.ReportCollisions(this.root, result, bounds);
        return result;
    }

    private void ReportCollisions(Node<T> node, List<T> result, Rectangle bounds)
    {
        int quadrant = this.GetQuadrant(node, bounds);

        if (quadrant == -1)
        {
            this.ForEachDfs(node, (items, depth, q) =>
            {
                foreach (var item in items)
                {
                    if (item.Bounds.Intersects(bounds))
                    {
                        result.Add(item);
                    }
                }
            });
        }
        else
        {
            this.ReportCollisions(node.Children[quadrant], result, bounds);
            foreach (var item in node.Items)
            {
                if (item.Bounds.Intersects(bounds))
                {
                    result.Add(item);
                }
            }
        }
    }

    private void ForEachDfs(Node<T> node, Action<List<T>, int, int> action, int depth = 1, int quadrant = 0)
    {
        if (node == null)
        {
            return;
        }

        if (node.Items.Any())
        {
            action(node.Items, depth, quadrant);
        }

        if (node.Children != null)
        {
            for (int i = 0; i < node.Children.Length; i++)
            {
                ForEachDfs(node.Children[i], action, depth + 1, i);
            }
        }
    }

    private void Split(Node<T> node, int depth)
    {
        if (!node.ShouldSplit || depth >= MaxDepth)
        {
            return;
        }

        int leftWidth = node.Bounds.Width / 2;
        int rightWidth = node.Bounds.Width - leftWidth;
        int topHeight = node.Bounds.Height / 2;
        int bottomHeight = node.Bounds.Height - topHeight;

        if(node.Children == null)
        {
            node.Children = new Node<T>[4];
            node.Children[0] = new Node<T>(node.Bounds.MidX, node.Bounds.Y1, rightWidth, topHeight);
            node.Children[1] = new Node<T>(node.Bounds.X1, node.Bounds.Y1, leftWidth, topHeight);
            node.Children[2] = new Node<T>(node.Bounds.X1, node.Bounds.MidY, leftWidth, bottomHeight);
            node.Children[3] = new Node<T>(node.Bounds.MidX, node.Bounds.MidY, rightWidth, bottomHeight);
        }

        for (int i = 0; i < node.Items.Count; i++)
        {
            T item = node.Items[i];
            int quadrant = GetQuadrant(node, item.Bounds);
            if (quadrant != -1)
            {
                node.Children[quadrant].Items.Add(item);
                node.Items.RemoveAt(i);
                i--;
            }
        }

        foreach (Node<T> child in node.Children)
        {
            this.Split(child, depth + 1);
        }
    }

    private int GetQuadrant(Node<T> node, Rectangle bounds)
    {
        if (node.Children == null)
        {
            return -1;
        }

        for (int i = 0; i < node.Children.Length; i++)
        {
            if (bounds.IsInside(node.Children[i].Bounds))
            {
                return i;
            }
        }

        return -1;
    }
}