namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T key, params Tree<T>[] children)
        {
            Key = key;
            _children = new List<Tree<T>>();

            foreach (var child in children)
            {
                AddChild(child);
                child.Parent = this;
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }


        public IReadOnlyCollection<Tree<T>> Children
            => _children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            _children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            Parent = parent;
        }

        public string GetAsString()
        {
            StringBuilder sb = new StringBuilder();
            AsString(this, string.Empty, sb);
            return sb.ToString().TrimEnd();
        }

        private void AsString(Tree<T> tree, string indent, StringBuilder sb)
        {
            sb.AppendLine($"{indent}{tree.Key}");
            indent += "  ";

            for (int i = 0; i < tree._children.Count; i++)
            {
                AsString(tree._children[i], indent, sb);
            }
        }

        public Tree<T> GetDeepestLeftomostNode()
        {
            IEnumerable<Tree<T>> leaves = GetLeafNodes();

            int depth = 0;
            Tree<T> node = null;
            foreach (var item in leaves)
            {
                int currentDepth = GetNodeDetph(item);
                if (currentDepth > depth)
                {
                    depth = currentDepth;
                    node = item;
                }
            }

            return node;
        }

        private int GetNodeDetph(Tree<T> item)
        {
            int depth = 0;

            Tree<T> curr = item;
            while (curr.Parent != null)
            {
                depth++;
                curr = curr.Parent;
            }

            return depth;
        }

        private IEnumerable<Tree<T>> GetLeafNodes()
        {
            List<Tree<T>> result = new List<Tree<T>>();
            BFS(this, result);
            return result;
        }

        private void BFS(Tree<T> tree, List<Tree<T>> result)
        {
            Queue<Tree<T>> queue = new Queue<Tree<T>>();

            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                Tree<T> subtrea = queue.Dequeue();

                if (!subtrea._children.Any())
                {
                    result.Add(subtrea);
                }

                foreach (var child in subtrea._children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        public List<T> GetLeafKeys()
        {
            List<T> result = new List<T>();
            DFS(this, result);
            return result;
        }

        private void DFS(Tree<T> tree, List<T> result)
        {
            foreach (var child in tree._children)
            {
                DFS(child, result);
            }


            if (!tree._children.Any())
            {
                result.Add(tree.Key);
            }
        }

        public List<T> GetMiddleKeys()
        {
            List<T> result = new List<T>();
            OrderMiddle(result, this);
            return result.OrderBy(r => r).ToList();
        }

        private void OrderMiddle(List<T> result, Tree<T> subtree)
        {
            Stack<Tree<T>> stack = new Stack<Tree<T>>();
            stack.Push(subtree);

            while (stack.Count != 0)
            {
                Tree<T> current = stack.Pop();

                if (current.Parent != null && current.Children.Count != 0)
                {
                    result.Add(current.Key);
                }

                foreach (var child in current.Children)
                {
                    stack.Push(child);
                }
            }
        }

        public List<T> GetLongestPath()
        {
            List<T> longestPath = new List<T>();
            Tree<T> node = GetDeepestLeftomostNode();

            longestPath.Add(node.Key);
            while (node.Parent != null)
            {
                node = node.Parent;
                longestPath.Add(node.Key);
            }

            longestPath.Reverse();
            return longestPath;
        }


        public List<List<T>> PathsWithGivenSum(int sum)
        {
            List<List<T>> result = new List<List<T>>();

            Stack<T> path = new Stack<T>();
            int total = int.Parse(Key.ToString());
            path.Push(Key);

            DFS(this, sum, ref total, path, result);
            return result;
        }

        private void DFS(Tree<T> tree,
                    int sum,
                    ref int total,
                    Stack<T> path,
                    List<List<T>> result)
        {
            if (tree.Children.Count == 0)
            {
                if (total == sum)
                {
                    List<T> list = new List<T>();
                    foreach (var item in path)
                    {
                        list.Add(item);
                    }

                    list.Reverse();
                    result.Add(list);
                }
                return;
            }

            foreach (var child in tree.Children)
            {
                total += int.Parse(child.Key.ToString());
                path.Push(child.Key);

                DFS(child, sum, ref total, path, result);

                path.Pop();
                total -= int.Parse(child.Key.ToString());
            }
        }


        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            List<Tree<T>> subtrees = new List<Tree<T>>();
            SubtreeSum(this, sum, subtrees);
            return subtrees;
        }

        private int SubtreeSum(Tree<T> tree, int sum, List<Tree<T>> subtrees)
        {
            int currentSum = int.Parse(tree.Key.ToString());
            foreach (var child in tree.Children)
            {
                currentSum += SubtreeSum(child, sum, subtrees);
            }

            if (currentSum == sum)
            {
                subtrees.Add(tree);
            }

            return currentSum;
        }
    }
}
