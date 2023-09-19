namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private List<Tree<T>> children;

        public Tree(T key, params Tree<T>[] _children)
        {
            Key = key;
            children = new List<Tree<T>>();

            foreach (var child in _children)
            {
                child.Parent = this;
                children.Add(child);
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children => children.AsReadOnly();

        public void AddChild(Tree<T> child) => children.Add(child);

        public void AddParent(Tree<T> parent) => Parent = parent;

        public string GetAsString()
        {
            StringBuilder sb = new StringBuilder();

            DfsToString(this, 0, sb);

            return sb.ToString().Trim();
        }

        public List<T> GetMiddleKeys()
        {
            var queue = new Queue<Tree<T>>();
            var result = new List<T>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var currentTree = queue.Dequeue();

                if (currentTree.Parent != null && currentTree.children.Count > 0)
                {
                    result.Add(currentTree.Key);
                }

                foreach (var child in currentTree.children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public IEnumerable<T> GetLeafKeys()
            => GetLeafNodesDfs().Select(node => node.Key);

        public T GetDeepestLeftomostNode() => GetDeepestLeaf().Key;

        public List<T> GetLongestPath()
        {
            var leaf = GetDeepestLeaf();
            
            return (List<T>)GetPath(leaf);
        }

        protected ICollection<Tree<T>> GetLeafNodesDfs()
        {
            var stack = new Stack<Tree<T>>();
            var result = new Stack<Tree<T>>();
            stack.Push(this);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                if (node.children.Count == 0)
                {
                    result.Push(node);
                    continue;
                }

                foreach (var child in node.children)
                {
                    stack.Push(child);
                }
            }

            return result.ToArray();
        }

        private Tree<T> GetDeepestLeaf()
        {
            var leafNodes = GetLeafNodesDfs();

            Tree<T> deepestLeaf = default;
            var maxDepth = 0;

            foreach (var leaf in leafNodes)
            {
                var depth = GetPath(leaf).Count();

                if (maxDepth < depth)
                {
                    maxDepth = depth;
                    deepestLeaf = leaf;
                }
            }

            return deepestLeaf;
        }

        protected ICollection<T> GetPath(Tree<T> tree)
        {
            var path = new Stack<T>();

            while (tree != null)
            {
                path.Push(tree.Key);
                tree = tree.Parent;
            }

            return path.ToList();
        }

        private void DfsToString(Tree<T> node, int indent, StringBuilder sb)
        {
            sb.AppendLine($"{new string(' ', indent)}{node.Key}");

            foreach (var child in node.Children)
            {
                DfsToString(child, indent + 2, sb);
            }
        }
    }
}
