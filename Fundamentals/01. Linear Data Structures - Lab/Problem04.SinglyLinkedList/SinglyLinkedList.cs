namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private class Node
        {
            public T Item { get; set; }

            public Node Next { get; set; }
        }

        private Node head;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var node = new Node() { Item = item };
            if (Count == 0)
            {
                head = node;
            }
            else
            {
                node.Next = head;
                head = node;
            }            

            Count++;
        }

        public void AddLast(T item)
        {
            var node = new Node() { Item = item };
            if (Count == 0)
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

        public IEnumerator<T> GetEnumerator()
        {
            var node = head;
            while (node != null)
            {
                yield return node.Item;
                node = node.Next;
            }
        }

        public T GetFirst()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The list is empty!");
            }

            return head.Item;
        }

        public T GetLast()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The list is empty!");
            }

            var tail = head;
            var result = tail.Item;
            if (Count > 1)            
            {
                while (tail.Next.Next != null)
                {
                    tail = tail.Next;
                }

                result = tail.Next.Item;
            }

            return result;
        }
        

        public T RemoveFirst()
        {
            var result = GetFirst();

            var newHead = head.Next;
            head.Next = null;
            head = newHead;
            Count--;

            return result;
        }

        public T RemoveLast()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The list is empty!");
            }

            var tail = head;
            var result = tail.Item;

            if (Count > 1)
            {
                while (tail.Next.Next != null)
                {
                    tail = tail.Next;
                }

                result = tail.Next.Item;

                tail.Next = null;
            }

            Count--;
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}