namespace _02.BinarySearchTree
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Linq;

    public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable
    {
        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
            }

            public T Value { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Count { get; set; }
        }

        private Node root;

        private BinarySearchTree(Node node)
        {
            this.PreOrderCopy(node);
        }

        public BinarySearchTree()
        {
        }

        public void Insert(T element)
        {
            this.root = this.Insert(element, this.root);
        }

        public bool Contains(T element)
        {
            Node current = this.FindElement(element);

            return current != null;
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.root, action);
        }

        public IBinarySearchTree<T> Search(T element)
        {
            Node current = this.FindElement(element);

            return new BinarySearchTree<T>(current);
        }

        public void Delete(T element)
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }

            root = Delete(root, element);
        }

        public void DeleteMax()
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }

            root = DeleteMax(root);
        }

        public void DeleteMin()
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }

            root = DeleteMin(root);            
        }

        public int Count() => Count(root);        

        public int Rank(T element)
        {
            return Rank(element, root);
        }

        public T Select(int rank)
        {
            var node = Select(rank, root);

            if (node == null)
            {
                throw new InvalidOperationException();
            }

            return node.Value;
        }
               
        public T Ceiling(T element)
            => Select(Rank(element) + 1);
        
        public T Floor(T element)
            => Select(Rank(element) - 1);

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            var result = new List<T>();
            var node = this.root;

            Range(startRange, endRange, node, result);

            return result;
        }

        private int Count(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Count;
        }

        private void Range(T startRange, T endRange, Node node, List<T> result)
        {
            if (node == null)
            {
                return;
            }

            if (IsInRange(node.Value, startRange, endRange))
            {
                Range(startRange, endRange, node.Left, result);

                result.Add(node.Value);

                Range(startRange, endRange, node.Right, result);
            }
            else
            {
                Range(startRange, endRange, node.Right, result);
            }

        }

        private bool IsInRange(T element, T start, T end)
            => element.CompareTo(start) >= 0 &&
               element.CompareTo(end) <= 0;

        private Node FindElement(T element)
        {
            Node current = this.root;

            while (current != null)
            {
                if (current.Value.CompareTo(element) > 0)
                {
                    current = current.Left;
                }
                else if (current.Value.CompareTo(element) < 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }

            return current;
        }

        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }

            this.Insert(node.Value);
            this.PreOrderCopy(node.Left);
            this.PreOrderCopy(node.Right);
        }

        private Node Insert(T element, Node node)
        {
            if (node == null)
            {
                node = new Node(element);
            }
            else if (element.CompareTo(node.Value) < 0)
            {
                node.Left = this.Insert(element, node.Left);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                node.Right = this.Insert(element, node.Right);
            }

            node.Count = 1 + Count(node.Left) + Count(node.Right);

            return node;
        }

        private void EachInOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
            {
                return node.Right;
            }

            node.Left = DeleteMin(node.Left);

            node.Count = 1 + Count(node.Left) + Count(node.Right);

            return node;
        }

        private Node DeleteMax(Node node)
        {
            if (node.Right == null)
            {
                return node.Left;
            }

            node.Right = DeleteMax(node.Right);

            node.Count = 1 + Count(node.Left) + Count(node.Right);

            return node;
        }

        private Node Delete(Node node, T element)
        {
            if (node == null)
            {
                return null;
            }

            if (element.CompareTo(node.Value) < 0)
            {
                node.Left = Delete(node.Left, element);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                node.Right = Delete(node.Right, element);
            }
            else
            {
                if (node.Left == null)
                {
                    return node.Right;
                }

                if (node.Right == null)
                {
                    return node.Left;
                }

                var tempNode = node;
                node = FindMax(tempNode.Left);
                node.Left = DeleteMax(tempNode.Left);
                node.Right = tempNode.Right;
            }

            node.Count = 1 + Count(node.Left) + Count(node.Right);

            return node;
        }

        private Node FindMax(Node node)
        {
            if (node.Right == null)
            {
                return node;
            }

            return FindMax(node.Right);
        }

        private int Rank(T element, Node node)
        {
            if (node == null)
            {
                return 0;
            }

            if (element.CompareTo(node.Value) < 0)
            {
                return Rank(element, node.Left);
            }

            if (element.CompareTo(node.Value) > 0)
            {
                return 1 + Count(node.Left) + Rank(element, node.Right);
            }

            return Count(node.Left);
        }

        private Node Select(int rank, Node node)
        {
            if (node == null)
            {
                return null;
            }

            var leftCount = Count(node.Left);

            if (leftCount == rank)
            {
                return node;
            }

            if (leftCount > rank)
            {
                return Select(rank, node.Left);
            }

            return Select(rank - (leftCount + 1), node.Right);
        }
    }
}
