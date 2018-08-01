using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Animations;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace ZeroFootPrintSociety.GUI
{
    public class FadingVisual : IAnimation, IVisualAutomaton
    {
        private readonly IVisual _visual;
        private readonly Action<Color> _setColor;

        public int FromAlpha { get; set; } = 255;
        public int ToAlpha { get; set; } = 0;
        public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(1);
        public Color SourceColor { get; set; }
        
        private TimeSpan _elapsed;
        private bool _started;
        private int _currentAlpha;
        private Action _onFinished;

        public FadingVisual(IVisual visual, Action<Color> setColor)
        {
            _visual = visual;
            _setColor = setColor;
        }
        
        public void Start(Action onFinished)
        {
            if (_started) return;

            _onFinished = onFinished;
            _setColor(SourceColor.WithAlpha(FromAlpha));
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
                _setColor(SourceColor.WithAlpha(_currentAlpha));
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