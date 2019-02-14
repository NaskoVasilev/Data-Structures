using System;
using System.Collections.Generic;

namespace GalaxyMap
{
    public class KdTree
    {
        private Node root;

        public KdTree()
        {
            this.root = null;
        }

        public void GetPoints(Action<Point2D> action, Rectangle rectangle, Rectangle space, int depth = 0)
        {
            this.EachInOrder(this.root, action, rectangle, space, depth);
        }

        public void BuildFromList(List<Point2D> systems)
        {
            this.root = this.Build(systems);
        }

        private Node Build(List<Point2D> systems, int depth = 0)
        {
            if (systems.Count == 0)
            {
                return null;
            }

            systems.Sort((point, other) =>
            {
                if (depth % 2 == 0)
                {
                    return point.X.CompareTo(other.X);
                }
                return point.Y.CompareTo(other.Y);
            });

            int medianIndex = systems.Count / 2;
            List<Point2D> left = new List<Point2D>(medianIndex);
            List<Point2D> right = new List<Point2D>(medianIndex + 1);

            for (int i = 0; i < medianIndex; i++)
            {
                left.Add(systems[i]);
            }

            for (int i = medianIndex + 1; i < systems.Count; i++)
            {
                right.Add(systems[i]);
            }

            Node node = new Node(systems[medianIndex]);
            node.Left = Build(left, depth + 1);
            node.Right = Build(right, depth + 1);
            return node;
        }


        private void EachInOrder(Node node, Action<Point2D> action, Rectangle rectangle, Rectangle space, int depth)
        {
            if (node == null)
            {
                return;
            }

            if (node.Point.IsInRectangle(rectangle))
            {
                action(node.Point);
            }

            Rectangle leftRect;
            Rectangle rightRect;

            if (depth % 2 == 0)
            {
                leftRect = new Rectangle(space.X1, node.Point.X, space.Y1, space.Y2);
                rightRect = new Rectangle(node.Point.X, space.X2, space.Y1, space.Y2);
            }
            else
            {
                leftRect = new Rectangle(space.X1, space.X2, space.Y1, node.Point.Y);
                rightRect = new Rectangle(space.X1, space.X2, node.Point.Y, space.Y2);
            }

            if (rectangle.Intersects(leftRect))
            {
                this.EachInOrder(node.Left, action, rectangle, leftRect, depth + 1);
            }

            if (rectangle.Intersects(rightRect))
            {
                this.EachInOrder(node.Right, action, rectangle, rightRect, depth + 1);
            }
        }

        private class Node
        {
            public Node(Point2D point)
            {
                this.Point = point;
            }

            public Point2D Point { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }
    }
}