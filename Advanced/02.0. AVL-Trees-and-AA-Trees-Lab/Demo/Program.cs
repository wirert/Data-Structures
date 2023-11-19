using System;
using AA_Tree;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new AATree<int>();

            tree.Insert(4);
            tree.Insert(13);
            tree.Insert(2);
            tree.Insert(6);
            tree.Insert(16);
            tree.Insert(9);
            tree.Insert(3);
            tree.Insert(1);
            tree.Insert(17);
            tree.Insert(5);
            tree.Insert(7);
            tree.Insert(11);
            tree.Insert(14);


            tree.Delete(1);

            Console.WriteLine("done");
        }
    }
}
