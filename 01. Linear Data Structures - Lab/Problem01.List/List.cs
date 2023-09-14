namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] items;

        public List()
            : this(DEFAULT_CAPACITY)
        {
            items = new T[DEFAULT_CAPACITY];
        }

        public List(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                ValidateIndex(index);
                return items[index];
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
            var index = IndexOf(item);

            return index != -1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return items[i];
            }
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (items[i].Equals(item))
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

            for (int i = Count; i > index; i--)
            {
                items[i] = items[i - 1];
            }
            items[index] = item;
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
            ShrinkIfNeeded();

            return true;
        }

        public void RemoveAt(int index)
        {
            ValidateIndex(index);

            Count--;

            for (int i = index; i < Count; i++)
            {
                items[i] = items[i + 1];
            }

            ShrinkIfNeeded();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void GrowIfNeeded()
        {
            if (Count == items.Length)
            {
                var copy = new T[items.Length * 2];
                Array.Copy(items, copy, items.Length);
                items = copy;
            }
        }

        private void ShrinkIfNeeded()
        {
            if (Count < items.Length / 2)
            {
                var copy = new T[items.Length / 2];
                Array.Copy(items, copy, Count); 
                items = copy;
            }
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}