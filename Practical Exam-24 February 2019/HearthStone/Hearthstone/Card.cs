using System;

public class Card : IComparable
{
    public Card(string name, int damage, int score, int level)
    {
        this.Name = name;
        this.Damage = damage;
        this.Score = score;
        this.Level = level;
        this.Health = 20;
    }
    public string Name { get; set; }

    public int Damage { get; set; }

    public int Score { get; set; }

    public int Health { get; set; }

    public int Level { get; set; }

    public int CompareTo(object obj)
    {
        Card card = (Card)obj;

        int compare = card.Level.CompareTo(this.Level);

        if(compare == 0)
        {
            return this.Name.CompareTo(card.Name);
        }

        return compare;
    }

    public override bool Equals(object obj)
    {
        return this.Name.Equals(((Card)obj).Name);
    }

    public override int GetHashCode()
    {
        return this.Name.GetHashCode();
    }
}