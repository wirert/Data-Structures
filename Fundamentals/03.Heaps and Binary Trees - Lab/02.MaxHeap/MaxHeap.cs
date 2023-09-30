namespace _03.MaxHeap
{
    using System;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> heap;

        public MaxHeap()
        {
            heap = new List<T>();
        }

        public int Size => heap.Count;

        public void Add(T element)
        {
            if (element.Equals(default)) return;

            heap.Add(element);

            HeapifyUp(Size - 1);
        }
       
        public T Peek()
        {
            return Size > 0 ? heap[0] : throw new InvalidOperationException();
        }

        private void HeapifyUp(int index)
        {
            if (index == 0) return;

            var parentIndex = (index - 1) / 2;

            if (heap[parentIndex].CompareTo(heap[index]) == -1)
            {
                var copy = heap[parentIndex];
                heap[parentIndex] = heap[index];
                heap[index] = copy;

                HeapifyUp(parentIndex);
            }
        }

    }
}
