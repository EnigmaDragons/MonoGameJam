using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeroFootPrintSociety.Tiles
{
    public class GameTileDetail
    {
        public Texture2D Texture { get; }
        public Rectangle SourceRect { get; }
        public int ZIndex { get; }
        public bool IsBlocking { get; }

        public GameTileDetail(Texture2D texture, Rectangle sourceRect, int zIndex, bool isBlocking)
        {
            Texture = texture;
            SourceRect = sourceRect;
            ZIndex = zIndex;
            IsBlocking = isBlocking;
        }
    }
}
