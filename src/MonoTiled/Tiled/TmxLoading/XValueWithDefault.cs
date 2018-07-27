using System;
using System.Linq;
using System.Xml.Linq;

namespace MonoTiled.Tiled.TmxLoading
{
    public class XValueWithDefault
    {
        private readonly XElement _element;
        private readonly string _key;
        private readonly string _defaultStr;
        private readonly int _defaultInt;

        public XValueWithDefault(XElement element, string key, string defaultStr = "", int defaultInt = 0)
        {
            _element = element;
            _key = key;
            _defaultStr = defaultStr;
            _defaultInt = defaultInt;
        }

        public int AsInt()
        {
            return _element.Attributes().Any(x => x.Name.LocalName == XName.Get(_key).LocalName) 
                ? int.Parse(AsString()) 
                : _defaultInt;
        }

        public string AsString()
        {
            return _element.Attributes().Any(x => x.Name.LocalName == XName.Get(_key).LocalName) 
                ? _element.Attribute(XName.Get(_key)).Value 
                : _defaultStr;
        }
    }
}
