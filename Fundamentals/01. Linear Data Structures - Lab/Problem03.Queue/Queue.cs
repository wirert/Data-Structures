namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private class Node
        {
            public T Item { get; set; }

            public Node Next { get; set; }
        }

        private Node head;

        public int Count { get; private set; }

        public void Enqueue(T item)
        {
            var node = new Node() { Item = item };

            if (head == null)
            {
                head = node;
            }
            else
            {
                var tail = head;
                while (tail.Next != null)
                {
                    tail = tail.Next;
                }

                tail.Next = node;
            }

            Count++;
        }

        public T Dequeue()
        {
            var result = Peek();
            var newHead = head.Next;
            head.Next = null;
            head = newHead;
            Count--;

            return result;
        }

        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The queue is empty!");
            }

            return head.Item;
        }

        public bool Contains(T item)
        {
            foreach (var member in this)
            {
                if (member.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = head;
            while (node != null)
            {
                yield return node.Item;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}