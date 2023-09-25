namespace _03.PriorityQueue
{
    using System;
    using System.Collections.Generic;

    public class PriorityQueue<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> priorityQueue;

        public PriorityQueue()
        {
            priorityQueue = new List<T>();
        }

        public int Size => priorityQueue.Count;

        public T Dequeue()
        {
            var result = Peek();
            priorityQueue[0] = priorityQueue[Size - 1];
            priorityQueue.RemoveAt(Size - 1);

            HeapifyDown(0);

            return result;
        }

        public void Add(T element)
        {
            priorityQueue.Add(element);

            Heapify(Size - 1);
        }

        public T Peek() => priorityQueue[0];

        private void HeapifyDown(int index)
        {
            int leftChildIndex = index * 2 + 1;
            int rightChildIndex = index * 2 + 2;

            if (leftChildIndex >= this.Size) return;

            var biggerValueIndex = leftChildIndex;

            if (rightChildIndex < this.Size && 
                priorityQueue[leftChildIndex].CompareTo(priorityQueue[rightChildIndex]) == -1
                )
            {
                biggerValueIndex = rightChildIndex;
            }

            if (priorityQueue[index].CompareTo(priorityQueue[biggerValueIndex]) == -1)
            {
                SwapIndexes(index, biggerValueIndex);

                HeapifyDown(biggerValueIndex);
            }
        }

        private void Heapify(int index)
        {
            if (index == 0) return;

            var parentIndex = (index - 1) / 2;

            if (priorityQueue[parentIndex].CompareTo(priorityQueue[index]) == -1)
            {
               SwapIndexes(parentIndex, index);

                Heapify(parentIndex);
            }
        }

        private void SwapIndexes(int index1, int index2)
        {
            var copy = priorityQueue[index1];
            priorityQueue[index1] = priorityQueue[index2];
            priorityQueue[index2] = copy;
        }
    }
}
