namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private class Node
        {
            public Node(T value)
            {
                Value = value;
            }

            public T Value { get; set; }
            public Node Next { get; set; }
            public Node Previous { get; set; }
        }

        private Node head;
        private Node tail;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var node = new Node(item);

            if (Count == 0)
            {
                head = tail = node;
            }
            else
            {
                head.Previous = node;
                node.Next = head;
                head = node;
            }

            Count++;
        }

        public void AddLast(T item)
        {
            var node = new Node(item);
            if (Count == 0)
            {
                head = tail = node;
            }
            else
            {
                node.Previous = tail;
                tail.Next = node;
                tail = node;
            }

            Count++;
        }

        public T GetFirst()
        {
            ThrowIfNoElements();

            return head.Value;
        }

        public T GetLast()
        {
            ThrowIfNoElements();

            return tail.Value;
        }

        public T RemoveFirst()
        {
            ThrowIfNoElements();

            var elementToRemove = head;

            if (Count == 1)
            {
                head = tail = null;
            }            
            else
            {
                head = elementToRemove.Next;
                head.Previous = null;
            }

            Count--;

            return elementToRemove.Value;
        }

        public T RemoveLast()
        {
            ThrowIfNoElements();

            var elementToRemove = tail;

            if (Count == 1)
            {
                head = tail = null;
            }
            else
            {
                tail = elementToRemove.Previous;
                tail.Next = null;
            }

            Count--;

            return elementToRemove.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = head;

            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void ThrowIfNoElements()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("There is no elements in list");
            }
        }
    }
}