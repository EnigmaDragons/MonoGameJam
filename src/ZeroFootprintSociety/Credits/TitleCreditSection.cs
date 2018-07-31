using Microsoft.Xna.Framework;
using MonoDragons.Core.Animations;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using System;

namespace ZeroFootPrintSociety.Credits
{
    public class TitleCreditSegment : IAnimation
    {
        private bool _isStarted;
        private VerticalFlyInAnimation _animation;

        public void Update(TimeSpan delta)
        {
            if (_isStarted)
                _animation.Update(delta);
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_isStarted)
                _animation.Draw(parentTransform);
        }

        public void Start(Action onFinished)
        {
            _animation = CreateTitle();
            _animation.Start(onFinished);
            _isStarted = true;
        }

        private VerticalFlyInAnimation CreateTitle()
        {
            var titleImage = new UiImage
            {
                Image = "UI/title-placeholder.png",
                Transform = new Transform2(new Vector2(0.5f.VW() - 338, 0.5f.VH() + 500), new Size2(678, 263))
            };
            titleImage.Transform.Location = new Vector2(titleImage.Transform.Location.X, titleImage.Transform.Location.Y + 800);
            return new VerticalFlyInAnimation(titleImage)
            {
                FromDir = VerticalDirection.Down,
                ToDir = VerticalDirection.Up,
                Drift = 200,
                DurationIn = TimeSpan.FromMilliseconds(200),
                DurationWait = TimeSpan.FromMilliseconds(3000),
                DurationOut = TimeSpan.FromMilliseconds(200)
            };
        }
    }
}
