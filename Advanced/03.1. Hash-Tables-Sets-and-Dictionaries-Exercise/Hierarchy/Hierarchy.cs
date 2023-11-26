namespace Hierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private Hierarchy<T> parent;

        private List<Hierarchy<T>> children;

        private T value;

        private Hierarchy(T value, Hierarchy<T> parent) :this(value)
        {
            this.parent = parent;
        }

        public Hierarchy(T value)
        {
            this.value = value;
            children = new List<Hierarchy<T>>();
        }

        public int Count => CountChildren(this);

        private int CountChildren(Hierarchy<T> node)
        {
            if (node == null) return 0;
            var count = 1;

            foreach (var child in node.children)
            {
                count += CountChildren(child);
            }

            return count;
        }

        public void Add(T element, T child)
        {
            var node = GetElement(element);

            if (node == null)
            {
                throw new ArgumentException("Element doesn't exist!");
            }

            if(Contains(child))
            {
                throw new ArgumentException("Child already exist!");
            }

            node.children.Add(new Hierarchy<T>(child, node));
        }


        public bool Contains(T element) => GetElement(element) != null;

        private Hierarchy<T> GetElement(T value)
        {
            var queue = new Queue<Hierarchy<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node.value.Equals(value))
                {
                    return node;
                }

                foreach (var child in node.children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        public IEnumerable<T> GetChildren(T element)
        {
            var node = GetElement(element);

            if (node == null)
            {
                throw new ArgumentException("Element doesn't exist!");
            }

            return node.children.Select(c => c.value);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            var set1 = GetAll(this);
            var set2 = GetAll(other);

            return set1.Intersect(set2);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var queue = new Queue<Hierarchy<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                foreach (var child in node.children)
                {
                    queue.Enqueue(child);
                }

                yield return node.value;
            }
        }

        public T GetParent(T element)
        {
            var node = GetElement(element);

            if (node == null)
            {
                throw new ArgumentException();
            }

            return node.parent == null ? default : node.parent.value;
        }

        public void Remove(T element)
        {
            var node = GetElement(element);

            if (node == null)
            {
                throw new ArgumentException();
            }

            if(node.parent == null)
            {
                throw new InvalidOperationException();
            }

            var children = node.children;

            foreach (var child in children)
            {
                child.parent = node.parent;
                node.parent.children.Add(child);
            }

            node.parent.children.Remove(node);
            node.parent = null;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private HashSet<T> GetAll(Hierarchy<T> tree)
        {
            var queue = new Queue<Hierarchy<T>>();
            var set = new HashSet<T>();
            queue.Enqueue(tree);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                foreach (var child in node.children)
                {
                    queue.Enqueue(child);
                }

                set.Add(node.value);
            }

            return set;
        }
    }
}