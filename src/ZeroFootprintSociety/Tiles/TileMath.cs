using System;
using Microsoft.Xna.Framework;

namespace ZeroFootPrintSociety.Tiles
{
    public static class TileMath
    {
        public static int TileDistance(this Point point1, Point point2)
        {
            return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
        } 
    }
}
