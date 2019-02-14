using System.Collections.Generic;
using System.Linq;

public class PersonCollectionSlow : IPersonCollection
{
    private List<Person> people;

    public PersonCollectionSlow()
    {
        this.people = new List<Person>();
    }

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (people.Any(x => x.Email == email))
        {
            return false;
        }

        Person person = new Person(email, name, age, town);
        people.Add(person);
        return true;
    }

    public int Count => this.people.Count;

    public Person FindPerson(string email)
    {
        return people.FirstOrDefault(x => x.Email == email);
    }

    public bool DeletePerson(string email)
    {
        Person person = this.FindPerson(email);
        return people.Remove(person);
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        return this.people
            .Where(p => p.Email.EndsWith('@' + emailDomain))
            .OrderBy(p => p.Email);
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        return this.people
            .Where(p => p.Name == name && p.Town == town)
            .OrderBy(p => p.Email);
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        return this.people
             .Where(p => p.Age >= startAge && p.Age <= endAge)
             .OrderBy(p => p.Age)
             .ThenBy(p => p.Email);
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
    {
        return people
            .Where(p => p.Age >= startAge && p.Age <= endAge && p.Town == town)
            .OrderBy(p => p.Age)
            .ThenBy(p => p.Email);
    }
}