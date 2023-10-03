namespace VaccOps.Models
{
    using System.Collections.Generic;

    public class Doctor
    {
        public Doctor(string name, int popularity)
        {
            this.Name = name;
            this.Popularity = popularity;
        }

        public string Name { get; set; }
        public int Popularity { get; set; }

        public HashSet<Patient> Patients { get; set; } = new HashSet<Patient>();
    }
}
