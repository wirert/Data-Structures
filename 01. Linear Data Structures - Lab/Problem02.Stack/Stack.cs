namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private class Node
        {
            public T Item { get; set; }

            public Node Next { get; set; }
        }

        private Node top;

        public int Count { get; private set; }

        public void Push(T item)
        {
            var newNode = new Node()
            {
                Item = item,
                Next = top
            };

            top = newNode;
            Count++;
        }

        public T Pop()
        {
            var result = Peek();
            var newTop = top.Next;
            top.Next = null;
            top = newTop;
            Count--;

            return result;
        }

        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The stack is empty!");
            }

            return top.Item;
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
            var node = top;
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