namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            Node<T> newNode = new Node<T>
            {
                Value = item,
                Next = _head
            };

            _head = newNode;
            Count++;
        }

        public void AddLast(T item)
        {
            var newitem = new Node<T>();
            newitem.Value = item;
            if (_head == null)
            {
                _head = newitem;
                _head.Next = null;
                Count++;
            }
            else
            {
                var current = _head;
                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = newitem;
                Count++;
            }
        }

        public T GetFirst()
        {
            EnsureNotEmpty();

            return _head.Value;
        }

        public T GetLast()
        {
            EnsureNotEmpty();

            var currentNode = _head.Value;
            var currentNextNode = _head.Next;

            while (currentNextNode != null)
            {
                currentNode = currentNextNode.Value;
                currentNextNode = currentNextNode.Next;
            }

            return currentNode;
        }

        public T RemoveFirst()
        {
            EnsureNotEmpty();

            T removeFirst = _head.Value;
            _head = _head.Next;
            Count--;

            return removeFirst;
        }

        public T RemoveLast()
        {
            EnsureNotEmpty();

            Count--;
            if (_head.Next == null)
            {
                T element = _head.Value;
                _head = null;
                return element;
            }

            var curr = _head;
            while (curr.Next.Next != null)
            {
                curr = curr.Next;
            }

            T elementValue = curr.Next.Value;
            curr.Next = null;
            return elementValue;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = this._head;

            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private void EnsureNotEmpty()
        {
            if (this.Count <= 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}