using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeroFootPrintSociety.Tiles
{
    public class GameTileDetail
    {
        public Texture2D Texture { get; }
        public Rectangle SourceRect { get; }
        public int ZIndex { get; }

        public GameTileDetail(Texture2D texture, Rectangle sourceRect, int zIndex)
        {
            Texture = texture;
            SourceRect = sourceRect;
            ZIndex = zIndex;
        }
    }
}
