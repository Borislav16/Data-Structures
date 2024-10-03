using System;
using AVLTree;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            var tree = new AVL<int>();

            tree.Insert(12);
            tree.Insert(3);
            tree.Insert(5);
            tree.Insert(8);
            tree.Insert(35);
            tree.Insert(16);
            tree.Insert(1);

        }
    }
}
