namespace _05.TopView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(T value, BinaryTree<T> left, BinaryTree<T> right)
        {
            this.Value = value;
            this.LeftChild = left;
            this.RightChild = right;
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public List<T> TopView()
        {
            var dict = new Dictionary<int, (T value, int level)>();

            PreOrderDfsTopView(dict, this, 0, 0);

            return dict.Select(n => n.Value.value).ToList();
        }

        private void PreOrderDfsTopView(Dictionary<int, (T value, int level)> dict, BinaryTree<T> node, int level, int distance)
        {
            if (node == null)
            {
                return;
            }

            if (!dict.ContainsKey(distance) || dict[distance].level > level)
            {
                dict[distance] = (node.Value, level);
            }

            PreOrderDfsTopView(dict, node.LeftChild, level + 1, distance - 1);
            PreOrderDfsTopView(dict, node.RightChild, level + 1, distance + 1);
        }
    }
}
