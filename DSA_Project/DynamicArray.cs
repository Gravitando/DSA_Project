namespace VoteAnalyzingSystem
{
    
    public class DynamicArray<T>
    {
        private T[] _array;
        public int Count { get; private set; }

        public DynamicArray()
        {
            _array = new T[4];
            Count = 0;
        }

        public void Add(T item)
        {
            if (Count == _array.Length)
            {
                Resize();
            }
            _array[Count] = item;
            Count++;
        }

        private void Resize()
        {
            T[] newArray = new T[_array.Length * 2];
            Array.Copy(_array, newArray, _array.Length);
            _array = newArray;
        }

        public T this[int index]
        {
            get => _array[index];
            set => _array[index] = value;
        }

        public T Find(Predicate<T> match)
        {
            for (int i = 0; i < Count; i++)
            {
                if (match(_array[i]))
                {
                    return _array[i];
                }
            }
            return default;
        }
    }
}