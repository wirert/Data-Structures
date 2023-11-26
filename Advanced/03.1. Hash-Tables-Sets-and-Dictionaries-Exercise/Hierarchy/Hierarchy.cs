namespace Hierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private class Node
        {
            public Node(T value)
            {
                Value = value;
                Children = new Dictionary<T, Node>();
            }

            public Node(T value, Node parent) : this(value)
            {
                Parent = parent;
            }

            public T Value { get; set; }
            public Node Parent { get; set; }
            public Dictionary<T,Node> Children { get; set; }
        }

        private Node root;

        private Dictionary<T, Node> nodesByValue;

        public int Count => nodesByValue.Count;

        public Hierarchy(T value)
        {
            root = new Node(value);
            nodesByValue = new Dictionary<T, Node>
            {
                { value, root }
            };
        }

        public void Add(T element, T child)
        {
            var node = GetNodeOrThrow(element);

            if (node.Children.ContainsKey(child))
            {
                throw new ArgumentException($"Child with value {child} already exist!");
            }
            var childNode = new Node(child, node);
            node.Children.Add(child, childNode);
            nodesByValue.Add(child, childNode);
        }

        public void Remove(T element)
        {
            if (root.Value.Equals(element))
            {
                throw new InvalidOperationException("Can't remove root element");
            }

            var node = GetNodeOrThrow(element);            

            foreach (var kvp in node.Children)
            {
                var childNode = kvp.Value;
                childNode.Parent = node.Parent;
                node.Parent.Children.Add(childNode.Value, childNode);
            }
            node.Parent.Children.Remove(node.Value);
            nodesByValue.Remove(element);
        }

        public IEnumerable<T> GetChildren(T element)
            => GetNodeOrThrow(element).Children.Keys;


        public T GetParent(T element)
        {
            var parent = GetNodeOrThrow(element).Parent;

            return parent == null ? default : parent.Value;
        }

        public bool Contains(T element) => nodesByValue.ContainsKey(element);

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other) 
            => this.nodesByValue.Keys.Intersect(other.nodesByValue.Keys);

        public IEnumerator<T> GetEnumerator()
        {
            var queue = new Queue<Node>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                foreach (var childPair in node.Children)
                {
                    queue.Enqueue(childPair.Value);
                }

                yield return node.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private Node GetNodeOrThrow(T element)
        {
            if (!Contains(element))
            {
                throw new ArgumentException();
            }

            return nodesByValue[element];
        }
    }
}
