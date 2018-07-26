using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core.Graphics
{
    public sealed class HollowedRectangleTexture
    {
        private readonly Color _color;
        private readonly Rectangle _rect;
        private readonly int _borderWidth;

        public HollowedRectangleTexture(Color color, Rectangle rect, int borderWidth)
        {
            _color = color;
            _rect = rect;
            _borderWidth = borderWidth;
        }

        public Texture2D Create()
        {
            var data = new Color[_rect.Width * _rect.Height];
            for (var x = 0; x < _rect.Width; x++)
            for (var y = 0; y < _rect.Height; y++)
                if ((x <= _borderWidth || x >= _rect.Width - _borderWidth) && (y <= _borderWidth || y >= _rect.Height - _borderWidth))
                    data[x * y] = _color;

            var texture = new Texture2D(CurrentGame.TheGame.GraphicsDevice, _rect.Width, _rect.Height);
            texture.SetData(data);
            return texture;
        }
    }
}
