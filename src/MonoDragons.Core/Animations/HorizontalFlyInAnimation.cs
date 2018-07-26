using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Animations
{
    public sealed class HorizontalFlyInAnimation : IAnimation
    {
        private readonly Queue<IAnimation> _animations = new Queue<IAnimation>();
        private readonly IVisual _visual;

        public int Distance { get; set; } = 1200;
        public int Drift { get; set; } = 100;
        public HorizontalDirection FromDir { get; set; } = HorizontalDirection.Left;
        public HorizontalDirection ToDir { get; set; } = HorizontalDirection.Right;
        public TimeSpan DurationIn { get; set; } = TimeSpan.FromMilliseconds(300);
        public TimeSpan DurationWait { get; set; } = TimeSpan.FromMilliseconds(1800);
        public TimeSpan DurationOut { get; set; } = TimeSpan.FromMilliseconds(300);

        private IAnimation _current;
        private bool _isRunning;

        public HorizontalFlyInAnimation(IVisual visual)
        {
            _visual = visual;
        }

        public void Start(Action onFinished)
        {
            if (_isRunning)
                return;

            _isRunning = true;

            var x1 = -((int)FromDir * Distance);
            var x2 = -((int)FromDir * Drift);
            var x3 = (int)ToDir * Distance;

            _animations.Enqueue(Create(_visual, x1, DurationIn));
            _animations.Enqueue(Create(new WithOffset(_visual, new Vector2(x1, 0)), x2, DurationWait));
            _animations.Enqueue(Create(new WithOffset(_visual, new Vector2(x1 + x2, 0)), x3, DurationOut));

            BeginNext(onFinished);
        }

        public void Update(TimeSpan delta)
        {
            if (_isRunning)
                _current.Update(delta);
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_isRunning)
                _current.Draw(parentTransform);
            else
                _visual.Draw(parentTransform);
        }

        private IAnimation Create(IVisual visual, int xMove, TimeSpan duration)
        {
            return new SinglePositionTraverseAnimation(visual, new Vector2(xMove, 0), duration, TimeSpan.Zero);
        }

        private void BeginNext(Action onFinished)
        {
            if (_animations.Count == 0)
            {
                onFinished();
                _isRunning = false;
            }
            else
            {
                _current = _animations.Dequeue();
                _current.Start(() => BeginNext(onFinished));
            }
        }
    }
}
