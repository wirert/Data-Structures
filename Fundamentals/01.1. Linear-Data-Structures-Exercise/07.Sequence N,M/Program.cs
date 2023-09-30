namespace _07.Sequence_N_M
{
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var m = int.Parse(Console.ReadLine());
            var queue = new Queue<Item>();
            queue.Enqueue(new Item(n));

            while (queue.Count != 0)
            {
                var currentNum = queue.Dequeue();

                if (currentNum.Value == m)
                {
                    Print(currentNum);
                    return;
                }

                if (currentNum.Value < m)
                {
                    queue.Enqueue(new Item(currentNum.Value + 1, currentNum));
                    queue.Enqueue(new Item(currentNum.Value + 2, currentNum));
                    queue.Enqueue(new Item(currentNum.Value * 2, currentNum));
                }
            }

            Console.WriteLine("(no solution)");
        }

        private static void Print(Item currentNum)
        {
            var result = new Stack<int>();

            while (currentNum != null)
            {
                result.Push(currentNum.Value);

                currentNum = currentNum.Origin;
            }

            Console.WriteLine(string.Join("->", result));
        }
    }
}
