namespace GalaxyMap
{
    public class Point2D
    {
        public Point2D(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return string.Format("({0}, {1})", this.X, this.Y);
        }

        public bool IsInRectangle(Rectangle rectangle)
        {
            return rectangle.X1 <= this.X && rectangle.X2 >= this.X 
                && rectangle.Y1 <= this.Y && rectangle.Y2 >= this.Y;
        }
    }
}
