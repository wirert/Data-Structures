namespace _02.FitGym
{
    using System.Collections.Generic;

    public class Trainer
    {
        public Trainer(int id, string name, int popularity)
        {
            this.Id = id;
            this.Name = name;
            this.Popularity = popularity;
            Trainees = new HashSet<Member>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Popularity { get; set; }

        public HashSet<Member> Trainees { get; set; }
    }
}