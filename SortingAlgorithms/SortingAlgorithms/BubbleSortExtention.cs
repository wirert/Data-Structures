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

            for ( var i = 0; i < sortedArr.Length - 1; i++ )
            {
                for ( var j = 0; j < sortedArr.Length - 1; j++)
                {
                    if (sortedArr[j].CompareTo(sortedArr[j + 1]) > 0)
                    {
                        SwapElements(sortedArr, j, j + 1);
                    }
                }
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
