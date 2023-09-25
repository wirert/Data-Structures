namespace Demo
{
    using _01.BinaryTree;
    using _04.BinarySearchTree;
    using System;

    internal class Program
    {
        static void Main(string[] args)
        {
            var bst = new BinarySearchTree<int>();
            bst.Insert(12);
            bst.Insert(21);
            bst.Insert(5);
            bst.Insert(1);
            bst.Insert(8);
            bst.Insert(18);
            bst.Insert(23);

            Console.WriteLine(bst.Contains(5));
        }
    }
}
