using System;

namespace MonoDragons.Core
{
    public sealed class Optional<T>
    {
        private readonly T _value;

        public bool HasValue { get; }

        public T Value
        {
            get
            {
                if (!HasValue)
                    throw new InvalidOperationException($"Optional {typeof(T).Name} has no value.");
                return _value;
            }
        }

        public Optional() { }

        public Optional(T value)
        {
            _value = value;
            HasValue = value != null;
        }

        public bool IsTrue(Predicate<T> condition)
        {
            return HasValue && condition(_value);
        }

        public bool IsFalse(Predicate<T> condition)
        {
            return HasValue && !condition(_value);
        }
    }
}
