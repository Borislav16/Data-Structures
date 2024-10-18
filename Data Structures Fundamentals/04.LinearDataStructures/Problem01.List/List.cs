namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] _items;

         public List()
            : this(DEFAULT_CAPACITY) {
        }

        public List(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            _items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                ValidateIndex(index);
                return _items[index];
            }
            set
            {
                ValidateIndex(index);
                _items[index] = value;
            }
        }

        private void ValidateIndex(int index)
        {
            if(index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            GrowIfNecessary();
            _items[Count++] = item;

        }

        private void GrowIfNecessary()
        {
            if (Count == _items.Length)
            {
                _items = Grow();
            }
        }

        private T[] Grow()
        {
            var newArray = new T[Count * 2];
            for (int i = 0; i < _items.Length; i++)
            {
                newArray[i] = _items[i];
            }

            return newArray;
        }

        public bool Contains(T item)
        {
            for(int i = 0; i < Count;i++)
            {
                if (_items[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }


        public int IndexOf(T item)
        {
            for(int i = 0; i < Count; i++)
            {
                if (_items[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {

            ValidateIndex(index);
            Grow();
            T[] insertInArray = new T[Count];
            for (int i = Count; i > index; i--)
            {
                _items[i] = _items[i - 1];
            }

            _items[index] = item;
            Count++;
        }

        public bool Remove(T item)
        {
            if(Contains(item))
            {
                RemoveAt(IndexOf(item));

                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            ValidateIndex(index);
            for (int i = index; i < Count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }

            _items[Count - 1] = default;
            Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for(int i = 0; i < Count; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}