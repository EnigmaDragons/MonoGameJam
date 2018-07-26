using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    public class TextureButtonWithText : ClickableUIElement, IVisual
    {
        private readonly Func<bool> _isVisible;
        private readonly Action _onClick;
        private readonly string _text;
        private readonly UiStateTextures _textures;

        private Texture2D _current;

        public Action ExitAction { private get; set; } = () => { };
        public Action EnterAction { private get; set; } = () => { };
        public Action PressAction { private get; set; } = () => { };

        public TextureButtonWithText(Rectangle area, Action onClick, string text, UiStateTextures textures)
            : this(area, onClick, text, textures, () => true) { }

        public TextureButtonWithText(Rectangle area, Action onClick, string text, UiStateTextures textures, Func<bool> isvisible) 
            : base(area)
        {
            _onClick = onClick;
            _text = text;
            _textures = textures;
            _current = _textures.Default;
            _isVisible = isvisible;
        }

        public override void OnEntered()
        {
            _current = _textures.Hover;
            EnterAction();
        }

        public override void OnExitted()
        {
            _current = _textures.Default;
            ExitAction();
        }

        public override void OnPressed()
        {
            _current = _textures.Pressed;
            PressAction();
        }

        public override void OnReleased()
        {
            _current = _textures.Default;
            _onClick();
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_isVisible())
            {
                World.Draw(_current, new Rectangle(Area.Location + parentTransform.Location.ToPoint(), Area.Size));
                UI.DrawTextCentered(_text, new Rectangle(Area.Location + parentTransform.Location.ToPoint(), Area.Size), Color.White);
            }
        }
    }
}
