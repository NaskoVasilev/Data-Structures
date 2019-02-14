using System;

namespace SweepAndProne
{
    public class GameObject : IComparable
    {
        private const int Height = 10;
        private const int Width = 10;

        public GameObject(string name, int x1, int y1)
        {
            Name = name;
            X1 = x1;
            Y1 = y1;
        }

        public string Name { get;private set; }

        public int X1 { get; set; }

        public int Y1 { get; set; }

        public int X2 => this.X1 + Width;

        public int Y2 => this.Y1 + Height;

        public int CompareTo(object obj)
        {
            GameObject gameObject = (GameObject)obj;
            return this.X1.CompareTo(gameObject.X1);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
