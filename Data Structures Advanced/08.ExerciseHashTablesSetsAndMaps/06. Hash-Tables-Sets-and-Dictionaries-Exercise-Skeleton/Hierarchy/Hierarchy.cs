namespace Hierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private class Node
        {
            public T Value { get; set; }
            public Node Parent { get; set; }
            public List<Node> Children { get; set; }

            public Node(T value)
            {
                Value = value;
                Children = new List<Node>();
            }
        }

        private Node root;
        private Dictionary<T, Node> nodesByValue;

        public Hierarchy(T value)
        {
            root = new Node(value);
            nodesByValue = new Dictionary<T, Node>();
            nodesByValue.Add(value, root);
        }

        public int Count => nodesByValue.Count;

        public void Add(T element, T child)
        {
            if (!nodesByValue.ContainsKey(element) || nodesByValue.ContainsKey(child))
            {
                throw new ArgumentException();
            }

            var node = new Node(child)
            {
                Parent = nodesByValue[element]
            };

            nodesByValue.Add(child, node);
            nodesByValue[element].Children.Add(node);
        }

        public bool Contains(T element)
        {
            return nodesByValue.ContainsKey(element);
        }

        public IEnumerable<T> GetChildren(T element)
        {
            if (!nodesByValue.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            return nodesByValue[element].Children.Select(x => x.Value);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            //var keys = new List<T>();
            //foreach (var key in nodesByValue.Keys)
            //{
            //    if (other.nodesByValue.ContainsKey(key))
            //    {
            //        keys.Add(key);
            //    }
            //}

            //return keys;

            return nodesByValue.Keys.Intersect(other.nodesByValue.Keys);
        }

        public T GetParent(T element)
        {
            if (!nodesByValue.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            if (nodesByValue[element].Parent == null)
            {
                return default;
            }

            return nodesByValue[element].Parent.Value;
        }

        public void Remove(T element)
        {
            if (element.Equals(root.Value))
            {
                throw new InvalidOperationException();
            }

            if (!nodesByValue.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            var node = nodesByValue[element];
            var parentNode = node.Parent;

            parentNode.Children.Remove(node);
            parentNode.Children.AddRange(node.Children);

            foreach(var child in node.Children)
            {
                nodesByValue[child.Value].Parent = parentNode;
            }

            nodesByValue.Remove(element);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var queue = new Queue<Node>();

            queue.Enqueue(root);

            while(queue.Count != 0)
            {
                var current = queue.Dequeue();

                yield return current.Value;

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}