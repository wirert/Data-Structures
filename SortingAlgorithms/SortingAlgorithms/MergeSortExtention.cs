namespace SortingAlgorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MergeSortExtention
    {
        /// <summary>
        /// Sorts the collection and return another collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Collection of comparable items</param>
        /// <returns>New IEnumerable collection</returns>
        public static IEnumerable<T> MergeSort<T>(this IEnumerable<T> source) where T : IComparable<T>
            => Sort<T>(source.ToList());

        private static List<T> Sort<T>(List<T> list) where T : IComparable<T>
        {
            if (list.Count <= 1)
            {
                return list;
            }

            var leftList = list.GetRange(0, list.Count/ 2);
            var rightList = list.GetRange(leftList.Count, list.Count - leftList.Count);

            leftList = Sort(leftList);
            rightList = Sort(rightList);

            return Merge(leftList, rightList);
        }

        private static List<T> Merge<T>(List<T> leftList, List<T> rightList) where T : IComparable<T>
        {
            var sortedList = new List<T>();
            int leftIndex = 0; 
            int rightIndex = 0;

            while (leftIndex < leftList.Count && rightIndex < rightList.Count)
            {
                if (leftList[leftIndex].CompareTo(rightList[rightIndex]) <= 0)
                {
                    sortedList.Add(leftList[leftIndex]);
                    leftIndex++;
                }
                else
                {
                    sortedList.Add(rightList[rightIndex]);
                    rightIndex++;
                }
            }

            for (int i = leftIndex; i < leftList.Count; i++)
            {
                sortedList.Add(leftList[i]);
            }

            for (int i = rightIndex; i < rightList.Count; i++)
            {
                sortedList.Add(rightList[i]);
            }

            return sortedList;
        }
    }
}
