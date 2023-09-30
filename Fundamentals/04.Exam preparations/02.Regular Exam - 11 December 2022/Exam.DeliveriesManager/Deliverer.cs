namespace Exam.DeliveriesManager
{
    using System.Collections.Generic;

    public class Deliverer
    {
        public Deliverer(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public HashSet<Package> Packages { get; set; } = new HashSet<Package>();
    }
}
