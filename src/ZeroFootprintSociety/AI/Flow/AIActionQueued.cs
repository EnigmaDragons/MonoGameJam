using System;

namespace ZeroFootPrintSociety.AI
{
    class AIActionQueued
    {
        public Action Action { get; }
        public TimeSpan Delay { get;  }

        public AIActionQueued(Action action)
            : this(action, TimeSpan.FromMilliseconds(0)) { }

        public AIActionQueued(Action action, TimeSpan delay)
        {
            Action = action;
            Delay = delay;
        }
    }
}
