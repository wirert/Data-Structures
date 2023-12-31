﻿namespace SortingAlgorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class QuickSortExtention
    {
        /// <summary>
        /// Sorts the collection and return new enumerable collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns>New collection</returns>
        public static ICollection<T> QuickSort<T>(this ICollection<T> arr) where T : IComparable<T>
        {
            var result = arr.ToArray();
            Sort(result, 0, arr.Count - 1);

            return result;
        }

        private static void Sort<T>(T[] arr, int loIndex, int hiIndex) where T : IComparable<T>
        {
            if (loIndex >= hiIndex || loIndex < 0)
            {
                return;
            }

            int pivotIndex = Partition(arr, loIndex, hiIndex);

            Sort(arr, loIndex, pivotIndex - 1);
            Sort(arr, pivotIndex + 1, hiIndex);
        }

        private static int Partition<T>(T[] arr, int loIndex, int hiIndex) where T : IComparable<T>
        {
            var pivot = arr[hiIndex];
            var pivotSwapIndex = loIndex - 1;

            for (int i = loIndex; i < hiIndex; i++)
            {
                if (arr[i].CompareTo(pivot) <= 0)
                {
                    pivotSwapIndex++;
                    SwapElements(arr, pivotSwapIndex, i);
                }
            }

            pivotSwapIndex++;
            SwapElements(arr, pivotSwapIndex, hiIndex);

            return pivotSwapIndex;
        }

        private static void SwapElements<T>(T[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}
