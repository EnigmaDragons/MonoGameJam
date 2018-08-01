using System;
using Microsoft.Xna.Framework;

namespace ZeroFootPrintSociety.Tiles
{
    public enum Cover
    {
        None = 0,
        Light = 8,
        Medium = 17,
        Heavy = 34
    }

    public static class CoverExtensions
    {
        public static int ToPercent(this Cover cover)
        {
            return MathHelper.Min(((int) cover) * 3, 100);
        }

        public static string GetVisual(this Cover cover)
        {
            switch (cover)
            {
                case Cover.Light:
                case Cover.Medium:
                case Cover.Heavy:
                    return $"Effects/shield-{Enum.GetName(typeof(Cover), cover).ToLower()}.png";
                default:
                    return null;
            }
        }
    }
}
