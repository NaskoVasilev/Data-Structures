using System;

public class Program
{
    static void Main(string[] args)
    {
        HashTable<string, string> phonebook = new HashTable<string, string>();
        string input = "";

        while ((input = Console.ReadLine()) != "search")
        {
            string[] tokens = input.Split('-');
            if(tokens.Length > 1)
            {
                string firstName = tokens[0];
                string phone = tokens[1];
                if (!phonebook.ContainsKey(firstName))
                {
                    phonebook.Add(firstName, phone);
                }
            }
        }

        string name = "";
        while ((name = Console.ReadLine()) != "end")
        {
            if (phonebook.ContainsKey(name))
            {
                Console.WriteLine($"{name} -> {phonebook[name]}");
            }
            else
            {
                Console.WriteLine($"Contact {name} does not exist.");
            }
        }
    }
}
