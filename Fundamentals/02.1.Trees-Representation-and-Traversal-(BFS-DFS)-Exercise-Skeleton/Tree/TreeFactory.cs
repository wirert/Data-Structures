namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TreeFactory
    {
        private Dictionary<int, IntegerTree> nodesByKey;

        public TreeFactory()
        {
            this.nodesByKey = new Dictionary<int, IntegerTree>();
        }

        public IntegerTree CreateTreeFromStrings(string[] input)
        {
            foreach (string str in input)
            {
                var values = str.Split(' ');
                var parentKey = int.Parse(values[0]);
                var childKey = int.Parse(values[1]);

                AddEdge(parentKey, childKey);
            }

            return GetRoot();
        }

        public IntegerTree CreateNodeByKey(int key)
        {
            if(!nodesByKey.ContainsKey(key))
            {
                nodesByKey.Add(key, new IntegerTree(key));
                
            }

            return nodesByKey[key];
        }

        public void AddEdge(int parent, int child)
        {
            var parentNode = CreateNodeByKey(parent);
            var childNode = CreateNodeByKey(child);

            parentNode.AddChild(childNode);
            childNode.AddParent(parentNode);            
        }

        public IntegerTree GetRoot()
            => nodesByKey
                    .Where(n => n.Value.Parent == null)
                    .SingleOrDefault()
                    .Value;
    }
}
