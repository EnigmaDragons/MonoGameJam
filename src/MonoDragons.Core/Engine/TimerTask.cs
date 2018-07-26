using System;

namespace MonoDragons.Core.Engine
{
    public sealed class TimerTask : IAutomaton
    {
        private readonly Action _performTask;
        private readonly double _intervalMs;
        private readonly bool _recurring;
        private bool _isFinished;

        private double _elapsedMs;

        public TimerTask(Action performTask, double intervalMs, bool recurring = true)
        {
            _performTask = performTask;
            _intervalMs = intervalMs;
            _recurring = recurring;
        }

        public void Update(TimeSpan delta)
        {
            if (_isFinished)
                return;

            _elapsedMs += delta.TotalMilliseconds;
            if (!(_elapsedMs > _intervalMs))
                return;

            _performTask();
            _isFinished = true;
            if (_recurring)
                Reset();
        }

        public void Reset()
        {
            _elapsedMs = 0;
            _isFinished = false;
        }
    }
}
