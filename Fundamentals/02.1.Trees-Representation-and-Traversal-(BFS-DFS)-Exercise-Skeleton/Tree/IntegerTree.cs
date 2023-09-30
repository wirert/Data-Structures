namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class IntegerTree : Tree<int>, IIntegerTree
    {
        public IntegerTree(int key, params Tree<int>[] children)
            : base(key, children)
        {
        }

        public List<List<int>> PathsWithGivenSum(int sum)
        {
            var leaves = GetLeafNodesDfs();
            var result = new List<List<int>>();

            foreach (var leaf in leaves)
            {
                var path = GetPath(leaf);

                if (path.Sum() == sum)
                {
                    result.Add(path.ToList());
                }
            }

            return result;
        }

        public List<Tree<int>> SubTreesWithGivenSum(int sum)
        {
            var allTrees = GetNodesBfs(this);
            var result = new List<Tree<int>>();

            foreach (var tree in allTrees)
            {
               var treeNodesKeys = GetNodesBfs(tree).Select(t => t.Key);

                if (treeNodesKeys.Sum() == sum)
                {
                    result.Add(tree);
                }
            }

            return result;
        }  
        
        private IEnumerable<Tree<int>> GetNodesBfs(Tree<int> root)
        {
            var queue = new Queue<Tree<int>>();
            var trees = new Queue<Tree<int>>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var tree = queue.Dequeue();                

                trees.Enqueue(tree);

                foreach (var child in tree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return trees;
        }
    }
}
