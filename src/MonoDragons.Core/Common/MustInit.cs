
namespace MonoDragons.Core
{
    public sealed class MustInit<T>
    {
        private readonly string _elementName;

        private T _value;

        public MustInit(string elementName) => _elementName = elementName;

        public bool IsInitialized => _value != null;

        public T Get()
        {
            if (!IsInitialized)
                throw new NotInitializedException(_elementName);
            return _value;
        }

        public void Init(T value) => _value = value;
        public void Set(T value) => Init(value);
    }
}
