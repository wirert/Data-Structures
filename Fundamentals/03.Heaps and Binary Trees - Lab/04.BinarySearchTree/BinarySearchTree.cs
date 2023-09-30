namespace _04.BinarySearchTree
{
    using System;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {
        }

        public BinarySearchTree(Node<T> root)
        {
            PreOrderCopy(root);
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public bool Contains(T element)
        {
            var result = Search(element);

            return result != null;
        }

        public void Insert(T element)
        {
            if (Root == null)
            {
                Root = new Node<T>(element);
                return;
            }

            var currNode = this.Root;

            while (true)
            {
                if (element.CompareTo(currNode.Value) == 1)
                {
                    if (currNode.RightChild == null)
                    {
                        currNode.RightChild = new Node<T>(element);
                        return;
                    }

                    currNode = currNode.RightChild;

                }
                else if (element.CompareTo(currNode.Value) == -1)
                {
                    if (currNode.LeftChild == null)
                    {
                        currNode.LeftChild = new Node<T>(element);
                        return;
                    }

                    currNode = currNode.LeftChild;
                }
                else
                {
                    return;
                }
            }            
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            var current = this.Root;

            while (current != null)
            {
                if (current.Value.CompareTo(element) == -1)
                {
                    current = current.RightChild;
                }
                else if (current.Value.CompareTo(element) == 1)
                {
                    current = current.LeftChild;
                }
                else
                {
                    return new BinarySearchTree<T>(current);
                }
            }

            return null;
        }

        private void PreOrderCopy(Node<T> node)
        {
            if (node == null)
            {
                return;
            }

            Insert(node.Value);
            PreOrderCopy(node.LeftChild);
            PreOrderCopy(node.RightChild);
        }
    }
}
