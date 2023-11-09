namespace _01.Two_Three
{
    using System;
    using System.Text;

    public class TwoThreeTree<T> where T : IComparable<T>
    {
        private TreeNode<T> root;

        public void Insert(T element)
        {
            root = Insert(root, element);
        }

        private TreeNode<T> Insert(TreeNode<T> node, T element)
        {
            if (node == null)
            {
                return new TreeNode<T>(element);
            }

            if (node.IsLeaf())
            {
                return MergeNodes(node, new TreeNode<T>(element));
            }

            if (IsSmaller(element, node.LeftKey))
            {
                var newNode = Insert(node.LeftChild, element);

                return newNode == node.LeftChild ? node : MergeNodes(node, newNode);
            }

            if (node.IsTwoNode() || IsSmaller(element, node.RightKey))
            {
                var newNode = Insert(node.MiddleChild, element);

                return newNode == node.MiddleChild ? node : MergeNodes(node, newNode);
            }
            else
            {
                var newNode = Insert(node.RightChild, element);

                return newNode == node.RightChild ? node : MergeNodes(node, newNode);
            }
        }

        private bool IsSmaller(T element, T anothorElement) => element.CompareTo(anothorElement) < 0;

        private TreeNode<T> MergeNodes(TreeNode<T> curNode, TreeNode<T> node)
        {
            if (curNode.IsTwoNode())
            {
                if (IsSmaller(node.LeftKey, curNode.LeftKey))
                {
                    curNode.RightKey = curNode.LeftKey;
                    curNode.LeftKey = node.LeftKey;
                    curNode.RightChild = curNode.MiddleChild;
                    curNode.MiddleChild = node.MiddleChild;
                    curNode.LeftChild = node.LeftChild;
                }
                else
                {
                    curNode.RightKey = node.LeftKey;
                    curNode.MiddleChild = node.LeftChild;
                    curNode.RightChild = node.MiddleChild;
                }

                return curNode;
            }

            if (IsSmaller(node.LeftKey, curNode.LeftKey))
            {
                var newRoot = new TreeNode<T>(curNode.LeftKey)
                {
                    LeftChild = node,
                    MiddleChild = curNode
                };

                curNode.LeftChild = curNode.MiddleChild;
                curNode.MiddleChild = curNode.RightChild;
                curNode.LeftKey = curNode.RightKey;
                curNode.RightKey = default;
                curNode.RightChild = null; 
                
                return newRoot;
            }

            if (IsSmaller(node.LeftKey, curNode.RightKey))
            {
                var newLeft = new TreeNode<T>(curNode.LeftKey)
                {
                    LeftChild = curNode.LeftChild,
                    MiddleChild = node.LeftChild
                };

                curNode.MiddleChild = curNode.RightChild;
                curNode.LeftChild = node.MiddleChild;
                curNode.RightChild = null;
                curNode.LeftKey = curNode.RightKey;
                curNode.RightKey = default;
                node.LeftChild = newLeft;
                node.MiddleChild = curNode;

                return node;
            }
            else
            {
                var newRoot = new TreeNode<T>(curNode.RightKey)
                {
                    LeftChild = curNode,
                    MiddleChild = node
                };

                curNode.RightKey = default;
                curNode.RightChild = null;

                return newRoot;
            }

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            RecursivePrint(this.root, sb);
            return sb.ToString();
        }

        private void RecursivePrint(TreeNode<T> node, StringBuilder sb)
        {
            if (node == null)
            {
                return;
            }

            if (node.LeftKey != null)
            {
                sb.Append(node.LeftKey).Append(" ");
            }

            if (node.RightKey != null)
            {
                sb.Append(node.RightKey).Append(Environment.NewLine);
            }
            else
            {
                sb.Append(Environment.NewLine);
            }

            if (node.IsTwoNode())
            {
                RecursivePrint(node.LeftChild, sb);
                RecursivePrint(node.MiddleChild, sb);
            }
            else if (node.IsThreeNode())
            {
                RecursivePrint(node.LeftChild, sb);
                RecursivePrint(node.MiddleChild, sb);
                RecursivePrint(node.RightChild, sb);
            }
        }
    }
}
