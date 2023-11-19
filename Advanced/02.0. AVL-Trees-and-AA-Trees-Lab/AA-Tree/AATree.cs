namespace AA_Tree
{
    using System;

    public class AATree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {
        private static Node Nil = new Node (default) 
        {            
            Level = 0,
        };
        private class Node
        {
            public Node(T element)
            {
                Value = element;
                Level = 1;
                Right = Nil;
                Left = Nil;
            }

            public T Value { get; set; }
            public Node Right { get; set; }
            public Node Left { get; set; }
            public int Level { get; set; }
            
        }

        public AATree()
        {
            Nil.Left = Nil;
            Nil.Right = Nil;
            root = Nil;
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

        public void Delete(T element)
        {
            root = Delete(root, element);
        }

        public bool Contains(T element)
        {
            var node = root;

            while (node != Nil)
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
            if (node == Nil)
            {
                return 0;
            }

            return 1 + Count(node.Left) + Count(node.Right);
        }

        private Node Insert(Node node, T element)
        {
            if (node == Nil)
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

        private Node Delete(Node node, T element)
        {
            if (node == Nil)
            {
                return Nil;
            }

            if (element.CompareTo(node.Value) < 0)
            {
                node.Left = Delete(node.Left, element);
            }
            else if(element.CompareTo(node.Value) > 0)
            {
                node.Right = Delete(node.Right, element);
            }
            else
            {                
                if (node.Left == Nil)
                {
                    return node.Right;
                }
                
                T newValue = FindMin(node.Right);
                node.Right = Delete(node.Right, newValue);
                node.Value = newValue;                               
            }

            return FixUpAfterDeletion(node);
        }

        private Node FixUpAfterDeletion(Node node)
        {
            node = UpdateLevel(node);
            node = Skew(node);
            node.Right = Skew(node.Right);
            node.Right.Right = Skew(node.Right.Right);
            node = Split(node);
            node.Right = Split(node.Right);

            return node;
        }

        private Node UpdateLevel(Node node)
        {
            var idealLevel = 1 + Math.Min(node.Left.Level, node.Right.Level);

            if (node.Level > idealLevel)
            {
                node.Level = idealLevel;
                if (node.Right.Level > idealLevel)
                {
                    node.Right.Level = idealLevel;
                }
            }

            return node;
        }

        private T FindMin(Node node)
        {
            if (node.Left == Nil)
            {
                return node.Value;
            }

            return FindMin(node.Left);
        }

        private Node Skew(Node node)
        {
            if (node.Left != Nil && node.Left.Level == node.Level)
            {
                node = RotateRight(node);
            }

            return node;
        }

        private Node Split(Node node)
        {
            if (node.Right != Nil
                && node.Right.Right != Nil
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
            if (node == Nil)
            {
                return;
            }

            InOrder(node.Left, action);
            action(node.Value);
            InOrder(node.Right, action);
        }

        private void PreOrder(Node node, Action<T> action)
        {
            if (node == Nil)
            {
                return;
            }

            action(node.Value);
            PreOrder(node.Left, action);
            PreOrder(node.Right, action);
        }

        private void PostOrder(Node node, Action<T> action)
        {
            if (node == Nil)
            {
                return;
            }

            PostOrder(node.Left, action);
            PostOrder(node.Right, action);
            action(node.Value);
        }
    }
}