using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace ZeroFootPrintSociety.CoreGame.StateEvents
{
    class MovementConfirmed
    {
        public IReadOnlyList<Point> Path { get; }

        public MovementConfirmed(List<Point> path)
        {
            if (path.Count == 0)
                throw new InvalidOperationException("All Movement Paths must have an ending");
            Path = path;
        }
    }
}
