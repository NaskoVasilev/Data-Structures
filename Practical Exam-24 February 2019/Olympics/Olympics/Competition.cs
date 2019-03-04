using System.Collections.Generic;

public class Competition
{
    public Competition(string name, int id, int score)
    {
        this.Name = name;
        this.Id = id;
        this.Score = score;
        this.Competitors = new HashSet<Competitor>();
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public int Score { get; set; }

    public ICollection<Competitor> Competitors { get; set; }
}
