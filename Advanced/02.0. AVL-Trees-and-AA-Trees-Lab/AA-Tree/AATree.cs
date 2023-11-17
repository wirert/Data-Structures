namespace AA_Tree
{
    using System;

    public class AATree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {
        private class Node
        {
            public Node(T element)
            {
                Value = element;
                Level = 1;
            }

            public T Value { get; set; }
            public Node Right { get; set; }
            public Node Left { get; set; }
            public int Level { get; set; }
        }

        private Node root;

        public int Count()
        {
            return Count(root);
        }

        public void Insert(T element)
        {
            root = Insert(root, element);
        }

        public bool Contains(T element)
        {
            var node = root;

            while (node != null)
            {
                if (node.Value.Equals(element))
                {
                    return true;
                }

                if (element.CompareTo(node.Value) < 0)
                {
                    node = node.Left;
                }
                else
                {
                    node = node.Right;
                }
            }

            return false;
        }

        public void InOrder(Action<T> action)
        {
            InOrder(root, action);
        }

        public void PreOrder(Action<T> action)
        {
            PreOrder(root, action);
        }

        public void PostOrder(Action<T> action)
        {
            PostOrder(root, action);
        }

        private int Count(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return 1 + Count(node.Left) + Count(node.Right);
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

            node = Skew(node);
            node = Split(node);

            return node;
        }

        private Node Skew(Node node)
        {
            if (node.Left != null && node.Left.Level == node.Level)
            {
                node = RotateRight(node);
            }

            return node;
        }

        private Node Split(Node node)
        {
            if (node.Right != null
                && node.Right.Right != null
                && node.Right.Right.Level == node.Level)
            {
                node = RotateLeft(node);
                node.Level++;
            }

            return node;
        }

        private Node RotateRight(Node node)
        {
            var newRoot = node.Left;
            node.Left = newRoot.Right;
            newRoot.Right = node;

            return newRoot;
        }

        private Node RotateLeft(Node node)
        {
            var newRoot = node.Right;
            node.Right = newRoot.Left;
            newRoot.Left = node;

            return newRoot;
        }

        private void InOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            InOrder(node.Left, action);
            action(node.Value);
            InOrder(node.Right, action);
        }

        private void PreOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            action(node.Value);
            PreOrder(node.Left, action);
            PreOrder(node.Right, action);
        }

        private void PostOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            PostOrder(node.Left, action);
            PostOrder(node.Right, action);
            action(node.Value);
        }
    }
}