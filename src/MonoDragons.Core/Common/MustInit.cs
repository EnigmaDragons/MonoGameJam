
namespace MonoDragons.Core.Common
{
    public sealed class MustInit<T>
    {
        private readonly string _elementName;

        private T _value;

        public MustInit(string elementName)
        {
            _elementName = elementName;
        }

        public T Get()
        {
            if (_value == null)
                throw new NotInitializedException(_elementName);
            return _value;
        }

        public void Init(T value)
        {
            _value = value;
        }

        public void Set(T value) => Init(value);
    }
}
