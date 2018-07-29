using System;

namespace ZeroFootPrintSociety.CoreGame.StateEvents
{
    class ActionSelected
    {
        public Action Action { get; }

        public ActionSelected(Action action) => Action = action;
    }
}
