using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTiled.Tiled.Orthographic
{
    public class TileDetail
    {
        public Texture2D Texture { get; }
        public Rectangle SourceRect { get; }

        public TileDetail(Texture2D texture, Rectangle sourceRect)
        {
            Texture = texture;
            SourceRect = sourceRect;
        }
    }
}
