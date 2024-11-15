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
            Dictionary<int, (T value, int level)> keyValuePairs = new Dictionary<int, (T value, int level)>();
            TopView(this, 0, 0, keyValuePairs);
            return keyValuePairs.Values.Select(x => x.value).ToList();
        }

        private void TopView(BinaryTree<T> binaryTree, int distance, int level, Dictionary<int, (T value, int level)> keyValuePairs)
        {
            if(binaryTree == null)
            {
                return;
            }

            if (!keyValuePairs.ContainsKey(distance))
            {
                keyValuePairs.Add(distance, (binaryTree.Value, level));
            }

            TopView(binaryTree.LeftChild, distance - 1, level + 1, keyValuePairs);
            TopView(binaryTree.RightChild, distance + 1, level + 1, keyValuePairs);
        }
    }
}
