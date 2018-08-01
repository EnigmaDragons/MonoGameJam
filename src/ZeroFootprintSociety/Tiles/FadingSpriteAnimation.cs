using System;
using MonoDragons.Core.Animations;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace ZeroFootPrintSociety.Tiles
{
    public class FadingSpriteAnimation : IVisualAutomaton, IAnimation
    {
        private readonly SpriteAnimation _anim;
        private readonly TimeSpan _duration;
        private TimerTask _task;
        private bool _running;

        public FadingSpriteAnimation(SpriteAnimation anim, TimeSpan duration)
        {
            _anim = anim;
            _duration = duration;
        }
      
        public void Update(TimeSpan delta)
        {
            if (_running)
            {
                _task.Update(delta);
                _anim.Update(delta);
            }
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_running)
                _anim.Draw(parentTransform);
        }

        public void Start(Action onFinished)
        {
            _task = new TimerTask(() =>
            {
                _running = false;
                onFinished();
                _anim.Reset();
            }, _duration.TotalMilliseconds, false);
            _running = true;
        }
    }
}