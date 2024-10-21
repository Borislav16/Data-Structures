namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;
        private Node<T> _last;


        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var currentNode = _head;
            while (currentNode != null)
            {
                if (currentNode.Value.Equals(item))
                {
                    return true;
                }

                currentNode = currentNode.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            this.EnsureNotEmpty();

            T removeHead = _head.Value;
            _head = _head.Next;
            Count--;

            return removeHead;
        }

        private void EnsureNotEmpty()
        {
            if (Count <= 0)
            {
                throw new InvalidOperationException();
            }
        }
        public void Enqueue(T item)
        {
            Node<T> newNode = new Node<T>
            {
                Value = item,
                Next = null
            };
            Count++;

            if (_head == null)
            {
                _head = newNode;
                _last = newNode;

                return;
            }

            _last.Next = newNode;
            _last = newNode;
        }

        public T Peek()
        {
            EnsureNotEmpty();
            return _head.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> node = this._head;
            while (node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}