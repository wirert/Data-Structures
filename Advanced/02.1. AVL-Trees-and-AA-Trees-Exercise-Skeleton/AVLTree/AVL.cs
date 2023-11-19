namespace AVLTree
{
    using System;

    public class AVL<T> where T : IComparable<T>
    {
        public class Node
        {
            public Node(T value)
            {
                this.Value = value;
                Height = 1;
            }

            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Height { get; set; }
        }

        public Node Root { get; private set; }

        public bool Contains(T element)
        {
            return Contains(Root, element);
        }                

        public void Delete(T element)
        {
            throw new InvalidOperationException();
        }

        public void DeleteMin()
        {
            throw new InvalidOperationException();
        }

        public void Insert(T element)
        {
            Root = Insert(Root, element);
        }

       

        public void EachInOrder(Action<T> action)
        {
            EachInOrder(Root, action);
        }

        private static int Height(Node node)
        {
            if (node == null) return 0;
            return node.Height;
        }

        private bool Contains(Node node, T element)
        {
            if (node == null) return false;

            if (element.CompareTo(node.Value) < 0)
            {
                return Contains(node.Left, element);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                return Contains(node.Right, element);
            }
            else
            {
                return true;
            }
        }

        private Node Insert(Node node, T element)
        {
            if (node == null)
            {
                return new Node(element);
            }

            if (element.CompareTo(node.Value) < 0)
            {
                node.Left = Insert(node.Left, element);
            }
            else
            {
                node.Right = Insert(node.Right, element);
            }

            node = Balance(node);

            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;

            return node;
        }

        private Node Balance(Node node)
        {
            var balanceFactor = Height(node.Left) - Height(node.Right);
            if (balanceFactor > 1)
            {
                var leftChildBalance = Height(node.Left.Left) - Height(node.Left.Right);
                if (leftChildBalance < 0)
                {
                    node.Left = RotateLeft(node.Left);
                }
                node = RotateRight(node);
            }
            else if (balanceFactor < - 1)
            {
                var rightChildBalance = Height(node.Right.Left) - Height(node.Right.Right);

                if (rightChildBalance > 0)
                {
                    node.Right = RotateRight(node.Right);
                }

                node = RotateLeft(node);
            }


            return node;
        }

        private Node RotateLeft(Node node)
        {
            var newRoot = node.Right;
            node.Right = newRoot.Left;
            newRoot.Left = node;

            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;  

            return newRoot;
        }

        private Node RotateRight(Node node)
        {
            var newRoot = node.Left;
            node.Left = newRoot.Right;
            newRoot.Right = node;

            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;

            return newRoot;
        }

        private void EachInOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            EachInOrder(node.Left, action);
            action(node.Value);
            EachInOrder(node.Right, action);
        }
    }
}
