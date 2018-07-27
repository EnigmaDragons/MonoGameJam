using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TiledExample.Tiled;

namespace MonoTiled.Tiled.TmxLoading
{
    public class TmxLayer
    {
        public int ZIndex { get; }
        public int Width { get; }
        public int Height { get; }
        public List<TmxTile> Tiles { get; } = new List<TmxTile>();

        public TmxLayer(int zIndex, XElement layer)
        {
            ZIndex = zIndex;
            Width = new XValue(layer, "width").AsInt();
            Height = new XValue(layer, "height").AsInt();
            var textureIds = new IntegersInText(layer.Element(XName.Get("data")).Value).Get().ToList();
            for (var i = 0; i < textureIds.Count; i++)
                Tiles.Add(new TmxTile(i % Width, (int)Math.Floor((double)i / Width), textureIds[i]));
        }
    }
}
