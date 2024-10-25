using System.Xml.Linq;

namespace Problem02.DoublyLinkedList
{
    public class Node<T>
    {
        public Node()
        {
            
        }


        public Node<T> Next { get; set; }

        public Node<T> Previous { get; set; }

        public T Value { get; set; }


    }
}