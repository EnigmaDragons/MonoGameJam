using System.Collections.Generic;

namespace MonoTiled.Tiled
{
    public class DictionaryWithDefault<Key, Value> : Dictionary<Key, Value>
    {
        private readonly Value _defaultValue;

        public DictionaryWithDefault(Value defaultValue)
        {
            _defaultValue = defaultValue;
        }

        public new Value this[Key key]
        {
            get => ContainsKey(key) ? base[key] : _defaultValue;
            set => base[key] = value;
        }
    }
}
