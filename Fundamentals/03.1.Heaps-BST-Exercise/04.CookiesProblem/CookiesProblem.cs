using System;
using System.Linq;
using _03.MinHeap;
using Wintellect.PowerCollections;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int minSweetness, int[] cookies)
        {
            var cookieQueue = new MinHeap<int>();

            foreach (var cookie in cookies)
            {
                cookieQueue.Add(cookie);
            }

            int count = 0;

            while (cookieQueue.Size > 1)
            {
                var firstCookie = cookieQueue.ExtractMin();
                var secondCookie = cookieQueue.ExtractMin();

                if (firstCookie >= minSweetness)
                {
                    return count;
                }

                cookieQueue.Add(firstCookie + secondCookie * 2);
                count++;
            }

            return -1;
        }
    }
}
