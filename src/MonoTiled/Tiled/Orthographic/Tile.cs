using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace MonoTiled.Tiled.Orthographic
{
    public class Tile
    {
        public TileDetail Detail { get; }
        public Rectangle DestRect { get; }

        public Tile(TileDetail detail, Rectangle destRect)
        {
            Detail = detail;
            DestRect = destRect;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Detail.Texture, DestRect, Detail.SourceRect, Color.White);
        } 
    }
}
