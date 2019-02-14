using System;
using System.Collections.Generic;
using Classes;
using Interfaces;
using Wintellect.PowerCollections;
using System.Linq;

public class PitFortressCollection : IPitFortress
{
    private int minionId;
    private int mineId;
    private Dictionary<string, Player> playersByName;
    private SortedSet<Player> sortedPlayers;
    private OrderedDictionary<int, SortedSet<Minion>> minions;
    private SortedSet<Mine> mines;

    public PitFortressCollection()
    {
        this.playersByName = new Dictionary<string, Player>();
        this.sortedPlayers = new SortedSet<Player>();
        this.minions = new OrderedDictionary<int, SortedSet<Minion>>();
        this.mines = new SortedSet<Mine>();
        this.mineId = 1;
        this.minionId = 1;
    }

    public int PlayersCount => this.playersByName.Count;

    public int MinionsCount => this.minions.SelectMany(x => x.Value).Count();

    public int MinesCount => mines.Count;

    public void AddPlayer(string name, int mineRadius)
    {
        if (this.playersByName.ContainsKey(name) || mineRadius < 0)
        {
            throw new ArgumentException();
        }

        Player player = new Player(name, mineRadius);
        this.playersByName.Add(name, player);
        this.sortedPlayers.Add(player);
    }

    public void AddMinion(int xCoordinate)
    {
        ValidateXCoordinate(xCoordinate);

        Minion minion = new Minion(minionId++, xCoordinate);
        if (!this.minions.ContainsKey(xCoordinate))
        {
            this.minions.Add(xCoordinate, new SortedSet<Minion>());
        }
        this.minions[xCoordinate].Add(minion);
    }

    public void SetMine(string playerName, int xCoordinate, int delay, int damage)
    {
        if (!this.playersByName.TryGetValue(playerName, out Player player))
        {
            throw new ArgumentException();
        }

        ValidateXCoordinate(xCoordinate);

        if ((delay < 0 || delay > 10000) || (damage < 0 || damage > 100))
        {
            throw new ArgumentException();
        }

        Mine mine = new Mine(mineId++, delay, damage, xCoordinate, player);
        this.mines.Add(mine);
    }

    public IEnumerable<Minion> ReportMinions()
    {
        return this.minions.SelectMany(x => x.Value);
    }

    public IEnumerable<Player> Top3PlayersByScore()
    {
        ValidateThereAreAtLeastThreePlayers();
        return this.sortedPlayers.Reverse().Take(3);
    }

    public IEnumerable<Player> Min3PlayersByScore()
    {
        ValidateThereAreAtLeastThreePlayers();
        return this.sortedPlayers.Take(3);
    }

    public IEnumerable<Mine> GetMines()
    {
        return this.mines;
    }

    public void PlayTurn()
    {
        List<Mine> minesToDetonate = GetMinesToDetonate();

        foreach (var mine in minesToDetonate)
        {
            List<Minion> minionsToAttack = GetMinionsToAttack(mine);
            AttackMinions(mine, minionsToAttack);
            this.mines.Remove(mine);
        }
    }

    private void AttackMinions(Mine mine, List<Minion> minionsToAttack)
    {
        Player player = mine.Player;

        foreach (var minion in minionsToAttack)
        {
            minion.Health -= mine.Damage;

            if (minion.Health <= 0)
            {
                this.minions[minion.XCoordinate].Remove(minion);
                this.sortedPlayers.Remove(player);
                player.Score++;
                this.sortedPlayers.Add(player);
            }
        }
    }

    private List<Minion> GetMinionsToAttack(Mine mine)
    {
        Player player = mine.Player;
        int start = mine.XCoordinate - player.Radius;
        int end = mine.XCoordinate + player.Radius;

        List<Minion> minionsToAttack = this.minions
            .Range(start, true, end, true)
            .SelectMany(x => x.Value)
            .ToList();
        return minionsToAttack;
    }

    private List<Mine> GetMinesToDetonate()
    {
        List<Mine> minesToDetonate = new List<Mine>();
        foreach (var mine in this.mines)
        {
            mine.Delay--;
            if (mine.Delay <= 0)
            {
                minesToDetonate.Add(mine);
            }
        }
        return minesToDetonate;
    }

    private void ValidateXCoordinate(int xCoordinate)
    {
        if (xCoordinate < 0 || xCoordinate > 1000000)
        {
            throw new ArgumentException();
        }
    }

    private void ValidateThereAreAtLeastThreePlayers()
    {
        if (this.PlayersCount < 3)
        {
            throw new ArgumentException();
        }
    }
}