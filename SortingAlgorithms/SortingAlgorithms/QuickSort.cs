namespace SortingAlgorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class QuickSort
    {
        public static ICollection<T> Sort<T>(ICollection<T> arr) where T : IComparable<T>
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
            var pivotIndex = loIndex - 1;

            for (int i = loIndex; i < hiIndex; i++)
            {
                if (arr[i].CompareTo(pivot) <= 0)
                {
                    pivotIndex++;
                    SwapElements(arr, pivotIndex, i);
                }
            }

            pivotIndex++;
            SwapElements(arr, pivotIndex, hiIndex);

            return pivotIndex;
        }

        private static void SwapElements<T>(T[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}
