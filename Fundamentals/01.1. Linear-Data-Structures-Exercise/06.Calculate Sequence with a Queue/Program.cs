namespace _06.Calculate_Sequence_with_a_Queue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Program
    {
        static void Main(string[] args)
        {
            var num = int.Parse(Console.ReadLine());
            var queue = new Queue<int>();

            List<int> result = new List<int>(56)
            {
                num
            };

            while (result.Count < 50)
            {
                result.Add(num + 1);
                queue.Enqueue(num +1);

                result.Add(num * 2 + 1);
                queue.Enqueue(num * 2 + 1);

                result.Add(num + 2);
                queue.Enqueue(num + 2);

                num = queue.Dequeue();
            }

            Console.WriteLine(string.Join(", ", result.Take(50)));
        }
    }
}
