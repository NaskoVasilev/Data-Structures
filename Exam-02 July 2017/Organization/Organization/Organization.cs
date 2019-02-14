using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Organization : IOrganization
{
    private List<Person> byInsertion;
    private Dictionary<string, List<Person>> byName;

    public Organization()
    {
        this.byInsertion = new List<Person>();
        this.byName = new Dictionary<string, List<Person>>();
    }

    public int Count
    {
        get
        {
            return this.byName.Count;
        }
    }
    public bool Contains(Person person)
    {
        if (!this.byName.ContainsKey(person.Name))
        {
            return false;
        }

        return this.byName[person.Name].Contains(person);
    }

    public bool ContainsByName(string name)
    {
        if (!this.byName.ContainsKey(name))
        {
            return false;
        }

        return this.byName[name].Count > 0;
    }

    public void Add(Person person)
    {
        this.byInsertion.Add(person);

        if (!this.byName.ContainsKey(person.Name))
        {
            this.byName.Add(person.Name, new List<Person>());
        }
        this.byName[person.Name].Add(person);
    }

    public Person GetAtIndex(int index)
    {
        if (this.byInsertion.Count <= index || index < 0)
        {
            throw new IndexOutOfRangeException();
        }

        return this.byInsertion[index];
    }

    public IEnumerable<Person> GetByName(string name)
    {
        if (!this.byName.ContainsKey(name))
        {
            return Enumerable.Empty<Person>();
        }

        return this.byName[name];
    }

    public IEnumerable<Person> FirstByInsertOrder(int count = 1)
    {
        for (int i = 0; i < Math.Min(byInsertion.Count, count); i++)
        {
            yield return this.byInsertion[i];
        }
    }

    public IEnumerable<Person> SearchWithNameSize(int minLength, int maxLength)
    {
        return this.byInsertion.Where(x => x.Name.Length >= minLength && x.Name.Length <= maxLength);
    }

    public IEnumerable<Person> GetWithNameSize(int length)
    {
        IEnumerable<Person> result = this.byInsertion.Where(x => x.Name.Length == length);

        if (!result.Any())
        {
            throw new ArgumentException();
        }

        return result;
    }

    public IEnumerable<Person> PeopleByInsertOrder()
    {
        return this.byInsertion;
    }

    public IEnumerator<Person> GetEnumerator()
    {
        return this.byInsertion.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}