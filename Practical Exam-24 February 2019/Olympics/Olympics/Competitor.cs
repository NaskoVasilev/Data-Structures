using System;

public class Competitor : IComparable
{
    public Competitor(int id, string name)
    {
        this.Id = id;
        this.Name = name;
        this.TotalScore = 0;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public long TotalScore { get; set; }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return ((Competitor)obj).Id == this.Id;
    }

    public int CompareTo(object obj)
    {
        return this.Id.CompareTo(((Competitor)obj).Id);
    }
}
