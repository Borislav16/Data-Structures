namespace HashTable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private const int DefaultCapacity = 4;
        private const float LoadFactor = 0.75f;

        private LinkedList<KeyValue<TKey, TValue>>[] slots;

        public int Count { get; private set; }

        public int Capacity => slots.Length;

        public HashTable() : this(DefaultCapacity) { }

        public HashTable(int capacity)
        {
            slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
        }

        private HashTable(int capacity, IEnumerable<KeyValue<TKey, TValue>> keyValuePairs)
            : this(capacity)
        {
            foreach (var element in keyValuePairs)
            {
                Add(element.Key, element.Value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            GrowIfNeeded();

            int index = Math.Abs(key.GetHashCode()) % Capacity;

            if (slots[index] == null)
            {
                slots[index] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var element in slots[index])
            {
                if (element.Key.Equals(key))
                {
                    throw new ArgumentException("Duplicate Key", key.ToString());
                }
            }

            var newElement = new KeyValue<TKey, TValue>(key, value);
            slots[index].AddLast(newElement);
            Count++;
        }

        private void GrowIfNeeded()
        {
            if ((float)(Count + 1) / Capacity >= LoadFactor)
            {
                var newTable = new HashTable<TKey, TValue>(Capacity * 2, this);

                slots = newTable.slots;
            }
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            try
            {
                Add(key, value);
            }
            catch (ArgumentException argumentException) 
            {
                if(argumentException.Message.Contains("Duplicate Key")
                    && argumentException.ParamName == key.ToString())
                {
                    int index = Math.Abs(key.GetHashCode()) % Capacity;
                    var keyValue = slots[index].FirstOrDefault(kvp => kvp.Key.Equals(key));
                    keyValue.Value = value;
                    return true;
                }

                throw argumentException;
            }

            return false;
        }

        public TValue Get(TKey key)
        {
            var element = Find(key);

            if(element == null)
            {
                throw new KeyNotFoundException();
            }

            return element.Value;
        }

        public TValue this[TKey key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                AddOrReplace(key, value);
            }
        }


        public bool TryGetValue(TKey key, out TValue value)
        {
            var element = this.Find(key);

            if (element != null)
            {
                value = element.Value;
                return true;
            }

            value = default;
            return false;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            int index = Math.Abs(key.GetHashCode()) % Capacity;

            if (slots[index] != null)
            {
                foreach(var kvp in slots[index])
                {
                    if(kvp.Key.Equals(key))
                    {
                        return kvp;
                    }
                }
            }

            return null;
        }

        public bool ContainsKey(TKey key)
        {
            return Find(key) != null;
        }

        public bool Remove(TKey key)
        {
            int index = Math.Abs(key.GetHashCode()) % Capacity;

            if (slots[index] != null)
            {
                var linkedListNode = slots[index].First;

                while (linkedListNode != null)
                {
                    if(linkedListNode.Value.Key.Equals(key))
                    {
                        slots[index].Remove(linkedListNode);
                        Count--;
                        return true;
                    }

                    linkedListNode = linkedListNode.Next;
                }
            }

            return false;
        }

        public void Clear()
        {
            slots = new LinkedList<KeyValue<TKey, TValue>>[DefaultCapacity];
            Count = 0;
        }

        public IEnumerable<TKey> Keys => this.Select(kvp => kvp.Key);

        public IEnumerable<TValue> Values
        {
            get
            {
                var result = new List<TValue>();
                foreach(var slot in slots)
                {
                    if(slot != null)
                    {
                        foreach(var element in slot)
                        {
                            result.Add(element.Value);
                        }
                    }
                }

                return result;
            }
        }

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach(var slot in slots)
            {
                if(slot != null)
                {
                    foreach( var element in slot)
                    {
                        yield return element;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
