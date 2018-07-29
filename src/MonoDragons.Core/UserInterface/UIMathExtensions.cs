using System;

namespace MonoDragons.Core.UserInterface
{
    public static class UIMathExtensions
    {
        public static int VW(this float ofScreen) => UI.OfScreenWidth(ofScreen);
        public static int VH(this float ofScreen) => UI.OfScreenHeight(ofScreen);
        public static int VW(this double ofScreen) => UI.OfScreenWidth(Convert.ToSingle(ofScreen));
        public static int VH(this double ofScreen) => UI.OfScreenHeight(Convert.ToSingle(ofScreen));
    }
}
