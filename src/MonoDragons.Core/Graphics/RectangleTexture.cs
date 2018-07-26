using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core.Graphics
{
    public class RectangleTexture
    {
        private static readonly Dictionary<Color, Texture2D> Textures = new Dictionary<Color, Texture2D>();

        private readonly Color _color;
        
        public RectangleTexture(Color color)
        {
            _color = color;
        }

        public Texture2D Create()
        {
            if (!Textures.ContainsKey(_color))
            {
                var data = new[] {_color};
                var texture = new Texture2D(CurrentGame.TheGame.GraphicsDevice, 1, 1);
                texture.SetData(data);
                Textures[_color] = texture;
            }

            return Textures[_color];
        }
    }
}
