namespace SortingAlgorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class BubbleSortExtention
    {
        /// <summary>
        /// Takes a collection and return another sorted collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">Collection of comparable items</param>
        /// <returns>New IEnumerable collection</returns>
        public static IEnumerable<T> BubbleSort<T>(this IEnumerable<T> values) where T : IComparable<T>
        {
            var sortedArr = values.ToArray();
            bool isSorted = false;
            int sortedElCount = 0;

            while (!isSorted)
            {
                isSorted = true;

                for (int i = 1; i < sortedArr.Length - sortedElCount; i++)
                {
                    if (sortedArr[i - 1].CompareTo(sortedArr[i]) > 0)
                    {
                        SwapElements(sortedArr, i, i - 1);
                        isSorted = false;
                    }
                }

                sortedElCount++;
            }            

            return sortedArr;
        }

        private static void SwapElements<T>(T[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}
