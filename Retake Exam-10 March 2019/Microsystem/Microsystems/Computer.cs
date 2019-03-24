using System;

public class Computer : IComparable<Computer>
{
    public Computer(int number, Brand brand, double price, double screenSize, string color)
    {
        this.Number = number;
        this.RAM = 8;
        this.Brand = brand;
        this.Price = price;
        this.ScreenSize = screenSize;
        this.Color = color;
    }
    public int Number { get; set; }

    public int RAM { get; set; }

    public Brand Brand { get; set; }

    public double Price { get; set; }

    public double ScreenSize { get; set; }

    public string Color { get; set; }

    public int CompareTo(Computer other)
    {
        return other.Price.CompareTo(this.Price);
    }

    public override int GetHashCode()
    {
        return this.Number.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return ((Computer)obj).Number.Equals(this.Number);
    }
}
