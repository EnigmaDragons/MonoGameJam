using System;
using System.Collections.Generic;

namespace ZeroFootPrintSociety.CoreGame
{
    class ActionOptionsAvailable
    {
        public Dictionary<ActionType, Action> Options { get; set; } = new Dictionary<ActionType, Action>();
    }
}
