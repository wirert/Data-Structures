namespace CollectionOfPeople
{
    using System.Collections.Generic;
    using System.Linq;

    public class PeopleCollectionSlow : IPeopleCollection
    {
        private List<Person> people = new List<Person>();
        public int Count => people.Count;

        public bool Add(string email, string name, int age, string town)
        {
            if (people.Any(p => p.Email == email))
            {
                return false;
            }
            people.Add(new Person(email, name, age, town));

            return true;
        }

        public bool Delete(string email)
        {
            var person = Find(email);

            if (person == null)
            {
                return false;
            }

            people.Remove(person);
            return true;
        }

        public Person Find(string email)
            => people.FirstOrDefault(p => p.Email == email);

        public IEnumerable<Person> FindPeople(string emailDomain)
            => people
            .Where(p => p.Email.Split('@')[1] == emailDomain)
            .OrderBy(p => p.Email);

        public IEnumerable<Person> FindPeople(string name, string town)
            => people
            .Where(p => p.Name == name && p.Town == town)
            .OrderBy(p => p.Email);

        public IEnumerable<Person> FindPeople(int startAge, int endAge)
            => people.Where(p => (p.Age >= startAge) && (p.Age <= endAge))
            .OrderBy(p => p.Age)
            .ThenBy(p => p.Email);

        public IEnumerable<Person> FindPeople(int startAge, int endAge, string town)
            => FindPeople(startAge, endAge).Where(p => p.Town == town);
    }
}
