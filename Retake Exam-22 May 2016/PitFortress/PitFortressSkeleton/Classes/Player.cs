namespace Classes
{
    using Interfaces;

    public class Player : IPlayer
    {
        public Player(string name, int radius)
        {
            Name = name;
            Radius = radius;
            Score = 0;
        }

        public int CompareTo(Player other)
        {
            int compare = this.Score.CompareTo(other.Score);
            if (compare == 0)
            {
                compare = this.Name.CompareTo(other.Name);
            }
            return compare;
        }

        public string Name { get; private set; }

        public int Radius { get; private set; }

        public int Score { get; set; }
    }
}
