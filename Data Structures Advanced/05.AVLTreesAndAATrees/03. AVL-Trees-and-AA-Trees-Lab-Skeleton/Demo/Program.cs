using System;
using AA_Tree;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new AATree<int>();

            tree.Insert(12);
            tree.Insert(3);
            tree.Insert(5);
            tree.Insert(8);
            tree.Insert(43);
            tree.Insert(9);
            tree.Insert(33);
            tree.Insert(1);
        }
    }
}
