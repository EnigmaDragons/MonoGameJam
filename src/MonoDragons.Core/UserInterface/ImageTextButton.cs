using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Text;

namespace MonoDragons.Core.UserInterface
{
    public class ImageTextButton : VisualClickableUIElement, ISpatial
    {
        private readonly ImageButton _button;
        private readonly Label _label;
        private readonly Func<bool> _isVisible;

        public Transform2 Transform => new Transform2(Area);
        public string Text { set => _label.Text = value; }
        public Color TextColor { set => _label.TextColor = value; }
        public Action OnPress { set => _button.OnPress = value; }
        public Action OnEnter { set => _button.OnEnter = value; }
        public HorizontalAlignment TextAlignment { set => _label.HorizontalAlignment = value; }
        public Transform2 TextTransform { set => _label.Transform = value.WithPadding(8, 8); }
        public Action OnClick { set => _button.OnClick = value; }

        public ImageTextButton(Rectangle rect, string text, string basic, string hover, string press)
            : this(new Transform2(rect), () => {}, text, basic, hover, press, () => true) { }

        public ImageTextButton(Rectangle rect, Action onClick, string text, string basic, string hover, string press)
            : this(new Transform2(rect), onClick, text, basic, hover, press, () => true) { }

        public ImageTextButton(Transform2 transform, Action onClick, string text, string basic, string hover, string press)
            : this(transform, onClick, text, basic, hover, press, () => true) { }

        public ImageTextButton(Transform2 transform, Action onClick, string text, string basic, string hover, string press, Func<bool> isVisible)
            : base(transform.ToRectangle())
        {
            _isVisible = isVisible;
            _button = new ImageButton(basic, hover, press, transform, onClick, _isVisible);
            _label = new Label { BackgroundColor = Color.Transparent, Text = text, Transform = transform.WithPadding(8, 8), TextColor = DefaultFont.Color };
        }

        public override void OnEntered()
        {
            _button.OnEntered();
        }

        public override void OnExitted()
        {
            _button.OnExitted();
        }

        public override void OnPressed()
        {
            _button.OnPressed();
        }

        public override void OnReleased()
        {
            _button.OnReleased();
        }

        public override void Draw(Transform2 parentTransform)
        {
            if (_isVisible())
            {
                _button.Draw(parentTransform);
                _label.Draw(parentTransform);
            }
        }
    }
}
