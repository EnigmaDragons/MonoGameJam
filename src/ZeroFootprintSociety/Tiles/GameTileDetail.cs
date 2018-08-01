using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeroFootPrintSociety.Tiles
{
    public class GameTileDetail
    {
        public static GameTileDetail None { get; } = new GameTileDetail(null, new Rectangle(), -1, true, Cover.Heavy, false, "", "");

        public Texture2D Texture { get; }
        public Rectangle SourceRect { get; }
        public int ZIndex { get; }
        public bool IsBlocking { get; }
        public Cover Cover { get; }
        public bool IsVisible { get; }
        public string PostFX { get; }
        public string SpawnCharacter { get; }

        public GameTileDetail(Texture2D texture, Rectangle sourceRect, int zIndex, bool isBlocking, 
            Cover cover, bool isHidden, string postFx, string spawnCharacter)
        {
            Texture = texture;
            SourceRect = sourceRect;
            ZIndex = zIndex;
            IsBlocking = isBlocking;
            Cover = cover;
            IsVisible = !isHidden;
            PostFX = postFx;
            SpawnCharacter = spawnCharacter;
        }
    }
}
