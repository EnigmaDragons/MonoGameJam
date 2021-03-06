﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace ZeroFootPrintSociety.CoreGame.StateEvents
{
    public class MovementConfirmed
    {
        public IReadOnlyList<Point> Path { get; }

        public MovementConfirmed(IReadOnlyList<Point> path)
        {
            if (path.Count == 0)
                throw new InvalidOperationException("All Movement Paths must have an ending");
            Path = path;
        }
    }
}
