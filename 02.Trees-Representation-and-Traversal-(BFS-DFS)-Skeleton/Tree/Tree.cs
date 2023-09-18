namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tree<T> : IAbstractTree<T>
    {
        private T value;
        private List<Tree<T>> children;
        private Tree<T> parent;


        public Tree(T _value)
        {
            value = _value;
            children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] _children)
            : this(value)
        {            
            foreach (var child in _children)
            {
                child.parent = this;
                children.Add(child);
            }
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            var node = FindTreeByValue(parentKey);
            node.children.Add(child);
        }

        public IEnumerable<T> OrderBfs()
        {
            var queue = new Queue<Tree<T>>();
            var list = new List<T>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                list.Add(node.value);
                
                foreach (var child in node.children)
                {
                    queue.Enqueue(child);                    
                }
            }

            return list;
        }

        public IEnumerable<T> OrderDfs()
        {     
            var stack = new Stack<Tree<T>>();
            var result = new Stack<T>();
            stack.Push(this);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                result.Push(node.value);

                foreach (var child in node.children)
                {
                    stack.Push(child);
                }
            }

            return result;
        }

        public void RemoveNode(T nodeKey)
        {
            var node = FindTreeByValue(nodeKey);

            if (node.parent == null)
            {
                throw new ArgumentException("Can't remove root tree");
            }

            var parent = node.parent;
            parent.children.Remove(node);
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = FindTreeByValue(firstKey);
            var secondNode = FindTreeByValue(secondKey);

            if (firstNode.parent == null || secondNode.parent == null)
            {
                throw new ArgumentException();
            }

            var parent1 = firstNode.parent;
            var parent1ChildIndex = parent1.children.IndexOf(firstNode);

            var parent2 = secondNode.parent;
            var parent2ChildIndex = parent2.children.IndexOf(secondNode);

            parent1.children[parent1ChildIndex] = secondNode; 
            firstNode.parent = parent2;

            parent2.children[parent2ChildIndex] = firstNode;
            secondNode.parent = parent1;
        }

        private Tree<T> FindTreeByValue(T value)
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node.value.Equals(value))
                {
                    return node;
                }

                foreach (var descendant in node.children)
                {
                    queue.Enqueue(descendant);
                }
            }

            throw new ArgumentNullException(nameof(value));
        }
    }
}
