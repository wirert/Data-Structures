using System;
using System.Collections.Generic;

namespace _03.MinHeap
{
    public class PriorityQueue<T> : MinHeap<T> where T : IComparable<T>
    {
        private Dictionary<T, int> indexesKeys;

        public PriorityQueue()
        {
            this.elements = new List<T>();
            indexesKeys = new Dictionary<T, int>();
        }

        public void Enqueue(T element)
        {
            if (element == null)
            {
                throw new InvalidOperationException();
            }

            elements.Add(element);

            indexesKeys.Add(element, Size - 1);

            HeapifyUp(Size - 1);
        }

        public T Dequeue()
        {
            var result = Peek();

            SwapElements(0, Size - 1);

            elements.RemoveAt(Size - 1);
            indexesKeys.Remove(result);

            HeapifyDown(0);

            return result;
        }

        public void DecreaseKey(T key)
        {
            HeapifyUp(indexesKeys[key]);
        }

        private protected override void SwapElements(int index1, int index2)
        {
            base.SwapElements(index1, index2);

            indexesKeys[elements[index1]] = index1;
            indexesKeys[elements[index2]] = index2;
        }
    }
}
