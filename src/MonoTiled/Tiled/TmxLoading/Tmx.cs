using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTiled.Tiled.TmxLoading
{
    public class Tmx
    {
        public int Width { get; }
        public int Height { get; }
        public int TileWidth { get; }
        public int TileHeight { get; }
        public List<Tsx> Tilesets { get; }
        public List<TmxLayer> Layers { get; } = new List<TmxLayer>();

        public Tmx(GraphicsDevice device, string mapDir, string tmxFileName)
        {
            var doc = XDocument.Load(Path.Combine("Content", mapDir, tmxFileName));
            var map = doc.Element(XName.Get("map"));
            Width = new XValue(map, "width").AsInt();
            Height = new XValue(map, "height").AsInt();
            TileWidth = new XValue(map, "tilewidth").AsInt();
            TileHeight = new XValue(map, "tileheight").AsInt();
            Tilesets = map.Elements(XName.Get("tileset"))
                .Select(x => x.HasElements 
                    ? new Tsx(device, new XValue(x, "firstgid").AsInt(), mapDir, x) 
                    : new Tsx(device, new XValue(x, "firstgid").AsInt(), mapDir, new XValue(x, "source").AsString()))
                .ToList();
            var layers = map.Elements(XName.Get("layer")).ToList();
            for (var i = 0; i < layers.Count; i++)
                 Layers.Add(new TmxLayer(i, layers[i]));
        }
    }
}
