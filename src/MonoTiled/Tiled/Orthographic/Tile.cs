using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace MonoTiled.Tiled.Orthographic
{
    public class Tile
    {
        private readonly TileDetail _detail;
        private readonly Rectangle _destRect;

        public Tile(TileDetail detail, Rectangle destRect)
        {
            _detail = detail;
            _destRect = destRect;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_detail.Texture, _destRect, _detail.SourceRect, Color.White);
        } 
    }
}
