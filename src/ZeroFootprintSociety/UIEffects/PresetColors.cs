using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;

namespace ZeroFootPrintSociety.UIEffects
{
    class PresetColors
    {
        public static readonly DictionaryWithDefault<string, Color> Values = new DictionaryWithDefault<string, Color>(Color.FromNonPremultiplied(255, 255, 0, 120))
        {
            { "blue", Color.FromNonPremultiplied(0, 0, 255, 100) },
        };
    }
}
