using Microsoft.Xna.Framework;

namespace MonoDragons.Core.Common
{
    public static class MathExtensions
    {
        public static Point InvertY(this Point p) => new Point(p.X, -p.Y);
        public static Vector2 InvertY(this Vector2 v) => new Vector2(v.X, -v.Y);
    }
}
