using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoTiled.Tiled.TmxLoading;

namespace MonoTiled.Tiled.Orthographic
{
    public class TileMapDetails
    {
        private readonly Dictionary<int, TileDetail> _tiles = new Dictionary<int, TileDetail>();

        public TileMapDetails(Tmx tmx)
        {
            tmx.Tilesets.ForEach(AddTileset);
        }

        public TileDetail Get(int id)
        {
            return _tiles[id];
        }

        private void AddTileset(Tsx tsx)
        {
            for (int i = 0; i < tsx.TileCount; i++)
                _tiles[tsx.FirstId + i] = new TileDetail(tsx.TileSource, GetTileRectangle(i, tsx));
        }

        private Rectangle GetTileRectangle(int tile, Tsx tsx)
        {
            var column = tile % tsx.Columns;
            var row = (int)Math.Floor((double)tile / tsx.Columns);
            var x = column * tsx.TileWidth + (column + 1) * tsx.Spacing;
            var y = row * tsx.TileHeight + (row + 1) * tsx.Spacing;
            return new Rectangle(x, y, tsx.TileWidth, tsx.TileHeight);
        }
    }
}
