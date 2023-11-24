namespace HashTable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private const int Default_Capacity = 6;
        private const double Load_Factor = 0.75;

        private LinkedList<KeyValue<TKey, TValue>>[] slots;

        public HashTable() : this(Default_Capacity)
        { }

        public HashTable(int capacity)
        {
            slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
            Count = 0;
        }

        public int Count { get; private set; }

        public int Capacity => slots.Length;

        public TValue this[TKey key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                AddOrReplace(key, value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            GrowIfNeeded();
            int index = GetIndex(key);
            if (slots[index] == null)
            {
                slots[index] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var kv in slots[index])
            {
                if (kv.Key.Equals(key))
                {
                    throw new ArgumentException($"Key already exists: {key}");
                }
            }

            slots[index].AddLast(new KeyValue<TKey, TValue>(key, value));
            Count++;
        }

        private void GrowIfNeeded()
        {
            if ((double)(Count + 1) / Capacity < Load_Factor)
            {
                return;
            }

            var newHashTable = new HashTable<TKey, TValue>(Capacity * 2);
            foreach (var element in this)
            {
                newHashTable.Add(element.Key, element.Value);
            }
            slots = newHashTable.slots;
            Count = newHashTable.Count;
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            try
            {
                Add(key, value);
                return true;
            }
            catch (ArgumentException ae)
            {
                if (ae.Message.Contains("Key already exists:")) 
                {
                    var element = Find(key);
                    element.Value = value;
                    return true;
                }

                throw;
            }
        }

        public TValue Get(TKey key)
        {
            var element = Find(key);

            if (element == null)
            {
                throw new KeyNotFoundException($"Key {key} doesn't exists!");
            }

            return element.Value;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var element = this.Find(key);

            if (element != null)
            {
                value = element.Value;
                return true;
            }

            value = default;
            return false;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            int index = GetIndex(key);
            var elements = slots[index];
            if (elements != null)
            {
                foreach (var kvp in elements)
                {
                    if (kvp.Key.Equals(key))
                    {
                        return kvp;
                    }
                }
            }

            return null;
        }

        public bool ContainsKey(TKey key) => Find(key) != null;
        
        public bool Remove(TKey key)
        {
            int index = GetIndex(key);
            if (slots[index] != null)
            {
                var curNode = slots[index].First;
                while (curNode != null) 
                {
                    if (key.Equals(curNode.Value.Key))
                    {
                        slots[index].Remove(curNode);
                        Count--;
                        return true;
                    }

                    curNode = curNode.Next;
                }
            }

            return false;
        }

        public void Clear()
        {
            slots = new LinkedList<KeyValue<TKey, TValue>>[Default_Capacity];
            Count = 0;
        }

        public IEnumerable<TKey> Keys => this.Select(e => e.Key);

        public IEnumerable<TValue> Values => this.Select(kvp => kvp.Value);

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach (var set in slots)
            {
                if (set != null)
                {
                    foreach (var kvp in set)
                    {
                        yield return kvp;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private int GetIndex(TKey key) => Math.Abs(key.GetHashCode()) % Capacity;
    }
}
