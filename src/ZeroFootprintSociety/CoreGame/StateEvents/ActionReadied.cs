using System;

namespace ZeroFootPrintSociety.CoreGame.StateEvents
{
    class ActionReadied
    {
        public Action Action { get; }

        public ActionReadied(Action action) => Action = action;
    }
}
