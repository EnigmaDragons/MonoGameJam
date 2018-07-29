using System;

namespace ZeroFootPrintSociety.CoreGame
{
    public class ShotConfirmed
    {
        public ShotProposed Proposed { get; set; }
        public Action OnFinished { get; set; }
    }
}
