using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTiled.Tiled.TmxLoading
{
    public class Tsx
    {
        public int FirstId { get; }
        public int TileWidth { get; }
        public int TileHeight { get; }
        public int Spacing { get; }
        public List<TsxTile> Tiles { get; } = new List<TsxTile>();
        public int Columns { get; }
        public Texture2D TileSource { get; }

        public Tsx(GraphicsDevice device, int firstId, string tsxDir, string tsxFile)
            : this(device, firstId, tsxDir, XDocument.Load(Path.Combine("Content", tsxDir, tsxFile)).Element(XName.Get("tileset"))) {}

        public Tsx(GraphicsDevice device, int firstId, string tsxDir, XElement tileset)
        {
            FirstId = firstId;
            TileWidth = new XValue(tileset, "tilewidth").AsInt();
            TileHeight = new XValue(tileset, "tileheight").AsInt();
            Spacing = new XValueWithDefault(tileset, "spacing").AsInt();
            Columns = new XValue(tileset, "columns").AsInt();
            TileSource = new Texture2DFromPath(device, Path.Combine("Content", tsxDir, new XValue(tileset.Element(XName.Get("image")), "source").AsString())).Get();

            var tileElements = tileset.Elements(XName.Get("tile")).ToList();
            var tiles = tileElements.Select(x => new TsxTile(new XValue(x, "id").AsInt(), GetTileRectangle(new XValue(x, "id").AsInt()), x)).ToList();
            var tileMap = new Dictionary<int, TsxTile>();
            tiles.ForEach(x => tileMap[x.ID] = x);
            for (var i = 0; i < new XValue(tileset, "tilecount").AsInt(); i++)
                Tiles.Add(tileMap.ContainsKey(i) ? tileMap[i] : new TsxTile(i, GetTileRectangle(i)));
        }


        private Rectangle GetTileRectangle(int tile)
        {
            var column = tile % Columns;
            var row = (int)Math.Floor((double)tile / Columns);
            var x = column * TileWidth + (column + 1) * Spacing;
            var y = row * TileHeight + (row + 1) * Spacing;
            return new Rectangle(x, y, TileWidth, TileHeight);
        }
    }
}
