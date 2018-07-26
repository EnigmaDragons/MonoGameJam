using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Graphics;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    public sealed class ColoredRectangle : IVisual
    {
        private Color _color;
        private Texture2D _background;

        public Transform2 Transform { get; set; }

        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                UpdateTexture();
            }
        }

        public ColoredRectangle()
        {
            Transform = new Transform2(new Size2(400, 100));
            _background = new RectangleTexture(Color.Transparent).Create();
        }

        public void Draw(Transform2 parentTransform)
        {
            var position = parentTransform + Transform;
            World.Draw(_background, position.ToRectangle());
        }

        private void UpdateTexture()
        {
            _background = new RectangleTexture(_color).Create();
        }
    }
}