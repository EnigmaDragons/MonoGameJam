using System;
using Microsoft.Xna.Framework;

using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Core.Animations
{
    public sealed class ScreenFade : IAnimation
    {
        private readonly UiColoredRectangle _visual = new UiColoredRectangle
        {
            Color = Color.Black,
            Transform = new Transform2(new Size2(1920, 1080))
        };

        public int FromAlpha { get; set; } = 255;
        public int ToAlpha { get; set; } = 0;
        public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(1);
        private TimeSpan _elapsed;
        private bool _started;
        private int _currentAlpha;
        private Action _onFinished;
        
        public void Start(Action onFinished)
        {
            if (_started) return;

            _onFinished = onFinished;
            _visual.Color = _visual.Color.WithAlpha(FromAlpha);
            _started = true;
        }

        public void Update(TimeSpan delta)
        {
            IfRunning(() =>
            {
                _elapsed += delta;
                _currentAlpha = _elapsed >= Duration
                    ? ToAlpha
                    : (int)(MathHelper.Lerp(FromAlpha, ToAlpha, (float)(_elapsed.TotalMilliseconds / Duration.TotalMilliseconds)));
                _visual.Color = _visual.Color.WithAlpha(_currentAlpha);
                if (_elapsed >= Duration)
                    _onFinished();
            });
        }

        public void Draw(Transform2 parentTransform)
        {
            IfRunning(() => _visual.Draw(parentTransform));
        }

        private void IfRunning(Action action)
        {
            if (_started && _elapsed <= Duration)
                action();
        }
    }
}
