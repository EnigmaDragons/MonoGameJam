using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using System;

namespace MonoDragons.Core.UserInterface
{
    public sealed class ImageButton : VisualClickableUIElement
    {
        private readonly string _basic;
        private readonly string _hover;
        private readonly string _press;
        private readonly Transform2 _transform;
        private readonly Func<bool> _isVisible;

        public Action OnClick { get; set; } = () => { };
        public Action OnPress { get; set; } = () => { };
        public Action OnEnter { get; set; } = () => { };
        public Action OnExit { get; set; } = () => { };

        private string _current;

        public ImageButton(string basic, string hover, string press, Transform2 transform, Action onClick)
            : this(basic, hover, press, transform, onClick, () => true) { }

        public ImageButton(string basic, string hover, string press, Transform2 transform, Action onClick, Func<bool> isVisible)
            : base(transform.ToRectangle())
        {
            _basic = basic;
            _hover = hover;
            _press = press;
            _transform = transform;
            _isVisible = isVisible;

            OnClick = onClick;
            _current = _basic;
        }

        public override void Draw(Transform2 parentTransform)
        {
            if (_isVisible())
                World.Draw(_current, parentTransform + _transform);
        }

        public override void OnEntered()
        {
            _current = _hover;

            if (_isVisible())
                OnEnter();
        }

        public override void OnExitted()
        {
            _current = _basic;

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
                OnClick.Invoke();
        }

        public override string ToString()
        {
            return _current;
        }
    }
}
