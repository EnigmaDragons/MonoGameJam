using System;
using Microsoft.Xna.Framework;

namespace ZeroFootPrintSociety.PhsyicsMath
{
    public static class VectorMath
    {
        public static Vector2 MoveTowards(this Vector2 start, Vector2 dest, double maxDistance)
        {
            var distance = start.Distance(dest);
            if (distance <= maxDistance)
                return dest;
            var lerpAmount = maxDistance / distance;
            return start.Lerp(dest, lerpAmount);
        }

        public static Vector2 Lerp(this Vector2 start, Vector2 dest, double lerpAmt)
        {
            return new Vector2(start.X + (float)((dest.X - start.X) * lerpAmt), start.Y + (float)((dest.Y - start.Y) * lerpAmt));
        }

        public static double Distance(this Vector2 point1, Vector2 point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }
    }
}
