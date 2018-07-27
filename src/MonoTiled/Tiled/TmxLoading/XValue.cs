using System;
using System.Xml.Linq;

namespace MonoTiled.Tiled.TmxLoading
{
    public class XValue
    {
        private readonly XElement _element;
        private readonly string _key;

        public XValue(XElement element, string key)
        {
            _element = element;
            _key = key;
        }

        public int AsInt()
        {
            return int.Parse(AsString());
        }

        public string AsString()
        {
            try
            {
                return _element.Attribute(XName.Get(_key)).Value;
            }
            catch (Exception ex)
            {
                throw new Exception($"Missing Value for {_key} from element {_element.Name.LocalName}", ex);
            }
        }
    }
}
