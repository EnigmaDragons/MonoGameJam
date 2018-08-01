using Microsoft.Xna.Framework;
using System;

namespace MonoDragons.Core.UserInterface
{
    public static class UIMathExtensions
    {
        public static int VW(this float ofScreen) => UI.OfScreenWidth(ofScreen);
        public static int VH(this float ofScreen) => UI.OfScreenHeight(ofScreen);
        public static int VW(this double ofScreen) => UI.OfScreenWidth(Convert.ToSingle(ofScreen));
        public static int VH(this double ofScreen) => UI.OfScreenHeight(Convert.ToSingle(ofScreen));
        public static Color Alpha(this int alpha) => Color.FromNonPremultiplied(255, 255, 255, alpha);
        public static Color WithAlpha(this Color color, int alpha) => Color.FromNonPremultiplied(color.R, color.G, color.B, alpha);
    }
}
