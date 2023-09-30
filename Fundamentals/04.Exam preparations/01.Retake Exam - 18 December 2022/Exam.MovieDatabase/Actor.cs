namespace Exam.MovieDatabase
{
    using System.Collections.Generic;

    public class Actor
    {
        public Actor(string id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public SortedSet<Movie> Movies { get; set; } 
            = new SortedSet<Movie>(Comparer<Movie>.Create((a, b) => a.Budget.CompareTo(b.Budget)));

    }
}
