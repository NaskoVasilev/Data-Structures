namespace Classes
{
    using Interfaces;

    public class Mine : IMine
    {
        public Mine(int id, int delay, int damage, int xCoordinate, Player player)
        {
            Id = id;
            Delay = delay;
            Damage = damage;
            XCoordinate = xCoordinate;
            this.Player = player;
        }

        public int CompareTo(Mine other)
        {
            int compare = this.Delay.CompareTo(other.Delay);
            if (compare == 0)
            {
                compare = this.Id.CompareTo(other.Id);
            }
            return compare;
        }

        public int Id { get; private set; }

        public int Delay { get; set; }

        public int Damage { get; private set; }

        public int XCoordinate { get; private set; }

        public Player Player { get; private set; }
    }
}
