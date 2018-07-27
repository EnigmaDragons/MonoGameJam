using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using TiledExample.Tiled;

namespace MonoTiled.Tiled.Orthographic
{
    public class TileMap
    {
        private readonly List<TileLayer> _layers;

        public TileMap(List<TileLayer> layers)
        {
            _layers = layers.OrderBy(x => x.ZIndex).ToList();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _layers.ForEach(x => x.Draw(spriteBatch));
        }
    }
}
