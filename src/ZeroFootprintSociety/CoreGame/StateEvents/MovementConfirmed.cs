using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.StateEvents
{
    class MovementConfirmed
    {
        public Character Character { get; set; }
        public List<Point> Path { get; set;  }
    }
}
