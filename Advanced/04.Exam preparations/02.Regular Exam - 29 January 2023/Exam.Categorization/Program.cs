using System;

namespace Exam.Categorization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var categorizator = new Categorizator();

            categorizator.AddCategory(new Category("1", "Test", "test"));
            categorizator.AddCategory(new Category("2", "Test1", "test"));
            categorizator.AddCategory(new Category("3", "Test2", "test"));

            categorizator.AssignParent("2", "1");
            categorizator.RemoveCategory("1");
            Console.WriteLine("");
        }
    }
}
