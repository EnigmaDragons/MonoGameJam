using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace ZeroFootPrintSociety.CoreGame.StateEvents
{
    class MovementConfirmed
    {
        private List<Point> _path;
        
        public List<Point> Path {
            get => _path;
            set
            {
                if (value.Count == 0)
                    throw new InvalidOperationException("All Movement Paths must have an ending");
                _path = value;
            }
        }
    }
}
