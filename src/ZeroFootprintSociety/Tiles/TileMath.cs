using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.Tiles
{
    public static class TileMath
    {
        public static int TileDistance(this Point point1, Point point2)
        {
            return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
        }

        public static List<Direction> PrimaryDirectionsTowards(this Point origin, Point dest)
        {
            var directions = new List<Direction>();
            if (Math.Abs(origin.X - dest.X) >= Math.Abs(origin.Y - dest.Y))
            {
                if (origin.X - dest.X > 0)
                    directions.Add(Direction.Left);
                if (origin.X - dest.X < 0)
                    directions.Add(Direction.Right);
            }
            if (Math.Abs(origin.X - dest.X) <= Math.Abs(origin.Y - dest.Y))
            {
                if (origin.Y - dest.Y > 0)
                    directions.Add(Direction.Down);
                if (origin.Y - dest.Y < 0)
                    directions.Add(Direction.Up);
            }
            return directions;
        }
    }
}
