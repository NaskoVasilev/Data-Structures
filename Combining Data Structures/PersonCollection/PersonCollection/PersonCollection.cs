using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    private Dictionary<string, Person> peopleByEmail;
    private Dictionary<string, SortedSet<Person>> peopleByEmailDomain;
    private Dictionary<Tuple<string, string>, SortedSet<Person>> peopleByNameAndTown;
    private OrderedDictionary<int, SortedSet<Person>> peopleByAge;
    private Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> peopleByTownAndAge;

    public PersonCollection()
    {
        this.peopleByEmail = new Dictionary<string, Person>();
        this.peopleByEmailDomain = new Dictionary<string, SortedSet<Person>>();
        this.peopleByNameAndTown = new Dictionary<Tuple<string, string>, SortedSet<Person>>();
        this.peopleByAge = new OrderedDictionary<int, SortedSet<Person>>();
        this.peopleByTownAndAge = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();
    }

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (peopleByEmail.ContainsKey(email))
        {
            return false;
        }

        Person person = new Person(email, name, age, town);
        peopleByEmail.Add(email, person);

        string emailDomain = this.ExtractEmailDomain(email);
        peopleByEmailDomain.AppendValueToKey(emailDomain, person);

        peopleByNameAndTown.AppendValueToKey(new Tuple<string, string>(name, town), person);

        peopleByAge.AppendValueToKey(age, person);

        this.peopleByTownAndAge.EnsureKeyExists(town);
        this.peopleByTownAndAge[town].AppendValueToKey(age, person);

        return true;
    }

    public int Count => this.peopleByEmail.Count;

    public Person FindPerson(string email)
    {
        this.peopleByEmail.TryGetValue(email, out Person person);
        return person;
    }

    public bool DeletePerson(string email)
    {
        Person person = this.FindPerson(email);
        if(person == null)
        {
            return false;
        }

        this.peopleByEmail.Remove(email);

        string domain = this.ExtractEmailDomain(email);
        this.peopleByEmailDomain[domain].Remove(person);

        Tuple<string, string> nameAndTown = new Tuple<string, string>(person.Name, person.Town);
        this.peopleByNameAndTown[nameAndTown].Remove(person);

        this.peopleByAge[person.Age].Remove(person);

        this.peopleByTownAndAge[person.Town][person.Age].Remove(person);

        return true;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        return this.peopleByEmailDomain.GetValuesForKey(emailDomain);
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        return this.peopleByNameAndTown.GetValuesForKey(new Tuple<string, string>(name, town));
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        bool inclusive = true;
        var targetPeopleByAge = this.peopleByAge.Range(startAge, inclusive, endAge, inclusive);
        foreach (var personByAge in targetPeopleByAge)
        {
            foreach (var person in personByAge.Value)
            {
                yield return person;
            }
        }
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
    {
        if (!peopleByTownAndAge.ContainsKey(town))
        {
            //returns empty collection
            yield break;
        }

        var targetPeopleByAge = this.peopleByTownAndAge[town].Range(startAge, true, endAge, true);

        foreach (var presonByAge in targetPeopleByAge)
        {
            foreach (var person in presonByAge.Value)
            {
                yield return person;
            }
        }
    }

    private string ExtractEmailDomain(string email)
    {
        return email.Split('@')[1];
    }
}