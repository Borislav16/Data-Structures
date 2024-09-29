using _01.Two_Three;
using System;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            var tree = new TwoThreeTree<string>();

            tree.Insert("D");
            tree.Insert("G");
            tree.Insert("A");
            tree.Insert("P");
            tree.Insert("Z");
            tree.Insert("F");
            tree.Insert("E");

            Console.WriteLine(tree.ToString());
        }
    }
}
