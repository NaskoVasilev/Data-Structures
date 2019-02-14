using System;
using System.Collections.Generic;

namespace GalaxyMap
{
    public class GalaxyMap
    {
        public static void Main(string[] args)
        {
            int starsCount = int.Parse(Console.ReadLine());
            int reportsCount = int.Parse(Console.ReadLine());
            int galaxySize = int.Parse(Console.ReadLine());

            Rectangle bounds = new Rectangle(0, galaxySize, 0, galaxySize);
            List<Point2D> galaxy = new List<Point2D>();

            for (int i = 0; i < starsCount; i++)
            {
                string[] data = Console.ReadLine().Split(' ');
                string starName = data[0];
                int x = int.Parse(data[1]);
                int y = int.Parse(data[2]);

                Point2D point = new Point2D(x, y);
                if (point.IsInRectangle(bounds))
                {
                    galaxy.Add(point);
                }
            }

            KdTree tree = new KdTree();
            tree.BuildFromList(galaxy);

            for (int i = 0; i < reportsCount; i++)
            {
                string[] data = Console.ReadLine().Split(' ');
                int x = int.Parse(data[1]);
                int y = int.Parse(data[2]);
                int width = int.Parse(data[3]);
                int height = int.Parse(data[4]);
                int x2 = x + width;
                int y2 = y + height;

                List<Point2D> points = new List<Point2D>();
                tree.GetPoints(points.Add, new Rectangle(x, x2, y, y2), bounds);
                Console.WriteLine(points.Count);
            }
        }
    }
}