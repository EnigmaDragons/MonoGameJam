using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ZeroFootPrintSociety.CoreGame.StateEvents
{
    class MovementOptionsAvailable
    {
        public IReadOnlyList<IReadOnlyList<Point>> AvailableMoves { get; internal set; }
    }
}
