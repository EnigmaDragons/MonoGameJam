using MonoDragons.Core.PhysicsEngine;
using System;

namespace MonoDragons.Core.Animations
{
    public abstract class AnimBase : IAnimation
    {
        protected abstract TimeSpan Duration { get; }
        protected TimeSpan Elapsed { get; private set; }

        private bool _started;
        private Action _onFinished;

        protected abstract void OnStart();
        protected abstract void OnDraw(Transform2 parentTransform);
        protected abstract void OnUpdate(TimeSpan delta);

        public void Start(Action onFinished)
        {
            if (_started) return;

            OnStart();
            _started = true;
        }

        public void Update(TimeSpan delta)
        {
            IfRunning(() => OnUpdate(delta));
        }

        public void Draw(Transform2 parentTransform)
        {
            IfRunning(() => OnDraw(parentTransform));
        }

        private void IfRunning(Action action)
        {
            if (_started && Elapsed <= Duration)
                action();
        }
    }
}