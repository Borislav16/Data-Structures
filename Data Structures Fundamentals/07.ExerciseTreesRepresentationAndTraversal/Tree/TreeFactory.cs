namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TreeFactory
    {
        private Dictionary<int, Tree<int>> nodesBykeys;

        public TreeFactory()
        {
            this.nodesBykeys = new Dictionary<int, Tree<int>>();
        }

        public Tree<int> CreateTreeFromStrings(string[] input)
        {
            foreach (var node in input)
            {
                List<int> keys = node.Split().Select(int.Parse).ToList();

                int parent = keys[0];
                int child = keys[1];

                AddEdge(parent, child);
            }

            return GetRoot();
        }

        public Tree<int> CreateNodeByKey(int key)
        {
            if (!nodesBykeys.ContainsKey(key))
            {
                nodesBykeys.Add(key, new Tree<int>(key));
            }

            return nodesBykeys[key];
        }

        public void AddEdge(int parent, int child)
        {
            Tree<int> parentNode = CreateNodeByKey(parent);
            Tree<int> childNode = CreateNodeByKey(child);

            parentNode.AddChild(childNode);
            childNode.AddParent(parentNode);
        }

        private Tree<int> GetRoot()
        {
            foreach (var item in nodesBykeys)
            {
                if(item.Value.Parent is null)
                {
                    return item.Value;
                }
            }

            return null;
        }
    }
}
