namespace _01.BinaryTree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
    {
        public BinaryTree(T value,
                        IAbstractBinaryTree<T> leftChild = null,
                        IAbstractBinaryTree<T> rightChild = null)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        public T Value { get; private set; }

        public IAbstractBinaryTree<T> LeftChild { get; private set; }

        public IAbstractBinaryTree<T> RightChild { get; private set; }

        public string AsIndentedPreOrder(int indent)
        {
            var sb = new StringBuilder();

            AsIndentedPreOrder(this, sb, 0);

            return sb.ToString().Trim();
        }
        
        public void AsIndentedPreOrder(IAbstractBinaryTree<T> node, StringBuilder sb, int indent)
        {
            sb.AppendLine($"{new string(' ', indent)}{node.Value}");

            if (node.LeftChild != null)
            {
                AsIndentedPreOrder(node.LeftChild, sb, indent + 2);
            }

            if (node.RightChild != null)
            {
                AsIndentedPreOrder(node.RightChild, sb, indent + 2);
            }
        }

        public List<IAbstractBinaryTree<T>> InOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            Order(this, result, "In");

            return result;
        }       

        public List<IAbstractBinaryTree<T>> PostOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            Order(this, result, "Post");

            return result;
        }

        public List<IAbstractBinaryTree<T>> PreOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            Order(this, result, "Pre");

            return result;
        }

        public void ForEachInOrder(Action<T> action)
        {
            if (this.LeftChild != null)
            {
                this.LeftChild.ForEachInOrder(action);
            }

            action.Invoke(this.Value);

            if (this.RightChild != null)
            {
                this.RightChild.ForEachInOrder(action);
            }

        }

        private void Order(IAbstractBinaryTree<T> node, List<IAbstractBinaryTree<T>> list, string method)
        {
            if (method == "Pre")
                list.Add(node);

            if (node.LeftChild != null)
            {
                Order(node.LeftChild, list, method);
            }

            if (method == "In")
                list.Add(node);

            if (node.RightChild != null)
            {
                Order(node.RightChild, list, method);
            }

            if (method == "Post")
                list.Add(node);
        }
    }
}
