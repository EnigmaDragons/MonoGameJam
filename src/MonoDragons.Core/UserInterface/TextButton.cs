using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Graphics;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    public class TextButton : VisualClickableUIElement
    {
        private readonly Action _onClick;
        private readonly string _text;
        private readonly Texture2D _default;
        private readonly Texture2D _hover;
        private readonly Texture2D _press;
        private Texture2D _currentRect;
        private readonly Func<bool> _isVisible;

        public Action OnExit { private get; set; } = () => { };
        public Action OnEnter { private get; set; } = () => { };
        public Action OnPress { private get; set; } = () => { };

        public TextButton(Rectangle area, Action onClick, string text, Color defaultColor, Color hover, Color press)
            : this(area, onClick, text, defaultColor, hover, press, () => true) { }
        public TextButton(Rectangle area, Action onClick, string text, Color defaultColor, Color hover, Color press, Func<bool> isvisible) : base(area)
        {
            _onClick = onClick;
            _text = text;
            _default = new RectangleTexture(defaultColor).Create();
            _hover = new RectangleTexture(hover).Create();
            _press = new RectangleTexture(press).Create();
            _currentRect = _default;
            _isVisible = isvisible;
        }

        public override void OnEntered()
        {
            _currentRect = _hover;
            OnEnter();
        }

        public override void OnExitted()
        {
            _currentRect = _default;
            OnExit();
        }

        public override void OnPressed()
        {
            _currentRect = _press;
            OnPress();
        }

        public override void OnReleased()
        {
            _currentRect = _default;
            _onClick();
        }

        public override void Draw(Transform2 parentTransform)
        {
            if (_isVisible())
            {
                World.Draw(_currentRect, new Rectangle(Area.Location + parentTransform.Location.ToPoint(), Area.Size));
                UI.DrawTextCentered(_text, new Rectangle(Area.Location + parentTransform.Location.ToPoint(), Area.Size), Color.White);
            }
        }
    }
}
