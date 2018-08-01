using System;
using ZeroFootPrintSociety.CoreGame;

namespace ZeroFootPrintSociety.AI
{
    class AIActionQueued
    {
        public Action Action { get; }
        public TimeSpan Delay { get;  }

        public AIActionQueued(Action action)
            : this(action, GameWorld.FriendlyPerception[GameWorld.CurrentCharacter.CurrentTile.Position] 
                  ? TimeSpan.FromMilliseconds(750) 
                  : TimeSpan.FromMilliseconds(0)) { }

        public AIActionQueued(Action action, TimeSpan delay)
        {
            Action = action;
            Delay = delay;
        }
    }
}
