using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Xna.Framework;
using System.Text;

namespace MonoDragons.Core.Text
{
    public class ScaledSpriteFontSet : IEnumerable<SpriteFont>
    {
        public SpriteFont DefaultFont { get; }
        private Dictionary<float, SpriteFont> _fonts;
        public SpriteFont this[float index] => _fonts[index];

        public ScaledSpriteFontSet(SpriteFont defaultFont, Dictionary<float, SpriteFont> allFonts)
        {
            DefaultFont = defaultFont;
            _fonts = allFonts;
        }

        public bool Contains(float scale)
        {
            return _fonts.ContainsKey(scale);
        }

        public Vector2 MeasureString(string text)
        {
            return DefaultFont.MeasureString(text);
        }

        public Vector2 MeasureString(StringBuilder text)
        {
            return DefaultFont.MeasureString(text);
        }

        public IEnumerator<SpriteFont> GetEnumerator()
        {
            return _fonts.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _fonts.Values.GetEnumerator();
        }
    }
}
