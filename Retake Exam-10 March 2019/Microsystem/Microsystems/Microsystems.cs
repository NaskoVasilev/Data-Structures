using System;
using System.Collections.Generic;
using System.Linq;

public class Microsystems : IMicrosystem
{
    private Dictionary<int, Computer> byNumber;
    private Dictionary<Brand, HashSet<Computer>> byBrand;

    public Microsystems()
    {
        this.byNumber = new Dictionary<int, Computer>();
        this.byBrand = new Dictionary<Brand, HashSet<Computer>>()
        {
            { Brand.ACER, new HashSet<Computer>() },
            { Brand.ASUS, new HashSet<Computer>() },
            { Brand.DELL, new HashSet<Computer>() },
            { Brand.HP, new HashSet<Computer>() },
        };
    }

    public bool Contains(int number)
    {
        return this.byNumber.ContainsKey(number);
    }

    public int Count()
    {
        return this.byNumber.Count;
    }

    public void CreateComputer(Computer computer)
    {
        if (this.Contains(computer.Number))
        {
            throw new ArgumentException();
        }

        this.byNumber.Add(computer.Number, computer);
        this.byBrand[computer.Brand].Add(computer);
    }

    public IEnumerable<Computer> GetAllFromBrand(Brand brand)
    {
        if (!this.byBrand.ContainsKey(brand) || !this.byBrand[brand].Any())
        {
            return new List<Computer>();
        }

        return this.byBrand[brand]
            .OrderByDescending(c => c.Price).ToList();
    }

    public IEnumerable<Computer> GetAllWithColor(string color)
    {
        return this.byNumber.Values
            .Where(c => c.Color == color)
            .OrderByDescending(c => c.Price)
            .ToList();
    }

    public IEnumerable<Computer> GetAllWithScreenSize(double screenSize)
    {
        return this.byNumber.Values
            .Where(c => c.ScreenSize == screenSize)
            .OrderByDescending(c => c.Number)
            .ToArray();
    }

    public Computer GetComputer(int number)
    {
        if (!this.Contains(number))
        {
            throw new ArgumentException();
        }
        return this.byNumber[number];
    }

    public IEnumerable<Computer> GetInRangePrice(double minPrice, double maxPrice)
    {
        return this.byNumber.Values
            .Where(c => c.Price >= minPrice && c.Price <= maxPrice)
            .OrderByDescending(x => x.Price)
            .ToList();
    }

    public void Remove(int number)
    {
        if (!this.Contains(number))
        {
            throw new ArgumentException();
        }

        Computer computer = this.byNumber[number];
        this.byNumber.Remove(number);
        this.byBrand[computer.Brand].Remove(computer);
    }

    public void RemoveWithBrand(Brand brand)
    {
        if(!this.byBrand[brand].Any())
        {
            throw new ArgumentException();
        }

        var computers = this.byBrand[brand];

        foreach (var computer in computers)
        {
            this.byNumber.Remove(computer.Number);
        }

        this.byBrand[brand].Clear();
    }

    public void UpgradeRam(int ram, int number)
    {
        if (!this.Contains(number))
        {
            throw new ArgumentException();
        }

        var computer = this.byNumber[number];

        if(ram > computer.RAM)
        {
            computer.RAM = ram;
        }
    }
}
