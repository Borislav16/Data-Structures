namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var newNode = new Node<T>()
            {
                Value = item,
                Next = null,
                Previous = null
            };

            if(Count == 0)
            {
                head = tail = newNode;
            }
            else
            {
                var oldHead = head;
                oldHead.Previous = newNode;
                head = newNode;
                head.Next = oldHead;
            }
            Count++;
        }

        public void AddLast(T item)
        {
            var newNode = new Node<T>()
            {
                Value = item,
                Next = null,
                Previous = null
            };

            if (Count == 0)
            {
                head = tail = newNode;
            }

            else
            {
                var oldTail = tail;
                oldTail.Next = newNode;
                tail = newNode;
                tail.Previous = oldTail;
            }
            Count++;
        }

        public T GetFirst()
        {
            ValidateCollection();
            return head.Value;
        }

        public T GetLast()
        {
            ValidateCollection();
            return tail.Value;
        }

        public T RemoveFirst()
        {
            ValidateCollection();

            var oldHead = head;
            if (head.Next == null)
            {
                head = null;
                tail = null;
            }
            else
            {
                var newNode = head.Next;
                head = newNode;
            }
            Count--;
            return oldHead.Value;
        }

        public T RemoveLast()
        {
            ValidateCollection();
            var oldTail = tail;
            if (oldTail.Previous == null)
            {
                head = null;
                tail = null;
            }
            else
            {
                var newNode = tail.Previous;
                tail = newNode;
            }
            Count--;
            return oldTail.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = head;
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ValidateCollection()
        {
            if(this.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}