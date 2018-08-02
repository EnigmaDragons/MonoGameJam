using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeroFootPrintSociety.Tiles
{
    public class GameTileDetail
    {
        public static GameTileDetail None { get; } = new GameTileDetail(null, new Rectangle(), -1, true, Cover.Heavy, false, "", "", false, "");

        public Texture2D Texture { get; }
        public Rectangle SourceRect { get; }
        public int ZIndex { get; }
        public bool IsBlocking { get; }
        public Cover Cover { get; }
        public bool IsVisible { get; }
        public string PostFX { get; }
        public string SpawnCharacter { get; }
        public string Dialog { get; }
        //TODO: these are trash properties
        public bool MustKill { get; }

        public GameTileDetail(Texture2D texture, Rectangle sourceRect, int zIndex, bool isBlocking, 
            Cover cover, bool isHidden, string postFx, string spawnCharacter, bool mustKill, string dialog)
        {
            Texture = texture;
            SourceRect = sourceRect;
            ZIndex = zIndex;
            IsBlocking = isBlocking;
            Cover = cover;
            IsVisible = !isHidden;
            PostFX = postFx;
            SpawnCharacter = spawnCharacter;
            MustKill = mustKill;
            Dialog = dialog;
        }
    }
}
