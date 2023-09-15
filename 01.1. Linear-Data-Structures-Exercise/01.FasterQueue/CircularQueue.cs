namespace Problem01.CircularQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CircularQueue<T> : IAbstractQueue<T>
    {
        private T[] elements;
        private int startIndex;
        private int endIndex;

        public CircularQueue(int capacity = 4)
        {
            elements = new T[capacity];
        }

        public int Count { get; private set; }

        public T Dequeue()
        {
            var result = Peek();
            if (Count == 1)
            {
                startIndex = 0;
                endIndex = 0;
            }
            else if (startIndex == elements.Length - 1)
            {
                startIndex = 0;
            }
            else
            {
                startIndex++;
            }

            Count--;

            return result;
        }

        public void Enqueue(T item)
        {
            GrowIfNeeded();

            if (endIndex == elements.Length - 1 || Count == 0)
            {
                endIndex = 0;
            }
            else
            {
                endIndex++;
            }

            elements[endIndex] = item;
            Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return elements[startIndex + i % elements.Length];
            }
        }

        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            return elements[startIndex];
        }

        public T[] ToArray() => CopyArray(Count);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void GrowIfNeeded()
        {
            if (Count == elements.Length)
            {
                var copy = CopyArray(elements.Length * 2);
                
                startIndex = 0;
                endIndex = Count - 1;
                elements = copy;
            }
        }

        private T[] CopyArray(int capacity)
        {
            var result = new T[capacity];
            for (int i = 0; i < Count; i++)
            {
                result[i] = elements[startIndex + i % elements.Length];
            }

            return result;
        }
    }
}
