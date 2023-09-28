using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace _03.MinHeap
{
    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        protected List<T> elements;

        public MinHeap()
        {
            this.elements = new List<T>();
        }

        public int Size => elements.Count;

        public void Add(T element)
        {
            if (element == null) 
            {
                throw new InvalidOperationException();            
            }

            elements.Add(element);

            HeapifyUp(Size - 1);
        }

        public T ExtractMin()
        {
            var result = Peek();

            elements[0] = elements[Size - 1];
            elements.RemoveAt(Size - 1);

            HeapifyDown(0);

            return result;
        }

        public T Peek() 
            => Size == 0 ? throw new InvalidOperationException() 
                          : elements[0];
        

        private protected void HeapifyUp(int index)
        {
            if (index == 0)
            {
                return;
            }

            var parentIndex = (index - 1)/ 2;

            if (elements[index].CompareTo(elements[parentIndex]) < 0)
            {
               SwapElements(index, parentIndex);

                HeapifyUp(parentIndex);
            }
        }

        private protected void HeapifyDown(int index)
        {
            var leftChildIndex = index * 2 + 1;
            var rightChildIndex = index * 2 + 2;

            if (leftChildIndex >= Size) return;

            var smallerElementIndex = leftChildIndex;

            if (rightChildIndex < Size &&
                IsSmallerElement(rightChildIndex, leftChildIndex))
            {
                smallerElementIndex = rightChildIndex;
            }

            if (IsSmallerElement(smallerElementIndex, index))
            {
                SwapElements(smallerElementIndex, index);

                HeapifyDown(smallerElementIndex);
            }
        }

        private protected virtual void SwapElements(int index1, int index2)
        {
            var copy = elements[index2];
            elements[index2] = elements[index1];
            elements[index1] = copy;
        }

        private bool IsSmallerElement(int index1, int index2)
            => elements[index1].CompareTo(elements[index2]) < 0;
    }
}
