﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeroFootPrintSociety.Tiles
{
    public class GameTileDetail
    {
        public Texture2D Texture { get; }
        public Rectangle SourceRect { get; }
        public int ZIndex { get; }
        public bool IsBlocking { get; }
        public Cover Cover { get; }
        public bool IsVisible { get; }
        public string PostFX { get; }

        public GameTileDetail(Texture2D texture, Rectangle sourceRect, int zIndex, bool isBlocking, Cover cover, bool isHidden, string postFx)
        {
            Texture = texture;
            SourceRect = sourceRect;
            ZIndex = zIndex;
            IsBlocking = isBlocking;
            Cover = cover;
            IsVisible = !isHidden;
            PostFX = postFx;
        }
    }
}
