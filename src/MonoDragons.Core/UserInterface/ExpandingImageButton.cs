using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using System;
using System.Windows.Forms;

namespace MonoDragons.Core.UserInterface
{
    public sealed class ExpandingImageButton : VisualClickableUIElement
    {
        private readonly string _basic;
        private readonly string _hover;
        private readonly string _press;
        private readonly Transform2 _expandedTransform;
        private readonly Transform2 _baseTransform;
        private readonly Action _onClick;
        private readonly Func<bool> _isVisible;

        public Action OnPress { get; set; } = () => { };
        public Action OnEnter { get; set; } = () => { };
        public Action OnExit { get; set; } = () => { };
        public Cursor HoveredCursor { get; set; } = Cursors.Default;

        private string _current;
        private Transform2 _currentTransform;

        public ExpandingImageButton(string basic, string hover, string press, Transform2 transform, Size2 sizeIncrease, Action onClick)
            : this(basic, hover, press, transform, sizeIncrease, onClick, () => true) { }

        public ExpandingImageButton(string basic, string hover, string press, Transform2 transform, Size2 sizeIncrease, Action onClick,
            Func<bool> isVisible)
            : base(transform.ToRectangle())
        {
            _basic = basic;
            _hover = hover;
            _press = press;
            _expandedTransform = new Transform2(
                new Vector2(transform.Location.X - sizeIncrease.Width / 2, transform.Location.Y - sizeIncrease.Height / 2),
                new Size2(transform.Size.Width + sizeIncrease.Width, transform.Size.Height + sizeIncrease.Height));
            _baseTransform = transform;
            _onClick = onClick;
            _isVisible = isVisible;

            _current = _basic;
            _currentTransform = _baseTransform;
        }

        public override void Draw(Transform2 parentTransform)
        {
            if (_isVisible())
                World.Draw(_current, parentTransform + _currentTransform);
        }

        public override void OnEntered()
        {
            _current = _hover;
            _currentTransform = _expandedTransform;
            CurrentGame.Cursor = HoveredCursor;

            if (_isVisible())
                OnEnter();
        }

        public override void OnExitted()
        {
            _current = _basic;
            _currentTransform = _baseTransform;
            CurrentGame.Cursor = Cursors.Default;

            if (_isVisible())
                OnExit();
        }

        public override void OnPressed()
        {
            _current = _press;

            if (_isVisible())
                OnPress();
        }

        public override void OnReleased()
        {
            _current = _basic;

            if (_isVisible())
                _onClick.Invoke();
        }

        public override string ToString()
        {
            return _current;
        }
    }
}
