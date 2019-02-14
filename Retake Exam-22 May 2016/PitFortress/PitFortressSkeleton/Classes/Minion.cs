namespace Classes
{
    using Interfaces;

    public class Minion : IMinion
    {
        private const int DefaultHealth = 100;

        public Minion(int id, int xCoordinate)
        {
            Id = id;
            XCoordinate = xCoordinate;
            Health = DefaultHealth;
        }

        public int CompareTo(Minion other)
        {
            int compare = this.XCoordinate.CompareTo(other.XCoordinate);
            if (compare == 0)
            {
                compare = this.Id.CompareTo(other.Id);
            }
            return compare;
        }

        public int Id { get; private set; }

        public int XCoordinate { get; private set; }

        public int Health { get; set; }
    }
}
