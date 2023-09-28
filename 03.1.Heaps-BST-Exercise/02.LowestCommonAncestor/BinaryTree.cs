namespace _02.LowestCommonAncestor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(
            T value,
            BinaryTree<T> leftChild,
            BinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
            if (leftChild != null)
            {
                this.LeftChild.Parent = this;
            }

            if (rightChild != null)
            {
                this.RightChild.Parent = this;
            }
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public BinaryTree<T> Parent { get; set; }

        public T FindLowestCommonAncestor(T first, T second)
        {
            var firstNode = FindNodeByValue(first, this);
            var secondNode = FindNodeByValue(second, this);

            if (firstNode == null || secondNode == null)
            {
                throw new InvalidOperationException();
            }

            Queue<T> firstNodeAncestors = FindAllAncestors(firstNode);
            Queue<T> secondNodeAncestors = FindAllAncestors(secondNode);

            while (firstNodeAncestors.Count != 0)
            {
                var currAncestor = firstNodeAncestors.Dequeue();

                if (secondNodeAncestors.Contains(currAncestor))
                {
                    return currAncestor;
                }
            }

            throw new InvalidOperationException();
        }

        private Queue<T> FindAllAncestors(BinaryTree<T> node)
        {
            Queue<T> result = new Queue<T>();

            while (node != null)
            {
                result.Enqueue(node.Value);
                node = node.Parent;
            }

            return result;
        }

        private BinaryTree<T> FindNodeByValue(T value, BinaryTree<T> node)
        {
            if (node == null)
            {
                return null;
            }
            
            if (value.CompareTo(node.Value) > 0)
            {
                node = FindNodeByValue(value, node.RightChild);
            }
            else if (value.CompareTo(node.Value) < 0)
            {
                node = FindNodeByValue(value, node.LeftChild);
            }
            
            return node;
        }
    }
}
