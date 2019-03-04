using System;
using System.Collections.Generic;
using System.Linq;

public class Olympics : IOlympics
{
    private Dictionary<string, SortedSet<Competitor>> competitorsByName;
    private Dictionary<int, Competition> competitions;
    private Dictionary<int, Competitor> competitors;
    SortedSet<Competitor> sortedById = new SortedSet<Competitor>();

    public Olympics()
    {
        this.competitions = new Dictionary<int, Competition>();
        this.competitorsByName = new Dictionary<string, SortedSet<Competitor>>();
        this.competitors = new Dictionary<int, Competitor>();
        this.sortedById = new SortedSet<Competitor>();
    }

    public void AddCompetition(int id, string name, int score)
    {
        if (this.competitions.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        Competition competition = new Competition(name, id, score);
        this.competitions[id] = competition;
    }

    public void AddCompetitor(int id, string name)
    {
        if(this.competitors.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        if (!this.competitorsByName.ContainsKey(name))
        {
            this.competitorsByName.Add(name, new SortedSet<Competitor>());
        }

        Competitor competitor = new Competitor(id, name);
        this.competitorsByName[name].Add(competitor);
        this.competitors[id] = competitor;
        this.sortedById.Add(competitor);
    }

    public void Compete(int competitorId, int competitionId)
    {
        if (!this.competitors.ContainsKey(competitorId) || !this.competitions.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }

        Competition competition = this.competitions[competitionId];
        Competitor competitor = this.competitors[competitorId];
        competitor.TotalScore += competition.Score;
        competition.Competitors.Add(competitor);
    }

    public int CompetitionsCount()
    {
        return this.competitions.Count();
    }

    public int CompetitorsCount()
    {
        return this.competitors.Count();
    }

    public bool Contains(int competitionId, Competitor comp)
    {
        if (!this.competitions.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }

        return this.competitions[competitionId].Competitors.Contains(comp);
    }

    public void Disqualify(int competitionId, int competitorId)
    {
        if (!this.competitors.ContainsKey(competitorId) || !this.competitions.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }

        Competition competition = this.competitions[competitionId];
        Competitor competitor = this.competitors[competitorId];

        if (!competition.Competitors.Contains(competitor))
        {
            throw new ArgumentException();
        }

        competitor.TotalScore -= competition.Score;
        competition.Competitors.Remove(competitor);
    }

    public IEnumerable<Competitor> FindCompetitorsInRange(long min, long max)
    {
        List<Competitor> result = new List<Competitor>();

        foreach (var competitor in this.sortedById)
        {
            if(competitor.TotalScore > min && competitor.TotalScore <= max)
            {
                result.Add(competitor);
            }
        }

        return result;
    }

    public IEnumerable<Competitor> GetByName(string name)
    {
        if(!this.competitorsByName.ContainsKey(name) || this.competitorsByName[name].Count == 0)
        {
            throw new ArgumentException();
        }

        return this.competitorsByName[name];
    }

    public Competition GetCompetition(int id)
    {
        if (!this.competitions.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        return this.competitions[id];
    }

    public IEnumerable<Competitor> SearchWithNameLength(int min, int max)
    {
        List<Competitor> result = new List<Competitor>();

        foreach (var competitor in this.sortedById)
        {
            if (competitor.Name.Length >= min && competitor.Name.Length <= max)
            {
                result.Add(competitor);
            }
        }

        return result;
    }
}