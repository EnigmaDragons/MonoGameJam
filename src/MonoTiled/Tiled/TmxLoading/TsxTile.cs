using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace MonoTiled.Tiled.TmxLoading
{
    public class TsxTile
    {
        public int ID { get; private set; }
        public Rectangle SourceRect { get; private set; }
        public DictionaryWithDefault<string, bool> CustomBools { get; private set; }
        public DictionaryWithDefault<string, string> CustomStrings { get; private set; }

        public TsxTile(int id, Rectangle sourceRect)
        {
            Construct(id, sourceRect, new DictionaryWithDefault<string, bool>(false), new DictionaryWithDefault<string, string>(""));
        }

        public TsxTile(int id, Rectangle sourceRect, XElement tile)
        {
            var customBools = new DictionaryWithDefault<string, bool>(false);
            var customStrings = new DictionaryWithDefault<string, string>("");
            var properties = tile.Element(XName.Get("properties")).Elements(XName.Get("property")).ToList();
            properties.Where(x => new XValueWithDefault(x, "type", "string").AsString() == "bool").ToList()
                .ForEach(x => customBools[new XValue(x, "name").AsString()] = new XValue(x, "value").AsBool());
            properties.Where(x => new XValueWithDefault(x, "type", "string").AsString() == "string").ToList()
                .ForEach(x => customStrings[new XValue(x, "name").AsString()] = new XValue(x, "value").AsString());
            Construct(id, sourceRect, customBools, customStrings);
        }

        private void Construct(int id, Rectangle sourceRect, DictionaryWithDefault<string, bool> customBools, DictionaryWithDefault<string, string> customStrings)
        {
            ID = id;
            SourceRect = sourceRect;
            CustomBools = customBools;
            CustomStrings = customStrings;
        }
    }
}
