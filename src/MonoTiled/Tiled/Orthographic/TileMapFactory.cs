using System.Linq;
using Microsoft.Xna.Framework;
using MonoTiled.Tiled.TmxLoading;

namespace MonoTiled.Tiled.Orthographic
{
    public class TileMapFactory
    {
        public TileMap CreateMap(Tmx tmx)
        {
            var details = new TileMapDetails(tmx);
            return new TileMap(tmx.Layers.Select(x => 
                new TileLayer(x.ZIndex, x.Tiles
                    .Where(y => y.TextureId != 0)
                    .Select(y => 
                        new Tile(details.Get(y.TextureId), 
                            new Rectangle(y.Column * tmx.TileWidth, y.Row * tmx.TileHeight, tmx.TileWidth, tmx.TileHeight))).ToList())).ToList());
        }
    }
}
