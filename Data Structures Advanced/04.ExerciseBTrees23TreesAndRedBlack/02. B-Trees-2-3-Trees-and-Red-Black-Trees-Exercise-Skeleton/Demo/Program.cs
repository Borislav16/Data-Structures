using _01.RedBlackTree;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            var rbt = new RedBlackTree<int>();

            rbt.Insert(12);
            rbt.Insert(3);
            rbt.Insert(5);
            rbt.Insert(8);
            rbt.Insert(32);
            rbt.Insert(45);
            rbt.Insert(1);
        }
    }
}
