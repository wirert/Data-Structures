namespace _01.RedBlackTree
{
    using System;

    public class RedBlackTree<T> where T : IComparable
    {
        private const bool RED = true;
        private const bool BLACK = false;

        public class Node
        {
            public Node(T value)
            {
                this.Value = value;
                Color = RED;
            }

            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public bool Color { get; set; }
        }

        private RedBlackTree(Node node)
        {
            PreOrderCopy(node);
        }

        public RedBlackTree()
        {
        }

        public Node root;

        public int Count()
        {
            return Count(root);
        }

        public void EachInOrder(Action<T> action)
        {
            EachInOrder(root, action);
        }

        public RedBlackTree<T> Search(T element)
        {
            var node = FindNodeByValue(element);

            return new RedBlackTree<T>(node);
        }       

        public void Insert(T value)
        {
            root = Insert(root, value);

            if (root.Color == RED)
            {
                root.Color = BLACK;
            }
        }

        public void DeleteMin()
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }

            root = DeleteMin(root);

            if (root != null)
            {
                root.Color = BLACK;
            }
        }

        public void DeleteMax()
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }

            root = DeleteMax(root);

            if (root != null)
            {
                root.Color = BLACK;
            }
        }

        public void Delete(T key)
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }

            root = Delete(root, key);

            if (root != null)
            {
                root.Color = BLACK;
            }
        }
        
        //private methods

        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }

            Insert(node.Value);
            PreOrderCopy(node.Left);
            PreOrderCopy(node.Right);
        }

        private int Count(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return 1 + Count(node.Left) + Count(node.Right);
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

        private Node FindNodeByValue(T element)
        {
            var curNode = root;

            while (curNode != null)
            {
                if (IsSmaller(element, curNode.Value))
                {
                    curNode = curNode.Left;
                }
                else if (IsSmaller(curNode.Value, element))
                {
                    curNode = curNode.Right;
                }
                else
                {
                    break;
                }
            }

            return curNode;
        }

        private Node Insert(Node node, T element)
        {
            if (node == null)
            {
                return new Node(element);
            }

            if (IsSmaller(element, node.Value))
            {
                node.Left = Insert(node.Left, element);
            }
            else
            {
                node.Right = Insert(node.Right, element);
            }

            return FixUp(node);
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
            {
                return null;
            }

            if (!IsRed(node.Left) && !IsRed(node.Left.Left))
            {
                node = MoveRedLeft(node);
            }

            node.Left = DeleteMin(node.Left);

            return FixUp(node);
        }

        private Node DeleteMax(Node node)
        {
            if (IsRed(node.Left))
            {
                node = RotateRight(node);
            }

            if (node.Right == null)
            {
                return null;
            }

            if (!IsRed(node.Right) && !IsRed(node.Right.Left))
            {
                node = MoveRedRight(node);
            }

            node.Right = DeleteMax(node.Right);

            return FixUp(node);
        }

        private Node Delete(Node node, T element)
        {
            if (IsSmaller(element, node.Value))
            {
                if (!IsRed(node.Left) && !IsRed(node.Left.Left))
                {
                    node = MoveRedLeft(node);
                }

                node.Left = Delete(node.Left, element);
            }
            else
            {
                if (IsRed(node.Left))
                {
                    node = RotateRight(node);
                }

                if (AreEqual(element, node.Value) && node.Right == null)
                {
                    return null;
                }

                if (!IsRed(node.Right) && !IsRed(node.Right.Left))
                {
                    node = MoveRedRight(node);
                }

                if (AreEqual(element, node.Value))
                {
                    node.Value = GetMinValue(node.Right);
                    DeleteMin(node.Right);
                }
                else
                {
                    node.Right = Delete(node.Right, element);
                }
            }

            return FixUp(node);
        }

        // Rotations

        private void FlipColors(Node node)
        {
            node.Color = !node.Color;
            node.Right.Color = !node.Right.Color;
            node.Left.Color = !node.Left.Color;
        }

        private Node RotateRight(Node node)
        {
            var newRoot = node.Left;
            node.Left = newRoot.Right;
            newRoot.Right = node;
            newRoot.Color = node.Color;
            node.Color = RED;

            return newRoot;
        }

        private Node RotateLeft(Node node)
        {
            var newRoot = node.Right;
            node.Right = newRoot.Left;
            newRoot.Left = node;
            newRoot.Color = node.Color;
            node.Color = RED;

            return newRoot;
        }

        private Node MoveRedLeft(Node node)
        {
            FlipColors(node);

            if (IsRed(node.Right.Left))
            {
                node.Right = RotateRight(node.Right);
                node = RotateLeft(node);
                FlipColors(node);
            }

            return node;
        }

        private Node MoveRedRight(Node node)
        {
            FlipColors(node);

            if (IsRed(node.Left.Left))
            {
                node = RotateRight(node);
                FlipColors(node);
            }

            return node;
        }

        private Node FixUp(Node node)
        {
            if (IsRed(node.Right))
            {
                node = RotateLeft(node);
            }

            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = RotateRight(node);
            }

            if (IsRed(node.Left) && IsRed(node.Right))
            {
                FlipColors(node);
            }

            return node;
        }

        private bool IsRed(Node node)
        {
            if (node == null) return false;

            return node.Color == RED;
        }

        //Helper methods

        private bool IsSmaller(T a, T b) => a.CompareTo(b) < 0;

        private bool AreEqual(T a, T b) => a.CompareTo(b) == 0;

        private T GetMinValue(Node node)
        {
            if (node.Left == null)
            {
                return node.Value;
            }

            return GetMinValue(node.Left);
        }
    }
}