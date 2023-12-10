namespace TaskManager
{
    using System.Collections.Generic;

    public class Task
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Assignee { get; set; }

        public int Priority { get; set; }

        public HashSet<Task> Dependancies { get; set; } = new HashSet<Task>();

        public HashSet<Task> Dependants { get; set; } = new HashSet<Task>();

        public override string ToString()
        {
            return this.Title;
        }

        public override bool Equals(object obj)
        {
            return Id.Equals((obj as Task).Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}