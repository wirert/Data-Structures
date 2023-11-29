namespace CollectionOfPeople
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class PeopleCollection : IPeopleCollection
    {
        private Dictionary<string, Person> peopleByEmail = new Dictionary<string, Person>();
        private Dictionary<string, SortedSet<Person>> peopleByDomain = new Dictionary<string, SortedSet<Person>>();
        private Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> peopleByTownAndAge = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();

        public int Count => peopleByEmail.Count;

        public bool Add(string email, string name, int age, string town)
        {
            if (peopleByEmail.ContainsKey(email))
            {
                return false;
            }

            var person = new Person(email, name, age, town);
            peopleByEmail.Add(email, person);
            peopleByDomain.AppendValueToKey(email.Split('@')[1], person);

            peopleByTownAndAge.EnsureKeyExists(town);
            peopleByTownAndAge[town].AppendValueToKey(age, person);

            return true;
        }

        public bool Delete(string email)
        {
            if (!peopleByEmail.ContainsKey(email))
            {
                return false;
            }

            var person = peopleByEmail[email];
            peopleByEmail.Remove(email);
            peopleByDomain[email.Split('@')[1]].Remove(person);
            peopleByTownAndAge[person.Town][person.Age].Remove(person);

            return true;
        }

        public Person Find(string email)
            => !peopleByEmail.ContainsKey(email) ? null : peopleByEmail[email];

        public IEnumerable<Person> FindPeople(string emailDomain)
            => !peopleByDomain.ContainsKey(emailDomain) 
                ? Enumerable.Empty<Person>() 
                : peopleByDomain[emailDomain];    

        public IEnumerable<Person> FindPeople(string name, string town)
        {
            if (string.IsNullOrEmpty(town) || !peopleByTownAndAge.ContainsKey(town))
            {
                return Enumerable.Empty<Person>();
            }

            return peopleByTownAndAge[town].Values
            .SelectMany(ss => ss.Where(p => p.Name == name));
        }
            
        public IEnumerable<Person> FindPeople(int startAge, int endAge)
            => peopleByTownAndAge.Values
                .SelectMany(p => p.Range(startAge, true, endAge, true))
                .SelectMany(p => p.Value)
            .OrderBy(p => p.Age)
            .ThenBy(p => p.Email);

        public IEnumerable<Person> FindPeople(int startAge, int endAge, string town)
        {
            if (string.IsNullOrEmpty(town) || !peopleByTownAndAge.ContainsKey(town))
            {
                return Enumerable.Empty<Person>();
            }

            return peopleByTownAndAge[town]
                .Range(startAge, true, endAge, true).Values
                .SelectMany(p => p);
        }
    }
}
