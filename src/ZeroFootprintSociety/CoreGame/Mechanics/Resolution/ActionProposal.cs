using MonoDragons.Core.EventSystem;
using System;
using System.Collections.Generic;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Resolution
{
    class ActionProposal
    {
        private Queue<Action> _proposed = new Queue<Action>(1);

        public ActionProposal()
        {
            Event.Subscribe<ActionSelected>(Set, this);
            Event.Subscribe<ActionConfirmed>(e => _proposed.Dequeue().Invoke(), this);
            Event.Subscribe<ActionCancelled>(e => _proposed.Clear(), this);
        }

        private void Set(ActionSelected e)
        {
            _proposed.Clear();
            _proposed.Enqueue(e.Action);
        }
    }
}
