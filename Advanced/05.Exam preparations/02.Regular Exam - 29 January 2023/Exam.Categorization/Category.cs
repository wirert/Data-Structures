namespace Exam.Categorization
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class Category
    {
        public Category(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            Children = new HashSet<Category>();
            MaxDepth = 0;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Category Parent { get; set; }

        public HashSet<Category> Children { get; set; }

        public int MaxDepth { get; set; }        
    }
}
