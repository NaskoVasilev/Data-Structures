using System;
using System.Collections.Generic;
using System.Linq;

public class Board : IBoard
{
    private Dictionary<string, Card> byName;
    private HashSet<string> deathCardsNames;

    public Board()
    {
        this.byName = new Dictionary<string, Card>();
        this.deathCardsNames = new HashSet<string>();
    }

    public bool Contains(string name)
    {
        return this.byName.ContainsKey(name);
    }

    public int Count()
    {
        return this.byName.Count;
    }

    public void Draw(Card card)
    {
        if (this.byName.ContainsKey(card.Name))
        {
            throw new ArgumentException();
        }

        this.byName[card.Name] = card;
    }

    public IEnumerable<Card> GetBestInRange(int start, int end)
    {
        return this.byName.Values
            .Where(c => c.Score >= start && c.Score <= end)
            .OrderByDescending(c => c.Level)
            .ToList();
    }

    public void Heal(int health)
    {
        Card card = this.byName.Values.OrderBy(c => c.Health).FirstOrDefault();
        card.Health += health;
    }

    public IEnumerable<Card> ListCardsByPrefix(string prefix)
    {
        if (string.IsNullOrEmpty(prefix))
        {
            return Enumerable.Empty<Card>();
        }

        return this.byName.Values
            .Where(c => c.Name.StartsWith(prefix))
            .OrderBy(c => ReverseString(c.Name))
            .ThenBy(c => c.Level)
            .ToList();
    }

    public void Play(string attackerCardName, string attackedCardName)
    {
        if (!this.byName.ContainsKey(attackedCardName) || !this.byName.ContainsKey(attackerCardName))
        {
            throw new ArgumentException();
        }

        Card attackerCard = this.byName[attackerCardName];
        Card attackedCard = this.byName[attackedCardName];

        if (attackerCard.Health <= 0 || attackedCard.Health <= 0)
        {
            return;
        }

        if (attackerCard.Level != attackedCard.Level)
        {
            throw new ArgumentException();
        }

        attackedCard.Health -= attackerCard.Damage;

        if (attackedCard.Health <= 0)
        {
            this.deathCardsNames.Add(attackedCard.Name);
            attackerCard.Score += attackedCard.Level;
        }
    }

    public void Remove(string name)
    {
        if (!this.byName.ContainsKey(name))
        {
            throw new ArgumentException();
        }
        this.byName.Remove(name);
        this.deathCardsNames.Remove(name);
    }

    public void RemoveDeath()
    {
        foreach (var name in this.deathCardsNames)
        {
            this.byName.Remove(name);
        }

        this.deathCardsNames.Clear();
    }

    public IEnumerable<Card> SearchByLevel(int level)
    {
        return this.byName.Values
            .Where(c => c.Level == level)
            .OrderByDescending(c => c.Score)
            .ToList();
    }

    private string ReverseString(string input)
    {
        string output = "";
        for (int i = input.Length - 1; i >= 0; i--)
        {
            output += input[i];
        }
        return output;
    }
}