namespace _05.ReverseNumbers
{
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            var stack = new Stack<int>();
            for (int i = 0; i < n; i++)
            {
                stack.Push(int.Parse(Console.ReadLine()));
            }

            Console.WriteLine(string.Join(" ", stack));
        }
    }
}
