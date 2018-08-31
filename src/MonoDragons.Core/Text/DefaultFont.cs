using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;
using System.Globalization;

namespace MonoDragons.Core.Text
{
    public static class DefaultFont
    {
        public static ScaledSpriteFontSet ScaledFontSet { internal get; set; }
        public static SpriteFont Value => ScaledFontSet.DefaultFont;

        public static string Name { get; set; } = "Fonts/Audiowide";
        public static Color Color { get; set; } = Color.White;
        public static float[] AvailableScales { get; set; } = new float[0];
        public static Optional<int> FutureLineSpacing { get; set; } = new Optional<int>();
        public static Optional<float> FutureSpacing { get; set; } = new Optional<float>();

        public static void Load(ContentManager content)
        {
            if (content == null)
                return;
            var defaultFont = content.Load<SpriteFont>(Name);
            var allFonts = new Dictionary<float, SpriteFont> { { 1, defaultFont } };
            AvailableScales.ForEach(s => allFonts.Add(s, content.Load<SpriteFont>(Name + "-" + s.ToString(CultureInfo.InvariantCulture))));
            ScaledFontSet = new ScaledSpriteFontSet(defaultFont, allFonts);
            if (FutureLineSpacing.HasValue)
                ScaledFontSet.ForEach(f => f.LineSpacing = FutureLineSpacing.Value);
            if (FutureSpacing.HasValue)
                ScaledFontSet.ForEach(f => f.Spacing = FutureSpacing.Value);
        }
    }
}
