namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] items;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this.items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                ValidateIndex(index);
                return items[Count - 1 - index];
            }
            set
            {
                ValidateIndex(index);
                items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            GrowIfNeeded();

            items[Count++] = item;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (items[Count - 1 - i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            ValidateIndex(index);
            GrowIfNeeded();
            var realIndex = Count - index;

            for (int i = Count; i > realIndex; i--)
            {
                items[i] = items[i - 1];
            }

            items[realIndex] = item;
            Count++;
        }

        public bool Remove(T item)
        {
            var index = IndexOf(item);

            if (index == -1)
            {
                return false;
            }

            RemoveAt(index);

            return true;
        }

        public void RemoveAt(int index)
        {
            ValidateIndex(index);

            var realIndex = Count - 1 - index;

            for (int i = realIndex; i < Count; i++)
            {
                items[i] = items[i + 1];
            }

            Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                yield return items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }
        }

        private void GrowIfNeeded()
        {
            if (Count == items.Length)
            {
                var copy = new T[items.Length * 2];
                Array.Copy(items, copy, Count);
                items = copy;
            }
        }
    }
}