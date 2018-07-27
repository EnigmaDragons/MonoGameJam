using System.IO;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTiled.Tiled.TmxLoading
{
    public class Tsx
    {
        public int FirstId { get; }
        public int TileWidth { get; }
        public int TileHeight { get; }
        public int Spacing { get; }
        public int TileCount { get; }
        public int Columns { get; }
        public Texture2D TileSource { get; }

        public Tsx(GraphicsDevice device, int firstId, string tsxDir, string tsxFile)
            : this(device, firstId, XDocument.Load(Path.Combine("Content", tsxDir, tsxFile)).Element(XName.Get("tileset"))) {}

        public Tsx(GraphicsDevice device, int firstId, XElement tileset)
        {
            FirstId = firstId;
            TileWidth = new XValue(tileset, "tilewidth").AsInt();
            TileHeight = new XValue(tileset, "tileheight").AsInt();
            Spacing = new XValueWithDefault(tileset, "spacing").AsInt();
            TileCount = new XValue(tileset, "tilecount").AsInt();
            Columns = new XValue(tileset, "columns").AsInt();
            TileSource = new Texture2DFromPath(device, Path.Combine("Content", new XValue(tileset.Element(XName.Get("image")), "source").AsString())).Get();
        }
    }
}
