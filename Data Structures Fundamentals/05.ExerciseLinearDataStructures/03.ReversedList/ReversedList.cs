namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] items;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            items = new T[capacity];
        }

        public int Count { get; private set; }

        public T this[int index]
        {
            get
            {
                ValidateIndex(index);

                return items[Count - index - 1];
            }
            set
            {
                ValidateIndex(index);
                items[index] = value;
            }
        }

        public void Add(T item)
        {
            IncreaseSize();
            items[Count++] = item;
        }

        public bool Contains(T item)
            => IndexOf(item) != -1;

        public int IndexOf(T item)
        {
            for (int i = 1; i <= Count; i++)
            {
                if (items[Count - i].Equals(item))
                {
                    return i - 1;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            IncreaseSize();
            ValidateIndex(index);
            int insertIndex = Count - index;

            for (int i = Count; i > insertIndex; i--)
            {
                items[i] = items[i - 1];
            }

            items[insertIndex] = item;
            Count++;
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);

            if (index == -1)
            {
                return false;
            }

            RemoveAt(index);

            return true;
        }

        public void RemoveAt(int index)
        {
            ValidateIndex(index);

            int toRemove = Count - index - 1;
            for (int i = toRemove; i < Count - 1; i++)
            {
                items[i] = items[i + 1];
            }

            items[Count - 1] = default;
            Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                yield return items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private void Resize()
        {
            T[] values = new T[items.Length * 2];
            Array.Copy(items, values, items.Length);
            items = values;
        }

        private void IncreaseSize()
        {
            if (Count == items.Length)
            {
                Resize();
            }
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}