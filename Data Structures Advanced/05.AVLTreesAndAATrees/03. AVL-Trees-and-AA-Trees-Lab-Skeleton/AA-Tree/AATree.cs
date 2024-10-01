namespace AA_Tree
{
    using System;

    public class AATree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {
        private class Node
        {
            public Node(T element)
            {
                Value = element;
                Level = 1;
            }

            public T Value { get; set; }
            public Node Right { get; set; }
            public Node Left { get; set; }
            public int Level { get; set; }
        }

        private Node root;

        public int Count()
        {
            return Count(root);
        }

        private int Count(Node node)
        {
            if(node == null)
            {
                return 0;
            }

            return 1 + Count(node.Left) + Count(node.Right);
        }

        public void Insert(T element)
        {
            root = Insert(root, element);
        }

        private Node Insert(Node node, T element)
        {
            if(node == null)
            {
                return new Node(element);
            }

            if(element.CompareTo(node.Value) < 0)
            {
                node.Left = Insert(node.Left, element);
            }
            else
            {
                 node.Right = Insert(node.Right, element);
            }

            node = Skew(node);
            node = Split(node);

            return node;
        }

        private Node Split(Node node)
        {
            if(node == null)
            {
                return null;
            }
            else if(node.Right == null || node.Right.Right == null)
            {
                return node;
            }
            else if(node.Right.Right.Level == node.Level)
            {
                node = RotateRight(node);
                node.Level++;
            }

            return node;
        }

        private Node RotateRight(Node node)
        {
            var temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;

            return temp;
        }

        private Node Skew(Node node)
        {
            if(node == null)
            {
                return null;
            }
            else if(node.Left == null)
            {
                return node;
            }
            else if(node.Left.Level == node.Level)
            {
                node = RotateLeft(node);
            }

            return node;
        }

        private Node RotateLeft(Node node)
        {
            var temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;

            return temp;
        }

        public bool Contains(T element)
        {
            var current = root;

            while(current != null) 
            { 
                if(current.Value.Equals(element))
                {
                    return true;
                }
                else if(current.Value.CompareTo(element) > 0)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }

            return false;
        }

        public void InOrder(Action<T> action)
        {
            InOrder(root, action);
        }

        private void InOrder(Node node, Action<T> action)
        {
            if(node == null)
            {
                return;
            }

            InOrder(node.Left, action);
            action(node.Value);
            InOrder(node.Right, action);
        }

        public void PreOrder(Action<T> action)
        {
            PreOrder(root, action);
        }

        private void PreOrder(Node node, Action<T> action)
        {
            if(node == null)
            {
                return;
            }

            action(node.Value);
            PreOrder(node.Left, action);
            PreOrder(node.Right, action);
        }

        public void PostOrder(Action<T> action)
        {
            PostOrder(root, action);
        }

        private void PostOrder(Node node, Action<T> action)
        {
            if(node == null)
            {
                return;
            }

            PostOrder(node.Left, action);
            PostOrder(node.Right, action);
            action(node.Value);
        }
    }
}