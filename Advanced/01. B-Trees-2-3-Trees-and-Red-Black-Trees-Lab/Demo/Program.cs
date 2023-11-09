using _01.Two_Three;
using System;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            var tree = new TwoThreeTree<string>();

            string[] arr = { "F", "C", "G", "A", "B", "D", "E", "K", "I", "G", "H", "J", "K" };
            for (int i = 0; i < 13; i++)
            {
                tree.Insert(arr[i]);
            }

            Console.WriteLine(tree.ToString());
        }
    }
}
