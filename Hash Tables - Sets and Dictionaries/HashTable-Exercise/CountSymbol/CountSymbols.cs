using System;
using System.Linq;

public class CountSymbols
{
    static void Main(string[] args)
    {
        string input = Console.ReadLine();
        HashTable<char, int> symbolsByCount = new HashTable<char, int>();

        foreach (var character in input)
        {
            if (!symbolsByCount.ContainsKey(character))
            {
                symbolsByCount.Add(character, 0);
            }
            symbolsByCount[character]++;
        }

        foreach (KeyValue<char, int> kvp in symbolsByCount.OrderBy(x => (int)x.Key))
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value} time/s");
        }
    }
}
