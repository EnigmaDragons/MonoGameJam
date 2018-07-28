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

        public TsxTile(int id, Rectangle sourceRect)
        {
            Construct(id, sourceRect, new DictionaryWithDefault<string, bool>(false));
        }

        public TsxTile(int id, Rectangle sourceRect, XElement tile)
        {
            var customBools = new DictionaryWithDefault<string, bool>(false);
            tile.Element(XName.Get("properties"))
                .Elements(XName.Get("property"))
                .Where(x => new XValue(x, "type").AsString() == "bool")
                .ToList()
                .ForEach(x => customBools[new XValue(x, "name").AsString()] = new XValue(x, "value").AsBool());
            Construct(id, sourceRect, customBools);
        }

        private void Construct(int id, Rectangle sourceRect, DictionaryWithDefault<string, bool> customBools)
        {
            ID = id;
            SourceRect = sourceRect;
            CustomBools = customBools;
        }
    }
}
