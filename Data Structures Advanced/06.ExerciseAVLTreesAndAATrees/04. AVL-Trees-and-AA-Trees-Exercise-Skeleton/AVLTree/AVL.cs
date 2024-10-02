namespace AVLTree
{
    using System;

    public class AVL<T> where T : IComparable<T>
    {
        public class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.Height = 1;
            }

            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Height { get; set; }

            public override string ToString()
            {
                return $"V:[{Value}] H:[{Height}]"; // R:[{Right}] L:[{Left}]
            }
        }

        public Node Root { get; private set; }

        public bool Contains(T item)
        {
            return this.Contains(this.Root, item) != null;
        }

        public void Delete(T element)
        {
            this.Root = this.Delete(this.Root, element);
        }

        public void DeleteMin()
        {
            if (this.Root == null)
            {
                return;
            }

            Node temp = FindSmallestNode(this.Root);

            this.Root = Delete(this.Root, temp.Value);
        }

        public void Insert(T item)
        {
            this.Root = this.Insert(this.Root, item);
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.Root, action);
        }

        private void EachInOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }

        private Node Insert(Node node, T element)
        {
            if (node == null)
            {
                return new Node(element);
            }

            int cmp = element.CompareTo(node.Value);
            if (cmp < 0)
            {
                node.Left = this.Insert(node.Left, element);
            }
            else if (cmp > 0)
            {
                node.Right = this.Insert(node.Right, element);
            }

            node = Balance(node);
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;

            return node;
        }

        private Node Contains(Node node, T item)
        {
            if (node == null)
            {
                return null;
            }

            int cmp = item.CompareTo(node.Value);
            if (cmp < 0)
            {
                return Contains(node.Left, item);
            }
            else if (cmp > 0)
            {
                return Contains(node.Right, item);
            }

            return node;
        }

        private int Height(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Height;
        }

        private Node Delete(Node node, T element)
        {
            if (node == null)
            {
                return null;
            }

            if (element.CompareTo(node.Value) < 0)
            {
                node.Left = Delete(node.Left, element);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                node.Right = Delete(node.Right, element);
            }
            else
            {
                if (node.Left == null && node.Right == null)
                {
                    return null;
                }
                else if (node.Left == null)
                {
                    node = node.Right;
                }
                else if (node.Right == null)
                {
                    node = node.Left;
                }
                else
                {
                    Node temp = this.FindSmallestNode(node.Right);
                    node.Value = temp.Value;

                    node.Right = this.Delete(node.Right, temp.Value);
                }
            }

            if (node == null)
            {
                return null;
            }

            node = this.Balance(node);
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;

            return node;

        }

        private Node FindSmallestNode(Node node)
        {
            Node current = node;

            while (current.Left != null)
                current = current.Left;

            return current;
        }

        // Rotations

        private Node RotateLeft(Node node)
        {
            var right = node.Right;
            node.Right = node.Right.Left;
            right.Left = node;

            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;

            return right;
        }

        private Node RotateRight(Node node)
        {
            var left = node.Left;
            node.Left = node.Left.Right;
            left.Right = node;

            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;

            return left;
        }

        private Node Balance(Node node)
        {
            var balance = Height(node.Left) - Height(node.Right);
            if (balance > 1)
            {
                var childBalance = Height(node.Left.Left) - Height(node.Left.Right);
                if (childBalance < 0)
                {
                    node.Left = RotateLeft(node.Left);
                }

                node = RotateRight(node);
            }
            else if (balance < -1)
            {
                var childBalance = Height(node.Right.Left) - Height(node.Right.Right);
                if (childBalance > 0)
                {
                    node.Right = RotateRight(node.Right);
                }

                node = RotateLeft(node);
            }

            return node;
        }
    }
}
